//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.18444
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;

public class DistanceCounter : MonoBehaviour
{

	float passedTime = 0.0f;
	Vector3 oldPosition;
	float distance = 0.0f;

	
	public float Distance
	{
		get{return distance;}
	}
	
	public float PassedTime
	{
		get{ return passedTime;}
	}


	void Start()
	{
		oldPosition = transform.position;
	}

	void Update() 
	{
		distance += Vector3.Distance(oldPosition, transform.position);
		oldPosition = transform.position;
		passedTime += Time.deltaTime;
	}
}

