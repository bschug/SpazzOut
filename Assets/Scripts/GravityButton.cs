using UnityEngine;
using System.Collections;

public class GravityButton : MonoBehaviour {

	bool isPressed;

	void Start () {
		isPressed = false;
	}

	void OnCollisionEnter (Collision col)
	{
		if (isPressed) return;
		Debug.Log ("MY FINGER IS ON THE BUTTON");

		foreach (var contact in col.contacts) {
			if (contact.otherCollider.rigidbody != null) {
				Physics.gravity = new Vector3 (0, -0.1f, 0); //Vector3.zero;
				foreach (var obj in GameObject.FindObjectsOfType<Rigidbody>()) {
					obj.AddForce (Random.insideUnitSphere, ForceMode.Impulse);
				}
				isPressed = true;
			}
		}
	}


}
