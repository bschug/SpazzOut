using UnityEngine;
using System.Collections;

public interface IGroundTouchable
{
	bool TouchesGround{get; set;}
}

public class LimbGroundCollision : MonoBehaviour {

	public CharacterCtr characterCtr;

	void OnCollisionEnter (Collision collision)
	{
		foreach (var contact in collision.contacts) {
			var components = contact.otherCollider.gameObject.GetComponents(typeof(IGroundTouchable));
			foreach(var component in components)
				((IGroundTouchable)(component)).TouchesGround = true;
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
			var components = contact.otherCollider.gameObject.GetComponents(typeof(IGroundTouchable));
			foreach(var component in components)
				((IGroundTouchable)(component)).TouchesGround = false;
			var obj = contact.otherCollider.rigidbody;
			var i = characterCtr.limbs.IndexOf (obj);
			if (i > 0) {
				characterCtr.limbOnGround[i] = false;
			}
		}
	}

}
