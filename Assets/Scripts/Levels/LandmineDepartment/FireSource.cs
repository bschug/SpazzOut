using UnityEngine;
using System.Collections;

public class FireSource : MonoBehaviour {

	public float burnDuration = 10;

	Transform player;

	void Start () {
		player = GameObject.FindObjectOfType<CharacterCtr> ().transform;
	}

	void OnCollisionEnter (Collision collision)
	{
		foreach (var contact in collision.contacts) {
			var burnable = Utils.FindInHierarchy<Burnable> (contact.otherCollider.transform);
			if (burnable != null) {
				burnable.SetOnFire (burnDuration);
			}
		}
	}
}
