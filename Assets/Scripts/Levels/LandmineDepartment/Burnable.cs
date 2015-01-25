using UnityEngine;
using System.Collections;

public class Burnable : MonoBehaviour {

	public bool DestroyWhenBurntOut = false;
	public ParticleSystem fireParticleSystem;

	float stopBurnTime = 0;
	IEnumerator coroutine;

	public void SetOnFire (float duration)
	{
		if (DestroyWhenBurntOut) {
			stopBurnTime = Time.time + 3;
		}
		else {
			stopBurnTime = Time.time + duration;
		}

		if (coroutine != null) {
			StopCoroutine (coroutine);
		}
		coroutine = Co_StopBurningAfter (duration);
		StartCoroutine (coroutine);

		fireParticleSystem.Play();
		Debug.Log (name + " is on fire now");
	}

	public bool IsOnFire
	{
		get
		{
			return Time.time < stopBurnTime;
		}
	}

	public float RemainingBurnTime
	{
		get
		{
			return Mathf.Max (0, stopBurnTime - Time.time);
		}
	}

	IEnumerator Co_StopBurningAfter (float delay)
	{
		yield return new WaitForSeconds (delay);

		Debug.Log (name + " is no longer burning");
		fireParticleSystem.Stop();

		if (DestroyWhenBurntOut) {
			Destroy (gameObject);
		}

		coroutine = null;
	}

	void OnCollisionEnter (Collision collision)
	{
		foreach (var contact in collision.contacts) {
			var burnable = Utils.FindInHierarchy<Burnable>(contact.otherCollider.transform);
			if (burnable != null) {
				if (!this.IsOnFire && burnable.IsOnFire) {
					SetOnFire(burnable.RemainingBurnTime);
				}
			}
		}
	}
}
