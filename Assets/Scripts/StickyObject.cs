using UnityEngine;
using System.Collections;

public class StickyObject : MonoBehaviour 
{
	public Transform player;
	FixedJoint joint;

	void OnCollisionEnter (Collision collision)
	{
		if (joint != null) return;

		foreach (var contact in collision.contacts) {
			if (IsAttachedTo (player, contact.otherCollider.transform)) {
				Debug.Log ("ATTACH JOINT");
				joint = this.gameObject.AddComponent<FixedJoint> ();
				joint.anchor = contact.point;
				joint.breakForce = 5;
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

	bool IsAttachedTo (Transform parent, Transform obj)
	{
		if (obj.parent == null) return false;
		if (obj.parent == parent) return true;
		return IsAttachedTo (parent, obj.parent);
	}
}
