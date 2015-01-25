using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {

	public float explosionRadius;
	public float explosionForce;

	// objects need to go at least this fast to trigger the explosion
	public float necessaryVelocity;

	Transform player;

	void Start ()
	{
		player = GameObject.FindObjectOfType<CharacterCtr>().transform;
	}

	void OnCollisionEnter (Collision collision)
	{
		foreach (var contact in collision.contacts) {
			if (Utils.IsAttachedTo (player, contact.otherCollider.transform) || collision.relativeVelocity.magnitude > necessaryVelocity) {
				Explode ();
				break;
			}
		}
	}

	void Explode() 
	{
		Collider[] colliders = Physics.OverlapSphere (transform.position, explosionRadius);
		foreach (Collider collider in colliders)
		{
			if (collider.rigidbody != null)
			{
				collider.rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
			}
			Destroy (this);
		}
	}
}
