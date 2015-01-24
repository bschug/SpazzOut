using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterCtr : MonoBehaviour {

    public Rigidbody head;
	public Rigidbody hip;
    public float desiredHeightOfHead = 0.4f;
    public float headForce = 2;
	public float hipForce = 5.75f;
	public float limbForce = 2;

    public List<Rigidbody> limbs = new List<Rigidbody> ();
	public List<Rigidbody> feet = new List<Rigidbody> ();
	public List<LimbCtr> limbControls = new List<LimbCtr>();
	public List<bool> limbOnGround = new List<bool> ();
    public float limbSensitivity = 45;

    Vector3 lastMousePos;

    void KeepHeadFloating ()
    {
		//var actualHeightOfHead = head.transform.position.y;
		//if (actualHeightOfHead < desiredHeightOfHead)
		//{
		//	var dst = actualHeightOfHead - desiredHeightOfHead;
		//	head.AddForce (0, dst * dst * force * Physics.gravity.magnitude, 0, ForceMode.Force);
		//}
		head.AddForce (0, Physics.gravity.magnitude * headForce, 0, ForceMode.Force);
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
				if (feet.Contains(limbs[i]) && limbOnGround[i]) {
					hip.AddForce (Vector3.up * hipForce * Physics.gravity.magnitude);
					limbs[i].AddForce (Vector3.down * hipForce * Physics.gravity.magnitude * 0.5f);
				}
            }
            if (Input.GetKey (limbDownKeys[i]))
            {
				limbs[i].AddRelativeTorque (new Vector3 (-limbSensitivity, 0, 0));
				if (feet.Contains(limbs[i]) && limbOnGround[i]) {
					hip.AddForce (Vector3.up * hipForce * Physics.gravity.magnitude);
					limbs[i].AddForce (Vector3.down * hipForce * Physics.gravity.magnitude * 0.5f);
				}
			}
        }
    }

    void Start ()
    {
        lastMousePos = Input.mousePosition;
		for (int i = 0; i < limbs.Count; i++) {
			limbOnGround.Add (false);
		}
    }

	void Update ()
    {
        ReadInputs ();
        KeepHeadFloating ();
	}
}
