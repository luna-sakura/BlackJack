using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButton : MonoBehaviour {

	private GameLogic gameLogic;

	public enum ButtonType
	{
		HIT,
		STAND,
		RESTART
	}

	[SerializeField] private ButtonType buttonType;

	// Use this for initialization
	void Start () {
		gameLogic = GameObject.Find ("GameManager").GetComponent<GameLogic> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClickButton() {
		switch (buttonType) {
		case ButtonType.HIT:
			if (gameLogic.gamePhase == GameLogic.GamePhase.PLAYER_TURN) {
				GameLogic.playerStatus = GameLogic.PlayerStatus.HIT;
			}

			break;

		case ButtonType.STAND:
			if (gameLogic.gamePhase == GameLogic.GamePhase.PLAYER_TURN) {
				GameLogic.playerStatus = GameLogic.PlayerStatus.STAND;
			}

			break;

		case ButtonType.RESTART:
			if (gameLogic.gamePhase == GameLogic.GamePhase.RESULT) {
				gameLogic.GameRestart ();
			}

			break;
		}
	}

}
