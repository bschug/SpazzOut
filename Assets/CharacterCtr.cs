using UnityEngine;
using System.Collections;

public class CharacterCtr : MonoBehaviour {

    public Rigidbody head;
    public float desiredHeightOfHead;
    public float force;

    public Rigidbody rightUpperLeg;

    public Rigidbody rightLowerLeg;
    public Rigidbody leftUpperLeg;
    public Rigidbody leftLowerLeg;

    void KeepHeadFloating ()
    {
        var actualHeightOfHead = head.transform.position.y;
        if (actualHeightOfHead < desiredHeightOfHead)
        {
            var dst = actualHeightOfHead - desiredHeightOfHead;
            head.AddForce (0, dst * dst * force, 0, ForceMode.Force);
        }
    }

	void Update () {
        KeepHeadFloating ();
	}
}
