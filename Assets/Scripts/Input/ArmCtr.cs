using UnityEngine;
using System.Collections;

public class ArmCtr : LimbCtr {

	public override void UpdateAxes(Vector3 axes)
	{
		// no base call
		upperLimb.AddRelativeTorque (new Vector3 (0, axes.x*sensitivity, 0));
		lowerLimb.AddRelativeTorque (new Vector3 (0, axes.y*sensitivity, 0));
	}
}
