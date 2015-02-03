using UnityEngine;
using System.Collections;

public class CharacterLimb : MonoBehaviour
{
	CharacterCtr characterCtr;

	void Awake()
	{
		characterCtr = FindObjectOfType<CharacterCtr>();
	}

	void OnJointBreak(float breakForce) 
	{
		characterCtr.HandleJointBreak(this);
	}
}

