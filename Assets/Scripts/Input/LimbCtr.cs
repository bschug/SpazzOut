using UnityEngine;
using System.Collections;

public class LimbCtr : MonoBehaviour, ITriAxisControllable, IGroundTouchable {

	public Texture		sprite;
	public Rigidbody 	upperLimb;
	public Rigidbody 	lowerLimb;
	public ParticleSystem	upperLimbIndicator;
	public ParticleSystem	lowerLimbIndicator;
	public float 		sensitivity = 45.0f;
	// set by InputCtrl
	int m_controllingPlayerIndex;
	public int			ControllingPlayerIndex
	{
		get{ return m_controllingPlayerIndex; }
		set{
			m_controllingPlayerIndex = value;
			upperLimbIndicator.startColor = CharacterColors.Instance._colors[value];
			lowerLimbIndicator.startColor = CharacterColors.Instance._colors[value];
		}
	}

	public bool TouchesGround{
		get; set;
	}

	public virtual void UpdateAxes(Vector3 axes)
	{
		upperLimb.AddRelativeTorque (new Vector3 (-axes.y*sensitivity, 0, 0));
		float axis = Mathf.Abs (axes.y);
		upperLimbIndicator.transform.localScale = new Vector3(axis, axis, axis);
		lowerLimb.AddRelativeTorque (new Vector3 (axes.z*sensitivity, 0, 0));
		axis = Mathf.Abs (axes.z);
		lowerLimbIndicator.transform.localScale = new Vector3(axis, axis, axis);
	}
}
