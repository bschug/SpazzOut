using UnityEngine;
using UnityEditor;
using System.Collections;

public class PlayMode : MonoBehaviour {

	static string oldScene;
	
	[MenuItem("Custom/Playmode")]
	static void StartPlayMode()
	{
		EditorApplication.isPaused = false;
		EditorApplication.isPlaying = false;
		oldScene = EditorApplication.currentScene;
		bool couldOpenScene = EditorApplication.OpenScene("Assets/Scenes/MainScene.unity");
		EditorApplication.playmodeStateChanged += OnPlayModeStateChanged;
		EditorApplication.isPlaying = true;

	}

	static void OnPlayModeStateChanged()
	{
		if(EditorApplication.isPlaying==false)
		{
			EditorApplication.playmodeStateChanged -= OnPlayModeStateChanged;
			EditorApplication.OpenScene(oldScene);
			oldScene = null;

		}
	}
}
