using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JustinUI : MonoBehaviour {

	public CharacterCtr _insertJustinHere;
	public Text			_timeText;
	public Text			_distanceText;

	// Update is called once per frame
	void Update () {
		_distanceText.text = _insertJustinHere.Distance.ToString("0")+" m";
		float passedTime = _insertJustinHere.PassedTime;
		int millisecs = Mathf.FloorToInt((passedTime*10.0f)%10);
		int seconds = Mathf.FloorToInt(passedTime%60);
		passedTime-=seconds;
		int minutes = Mathf.FloorToInt(passedTime/60.0f);
		_timeText.text = minutes.ToString("D2")+":"+seconds.ToString("D2")+"."+millisecs;
	}
}
