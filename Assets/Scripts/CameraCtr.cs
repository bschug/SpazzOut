using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraCtr : MonoBehaviour 
{
    public Transform Player;

    public float Height = 2;
    public int PositionDamping = 20;

    List<Vector3> lastPlayerPositions = new List<Vector3> ();

	void Start () {
        for (int i = 0; i < PositionDamping; i++) {
            lastPlayerPositions.Add (Player.position);
        }

		RingBuffer<int>.Test ();
	}

    void Update ()
    {
	    
	}
}
