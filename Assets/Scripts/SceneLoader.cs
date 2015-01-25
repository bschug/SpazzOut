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

			SceneRoot root = null;
			foreach (var sceneRoot in GameObject.FindObjectsOfType<SceneRoot>()) {
				if (sceneRoot.name == scene) {
					root = sceneRoot;
					break;
				}
			}

			if (root == null) {
				Debug.LogError ("Scene '" + scene + "' has no SceneRoot script on its root object!");
			}

			root.transform.Translate (0, 0, nextStartPos);
			nextStartPos += root.length;
		}


		// Quest Marker
		/*var bigRedButton = GameObject.FindObjectOfType<GravityButton> ();
		var questMarker = GameObject.FindObjectOfType<QuestPointer> ();
		questMarker.questTarget = bigRedButton.transform;*/
	}
}
