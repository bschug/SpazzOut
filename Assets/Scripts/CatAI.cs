using UnityEngine;
using System.Collections;

public class CatAI : MonoBehaviour {

	public float directionChangeCooldown;
	float directionChangeTimer = 5;

	Vector3 direction = Vector3.zero;

	public float speed = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		directionChangeTimer -= Time.deltaTime;
		if (directionChangeTimer < 0)
		{
			changeDirection ();
			directionChangeTimer = directionChangeCooldown;
		}
	}

	void changeDirection ()
	{
		direction = new Vector3 (Random.value-0.5f, 0, Random.value-0.5f);
		move ();
	}

	void move ()
	{
		rigidbody.velocity = direction.normalized * speed;
		transform.forward = direction;
	}
}
