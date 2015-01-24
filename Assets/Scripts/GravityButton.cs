using UnityEngine;
using System.Collections;

public class GravityButton : MonoBehaviour {

	public float gravityStrength = -0.2f;
	
	bool isPressed;
	Transform player;

	void Start () {
		isPressed = false;
		player = GameObject.FindObjectOfType<CharacterCtr> ().transform;
	}

	void OnCollisionEnter (Collision col)
	{
		if (isPressed) return;

		foreach (var contact in col.contacts) {
			if (contact.otherCollider.rigidbody != null) {
				if (Utils.IsAttachedTo (player, contact.otherCollider.transform)) {
					Physics.gravity = new Vector3 (0, gravityStrength, 0);
					foreach (var obj in GameObject.FindObjectsOfType<Rigidbody> ()) {
						obj.AddForce (Random.insideUnitSphere, ForceMode.Impulse);

						// un-freeze player body rotation
						if (obj.name == "Spine1" || obj.name == "Hips") {
							obj.constraints = new RigidbodyConstraints ();
						}
					}
					Debug.Log ("MY FINGER IS ON THE BUTTON");
					isPressed = true;
				}
			}
		}
	}


}
