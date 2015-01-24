using UnityEngine;
using System.Collections;

public class HeadGear : MonoBehaviour 
{
	public Vector3 offset;
	public Quaternion rotation;

	FixedJoint joint;
	bool canAttach = true;

	void OnCollisionEnter (Collision collision)
	{
		if (!canAttach) return;

		foreach (var contact in collision.contacts) {
			var other = contact.otherCollider;
			if (other.rigidbody != null && other.name == "Head") {
				AttachToHead (other.rigidbody);
			}
		}
	}

	void AttachToHead (Rigidbody head)
	{
		collider.enabled = false;

		transform.rotation = head.rotation * rotation;
		transform.position = head.position + offset;

		joint = gameObject.AddComponent<FixedJoint>();
		joint.breakForce = 5;
		joint.breakTorque = 10;
		joint.connectedAnchor = head.GetComponent<CharacterJoint>().connectedAnchor;
		joint.connectedBody = head.GetComponent<CharacterJoint>().connectedBody;
	}

	void OnJointBreak (float breakForce)
	{
		joint.connectedBody = null;
		Object.Destroy (joint);
		joint = null;
		collider.enabled = true;
		StartCoroutine (Co_Reactivate ());
	}

	IEnumerator Co_Reactivate ()
	{
		yield return new WaitForSeconds (1);
		canAttach = true;
	}
}
