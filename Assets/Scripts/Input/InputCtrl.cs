using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ITriAxisControllable {
	void UpdateAxes(Vector3 axes);
}

public class InputCtrl : MonoBehaviour {

	public delegate void PlayerStateChanged(Player player, Player.PlayerState oldState, Player.PlayerState newState);
	public event PlayerStateChanged EventPlayerStateChanged;

	public class ControllerSource
	{
		public enum ControllerHalf
		{
			Left,
			Right
		}
		public Player			_player;
		public ControllerHalf 	_controllerHalf;

		public ControllerSource(Player player, ControllerHalf controllerHalf)
		{
			_player = player;
			_controllerHalf = controllerHalf;
		}
	}

	public struct Player
	{
		public enum PlayerState
		{
			NotConnected,
			Pending,
			Active
		}
		public int			_currentIndex;
		public PlayerState	_state;
		public string 		_controllerName;
		public int 			_controllerNumber;
		public LimbCtr		_controlledLimb;
	}

	public LimbCtr[] _limbs = new LimbCtr[4];
	ControllerSource[] m_limbSources = new ControllerSource[4];
	Player[] m_players = new Player[4];

	public Player[] Players
	{
		get{
			return m_players;
		}
	}

	void FixedUpdate()
	{
		var controllers = Input.GetJoystickNames();
		UpdateConnectedPlayers(controllers);
		UpdatePlayerInput();
	}

	void UpdatePlayerInput()
	{
		// Update pending player input
		for(int plIdx = 0; plIdx < m_players.Length; ++plIdx)
		{
			if( m_players[plIdx]._state == Player.PlayerState.Pending)
			{
				if(Input.GetButtonDown("Start_"+(m_players[plIdx]._controllerNumber)))
				{
					JoinPlayer(plIdx);
				}
			}
		}

		for(int i = 0; i < m_limbSources.Length; ++i)
		{
			if(m_limbSources[i]!=null)
			{
				UpdateLimbInput(_limbs[i], m_limbSources[i]._player._controllerNumber, m_limbSources[i]._controllerHalf);
			}
		}
	}

	void UpdateLimbInput(LimbCtr limb, int controller, ControllerSource.ControllerHalf half)
	{
		string side = (half == ControllerSource.ControllerHalf.Left) ? "L" : "R";
		float x_vel = Input.GetAxis(side + "_XAxis_" + controller);
		float y_vel = Input.GetAxis(side + "_YAxis_" + controller);
		float z_vel = Input.GetAxis("Triggers" + side + "_" + controller);
		limb.UpdateAxes(new Vector3(x_vel, y_vel, z_vel));
		//Debug.Log ("Input on controller #"+controller+": "+x_vel+", "+y_vel+", "+z_vel);

	}

	void UpdateConnectedPlayers(string[] controllers)
	{
		// Check for Players that have to be removed
		for(int plIdx = 0; plIdx < m_players.Length; ++plIdx)
		{
			if(m_players[plIdx]._state!= Player.PlayerState.NotConnected)
			{
				int ctrIdx = m_players[plIdx]._controllerNumber;
				if(controllers.Length>(Mathf.Max(ctrIdx-1,0)) && m_players[plIdx]._controllerName == controllers[ctrIdx-1])
					continue;
				else
				{
					RemovePlayer(plIdx);
					plIdx--;	// The next player will advance up the list.
				}
			}
		}

		// Check for new controllers
		for(int ctrIdx = 0; ctrIdx < controllers.Length; ++ctrIdx)
		{
			if(!string.IsNullOrEmpty(controllers[ctrIdx]))
			{
				bool registered = false;
				for(int plIdx = 0; plIdx < m_players.Length; ++plIdx)
				{
					if(m_players[plIdx]._controllerNumber == ctrIdx+1)
					{
						registered = true;
						continue; // already signed up
					}
				}
				if(!registered)
					AddAvailablePlayer(ctrIdx+1, controllers[ctrIdx]);
			}
		}
	}

	void AddAvailablePlayer(int controllerIndex, string controllerName)
	{
		for(int plIdx = 0; plIdx < m_players.Length; ++plIdx)
		{
			if(m_players[plIdx]._state == Player.PlayerState.NotConnected)
			{
				Debug.Log ("Adding player #"+plIdx+" on controller #"+controllerIndex);
				m_players[plIdx]._state = Player.PlayerState.Pending;
				m_players[plIdx]._controllerName = controllerName;
				m_players[plIdx]._controllerNumber = controllerIndex;
				OnPlayersChanged();
				if(EventPlayerStateChanged!=null)
					EventPlayerStateChanged(m_players[plIdx], Player.PlayerState.NotConnected, Player.PlayerState.Pending);
				return;
			}
		}
	}

	void JoinPlayer(int index)
	{
		Debug.Log ("Joining player #"+index+" on controller #"+m_players[index]._controllerNumber);
		Player.PlayerState oldState = m_players[index]._state;
		m_players[index]._state = Player.PlayerState.Active;
		OnPlayersChanged();
		if(EventPlayerStateChanged!=null)
			EventPlayerStateChanged(m_players[index], oldState, m_players[index]._state);
	}

	void RemovePlayer(int index)
	{
		Debug.Log ("Removing player #"+index+" on controller #"+m_players[index]._controllerNumber);
		Player.PlayerState oldState = m_players[index]._state;
		m_players[index] = new Player();
		OnPlayersChanged();
		if(EventPlayerStateChanged!=null)
			EventPlayerStateChanged(m_players[index], oldState, m_players[index]._state);
	}

	void OnPlayersChanged()
	{
		List<int> activePlayerIndexes = new List<int>();
		for(int plIdx = 0; plIdx < m_players.Length; ++plIdx)
		{
			if(m_players[plIdx]._state == Player.PlayerState.Active)
			{
				m_players[plIdx]._currentIndex = activePlayerIndexes.Count;
				activePlayerIndexes.Add (plIdx);
			}
		}
		switch(activePlayerIndexes.Count)
		{
		case 0:
			m_limbSources[0] = null;
			m_limbSources[1] = null;
			m_limbSources[2] = null;
			m_limbSources[3] = null;
			break;
		case 1:
			m_limbSources[0] = new ControllerSource(m_players[activePlayerIndexes[0]], ControllerSource.ControllerHalf.Left);
			m_limbSources[1] = new ControllerSource(m_players[activePlayerIndexes[0]], ControllerSource.ControllerHalf.Right);
			m_limbSources[2] = null;
			m_limbSources[3] = null;
			break;
		case 2:
			m_limbSources[0] = new ControllerSource(m_players[activePlayerIndexes[0]], ControllerSource.ControllerHalf.Left);
			m_limbSources[1] = new ControllerSource(m_players[activePlayerIndexes[0]], ControllerSource.ControllerHalf.Right);
			m_limbSources[2] = new ControllerSource(m_players[activePlayerIndexes[1]], ControllerSource.ControllerHalf.Left);
			m_limbSources[3] = new ControllerSource(m_players[activePlayerIndexes[1]], ControllerSource.ControllerHalf.Right);
			break;
		case 3:
			m_limbSources[0] = new ControllerSource(m_players[activePlayerIndexes[0]], ControllerSource.ControllerHalf.Left);
			m_limbSources[1] = new ControllerSource(m_players[activePlayerIndexes[1]], ControllerSource.ControllerHalf.Left);
			m_limbSources[2] = new ControllerSource(m_players[activePlayerIndexes[2]], ControllerSource.ControllerHalf.Left);
			m_limbSources[3] = new ControllerSource(m_players[activePlayerIndexes[2]], ControllerSource.ControllerHalf.Right);
			break;
		case 4:
			m_limbSources[0] = new ControllerSource(m_players[activePlayerIndexes[0]], ControllerSource.ControllerHalf.Left);
			m_limbSources[1] = new ControllerSource(m_players[activePlayerIndexes[1]], ControllerSource.ControllerHalf.Left);
			m_limbSources[2] = new ControllerSource(m_players[activePlayerIndexes[2]], ControllerSource.ControllerHalf.Left);
			m_limbSources[3] = new ControllerSource(m_players[activePlayerIndexes[3]], ControllerSource.ControllerHalf.Left);
			break;
		}
	}
}
