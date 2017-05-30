using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpScene : MonoBehaviour {

	private string SceneName;

	// Use this for initialization
	void Start () {
		SceneName = SceneManager.GetActiveScene ().name;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void nextSceneJump() {
		if (SceneName == "Title") {
			SceneManager.LoadScene ("Scenes/Main");
		} else if (SceneName == "Main") {
			SceneManager.LoadScene ("Scenes/Title");
		}
	}

}
