using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SceneRoot : MonoBehaviour
{
	public float length = 50;

	void Start()
	{
		var sceneLoader = GameObject.FindObjectOfType<SceneLoader>();
		sceneLoader.AlignLevel(this);
		sceneLoader.LoadNextScene();
	}
}
