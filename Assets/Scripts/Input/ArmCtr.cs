using UnityEngine;
using System.Collections;

public class ArmCtr : LimbCtr {

	public float _armForce = 20.0f;
	public Rigidbody _hip;

	public override void UpdateAxes(Vector3 axes)
	{
		// no base call
		float axis = Mathf.Abs (axes.x);
		upperLimbIndicator.transform.localScale = new Vector3(axis, axis, axis);
		upperLimb.AddRelativeTorque (new Vector3 (0, axes.x*sensitivity, 0));
		axis = Mathf.Abs (axes.y);
		lowerLimbIndicator.transform.localScale = new Vector3(axis, axis, axis);
		lowerLimb.AddRelativeTorque (new Vector3 (0, axes.y*sensitivity, 0));
		// add torque in hand's direction
		//lowerLimb.AddRelativeForce(new Vector3(_armForce, 0.0f, 0.0f));
		Vector3 force = lowerLimb.transform.forward*_armForce;
		force.y = 0.0f;
		_hip.AddForce(force);
	}
}
