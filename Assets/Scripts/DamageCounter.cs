using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DamageCounter : MonoBehaviour
{
	public static float TotalDamage;

	public float Value = 10;
	public float Sturdiness = 10;

	bool destroyed = false;

	void OnCollisionEnter (Collision collision)
	{
		if (destroyed) return;

		if (collision.relativeVelocity.sqrMagnitude > Sturdiness) {
			TotalDamage += Value;
			destroyed = true;
			Debug.Log ("Damage: " + Value + "  -->  Total: " + TotalDamage);
		}
	}
}
