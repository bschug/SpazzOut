using UnityEngine;
using System.Collections;

public class MaintainRotation : MonoBehaviour {

	Quaternion originalRotation;

	// Use this for initialization
	void Start () {
		originalRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		//rigidbody.MoveRotation (originalRotation);
		//var rotation = Quaternion.Inverse (transform.rotation) * originalRotation;
		//rigidbody.AddTorque (rotation.eulerAngles * 100000, ForceMode.Force);
	}
}
