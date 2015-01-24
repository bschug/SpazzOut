using UnityEngine;
using System.Collections;

public class LimbCtr : MonoBehaviour, ITriAxisControllable {

	public Texture		sprite;
	public Rigidbody 	upperLimb;
	public Rigidbody 	lowerLimb;
	public float 		sensitivity = 45.0f;

	public void UpdateAxes(Vector3 axes)
	{
		upperLimb.AddRelativeTorque (new Vector3 (axes.y*sensitivity, 0, 0));
		lowerLimb.AddRelativeTorque (new Vector3 (axes.z*sensitivity, 0, 0));
	}
}
