using UnityEngine;
using System.Collections;

public class LegCtr : LimbCtr {
	
	public Rigidbody 	hip;
	public float		hipForce = 5.75f;
	public float		limbForce = 2.0f;

	public override void UpdateAxes(Vector3 axes)
	{
		base.UpdateAxes(axes);
		if (axes.z != 0.0f && TouchesGround) {
			hip.AddForce (Vector3.up * hipForce * Physics.gravity.magnitude);
			lowerLimb.AddForce (Vector3.down * limbForce * Physics.gravity.magnitude * 0.5f);
		}
	}
}
