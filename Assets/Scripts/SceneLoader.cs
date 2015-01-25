using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
	public List<string> scenes = new List<string>();

	void Start ()
	{
		StartCoroutine (Co_LoadScenes());
	}

	IEnumerator Co_LoadScenes ()
	{
		float nextStartPos = 0;

		foreach (var scene in scenes) {
			var op = Application.LoadLevelAdditiveAsync (scene);
			if (op == null) {
				Debug.LogError ("Could not find Scene '" + scene + "'");
				continue;
			}

			while (!op.isDone) {
				yield return new WaitForEndOfFrame ();
			}

			var rootObj = GameObject.Find (scene);
			var root = rootObj.GetComponent<SceneRoot> ();

			rootObj.transform.Translate (0, 0, nextStartPos);
			nextStartPos += root.length;
		}
	}
}
