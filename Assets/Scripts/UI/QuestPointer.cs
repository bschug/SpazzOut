using UnityEngine;
using System.Collections;

public class QuestPointer : MonoBehaviour {

	public float height;
	public Transform player;
	public Transform questTarget;

	void Update () {
		var pos = player.position;
		pos.y = height;
		transform.position = pos;

		transform.LookAt (questTarget);
		transform.Rotate (Vector3.up, Mathf.PI * 0.5f);
	}
}
