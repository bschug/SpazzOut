using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputCtrUI : MonoBehaviour {
	
	public InputCtrl _viewTarget;
	public Text[] _playerTexts;

	void Start()
	{
		_viewTarget.EventPlayerStateChanged+= OnPlayerStateChanged;

		UpdateAllPlayers();
	}

	void UpdateAllPlayers()
	{
		var players = _viewTarget.Players;
		for(int i = 0; i < players.Length; i++)
		{
			switch(players[i]._state)
			{
			case InputCtrl.Player.PlayerState.NotConnected: 
				_playerTexts[i].text = "P"+(i+1)+" Connect";
				break;
			case InputCtrl.Player.PlayerState.Pending: 
				_playerTexts[i].text = "P"+(i+1)+" Press START";
				break;
			case InputCtrl.Player.PlayerState.Active: 
				_playerTexts[i].text = "P"+(i+1)+" Active";
				break;
			}
		}
	}

	void OnPlayerStateChanged(InputCtrl.Player player, InputCtrl.Player.PlayerState oldState, InputCtrl.Player.PlayerState newState)
	{
		UpdateAllPlayers();
	}
}
