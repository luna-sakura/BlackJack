using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {

	private GameObject hitButton;
	private GameObject standButton;

	private GameObject restartButton;
	private GameObject goTitleButton;

	// Use this for initialization
	void Start () {
		hitButton = GameObject.Find ("HitButton");
		standButton = GameObject.Find ("StandButton");
		restartButton = GameObject.Find ( "RestartButton" );
		goTitleButton = GameObject.Find ("GoTitleButton");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetHitAndStandButtonActive(bool isActive) {
		hitButton.SetActive(isActive);
		standButton.SetActive(isActive);
	}

	public void SetRestartButtonActive(bool isActive) {
		restartButton.SetActive (isActive);
	}

	public void SetGoTitleButtonActive(bool isActive) {
		goTitleButton.SetActive (isActive);
	}

}
