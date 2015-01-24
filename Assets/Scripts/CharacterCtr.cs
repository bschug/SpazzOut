using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterCtr : MonoBehaviour {

    public Rigidbody head;
    public float desiredHeightOfHead = 0.4f;
    public float force = 200;

    public List<Rigidbody> limbs = new List<Rigidbody> ();
    public float limbSensitivity = 45;

    Vector3 lastMousePos;

    void KeepHeadFloating ()
    {
        var actualHeightOfHead = head.transform.position.y;
        if (actualHeightOfHead < desiredHeightOfHead)
        {
            var dst = actualHeightOfHead - desiredHeightOfHead;
            head.AddForce (0, dst * dst * force * Physics.gravity.magnitude, 0, ForceMode.Force);
        }
    }

    void ReadInputs ()
    {
        var mouseDelta = lastMousePos - Input.mousePosition;
        lastMousePos = Input.mousePosition;

        var limbUpKeys = new List<KeyCode> { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R };
        var limbDownKeys = new List<KeyCode> { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F };

        for (int i=0; i < limbUpKeys.Count; i++)
        {
            if (Input.GetKey (limbUpKeys[i]))
            {
                limbs[i].AddRelativeTorque (new Vector3 (limbSensitivity, 0, 0));
            }
            if (Input.GetKey (limbDownKeys[i]))
            {
                limbs[i].AddRelativeTorque (new Vector3 (-limbSensitivity, 0, 0));
            }
        }
    }

    void Start ()
    {
        lastMousePos = Input.mousePosition;
    }

    void Update ()
    {
        ReadInputs ();
        KeepHeadFloating ();
	}
}
