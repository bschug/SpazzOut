using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraCtr : MonoBehaviour 
{
    public Transform Player;

    public float Height = 2;
	public float Radius = 4;
    public int PositionDamping = 20;
	public float SamplingRate = 0.1f;
	public float MoveSpeed = 0.5f;

    List<Vector3> lastPlayerPositions = new List<Vector3> ();

	void Start () {
        for (int i = 0; i < PositionDamping; i++) {
            lastPlayerPositions.Add (Player.position);
        }
		StartCoroutine (Co_CollectPositionSamples ());
	}

	IEnumerator Co_CollectPositionSamples ()
	{
		while (true) {
			yield return new WaitForSeconds (SamplingRate);
			lastPlayerPositions.Add (Player.position);
		}
	}

    void Update ()
    {
		var currentCameraPos = transform.position;
		var desiredCameraPos = ComputeDesiredCameraPos ();
		transform.position = Vector3.Lerp (transform.position, desiredCameraPos, Time.deltaTime * MoveSpeed);
		transform.LookAt (Player);
	}

	Vector3 ComputeDesiredCameraPos ()
	{
		var currentPos = Player.position;
		var lastPos = LastPlayerPositionAverage ();
		var dir = lastPos - currentPos;
		var desiredCameraPos = currentPos + dir.normalized * Radius;
		desiredCameraPos.y = Height;
		return desiredCameraPos;
	}

	Vector3 LastPlayerPositionAverage ()
	{
		Vector3 sum = Vector3.zero;
		for (int i = 0; i < lastPlayerPositions.Count; i++) {
			sum += lastPlayerPositions[i];
		}
		return sum / lastPlayerPositions.Count;
	}
}
