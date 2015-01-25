using UnityEngine;
using System.Collections;

public class CharacterColors : MonoBehaviour {

	public Color[] _colors;

	public static CharacterColors Instance
	{
		get;
		private set;
	}

	void Start()
	{
		Instance = this;
	}
}
