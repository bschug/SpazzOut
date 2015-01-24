using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {

	public float explosionRadius;

	public float explosionForce;

	void OnTriggerEnter ()
	{
		Debug.Log ("Boom!");
		Collider[] colliders = Physics.OverlapSphere (transform.position, explosionRadius);
		foreach (Collider collider in colliders)
		{
			if (collider.rigidbody != null)
			{
				collider.rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
			}
		}
	}
}
