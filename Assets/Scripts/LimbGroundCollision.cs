using UnityEngine;
using System.Collections;

public class LimbGroundCollision : MonoBehaviour {

	public CharacterCtr characterCtr;

	void OnCollisionEnter (Collision collision)
	{
		foreach (var contact in collision.contacts) {
			var obj = contact.otherCollider.rigidbody;
			var i = characterCtr.limbs.IndexOf (obj);
			if (i > 0) {
				characterCtr.limbOnGround[i] = true;
			}
		}
	}

	void OnCollisionExit (Collision collision)
	{
		foreach (var contact in collision.contacts) {
			var obj = contact.otherCollider.rigidbody;
			var i = characterCtr.limbs.IndexOf (obj);
			if (i > 0) {
				characterCtr.limbOnGround[i] = false;
			}
		}
	}
}
