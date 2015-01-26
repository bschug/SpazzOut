using UnityEngine;
using System.Collections;

public class StickyObject : MonoBehaviour 
{
	public bool yourHandsOnly = false;

	Transform player;
	FixedJoint joint;

	void Start ()
	{
		player = GameObject.FindObjectOfType<CharacterCtr>().transform;
	}

	void OnCollisionEnter (Collision collision)
	{
		if (joint != null) return;

		foreach (var contact in collision.contacts) {
			if (Utils.IsAttachedTo (player, contact.otherCollider.transform)) {

				// only stick to the hand if flag is set
				if (yourHandsOnly) {
					if (contact.otherCollider.name != "LeftForeArm" && contact.otherCollider.name != "RightForeArm") {
						break;
					}
				}

				Debug.Log ("ATTACH JOINT");
				joint = this.gameObject.AddComponent<FixedJoint> ();
				joint.anchor = contact.point;
				joint.breakForce = (yourHandsOnly ? 3.5f : 4.5f);
				joint.breakTorque = 10;
				joint.connectedBody = contact.otherCollider.rigidbody;
				break;
			}
		}
	}

	void OnJointBreak (float breakForce)
	{
		Debug.Log ("JOINT BREAK!!! " + breakForce);
		joint.connectedBody = null;
		Object.Destroy (joint);
		joint = null;
	}
}
