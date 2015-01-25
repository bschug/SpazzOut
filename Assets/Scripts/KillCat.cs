using UnityEngine;
using System.Collections;

public class KillCat : MonoBehaviour 
{

	public GameObject deathEffectPrefab;

	public float killAccelleration;

	float lastvelocity;

	void Start ()
	{
		lastvelocity = rigidbody.velocity.magnitude;
	}

	void FixedUpdate () 
	{
		float accelleration = Mathf.Abs(rigidbody.velocity.magnitude - lastvelocity);
		// die when exposed to sudden accelleration
		if (accelleration > killAccelleration)
		{
			Debug.Log ("meow");
			Instantiate (deathEffectPrefab, transform.position, Quaternion.identity);
			Destroy (this.gameObject);
		}
		lastvelocity = rigidbody.velocity.magnitude;
	}
}
