using UnityEngine;
using System.Collections;

public class LimbCtr : MonoBehaviour, ITriAxisControllable, IGroundTouchable {

	public Texture		sprite;
	public Rigidbody 	upperLimb;
	public Rigidbody 	lowerLimb;
	public float 		sensitivity = 45.0f;
	public Rigidbody 	hip;
	public float		hipForce = 5.75f;
	public float		limbForce = 2.0f;

	public bool TouchesGround{
		get; set;
	}

	public virtual void UpdateAxes(Vector3 axes)
	{
		upperLimb.AddRelativeTorque (new Vector3 (axes.y*sensitivity, 0, 0));
		lowerLimb.AddRelativeTorque (new Vector3 (axes.z*sensitivity, 0, 0));
		if (axes.z != 0.0f && TouchesGround) {
			hip.AddForce (Vector3.up * hipForce * Physics.gravity.magnitude);
			lowerLimb.AddForce (Vector3.down * limbForce * Physics.gravity.magnitude * 0.5f);
		}
	}
}
