using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

	public const int BLACK_JACK = 21;

	public enum GamePhase
	{
		GAME_BIGIN,
		PLAYER_TURN,
		DEALER_TURN,
		WIN_OR_LOSE,
		RESULT
	}

	public enum PlayerStatus
	{
		HIT,
		STAND,
		BUST,
		NONE
	}

	private enum Judge
	{
		WIN,
		LOSE,
		DRAW,
		NONE
	}

	public GamePhase gamePhase;
	private int playerScore;
	private int dealerScore;
	public static PlayerStatus playerStatus;
	private Judge judge;

	private Cards cards;
	private ButtonManager buttonManager;
	private CardCreater cardCreater;

	private Text judgeText;
	private int loseCount;

	// Use this for initialization
	void Start () {

		cards = GameObject.Find ("GameManager").GetComponent<Cards> ();
		buttonManager = GameObject.Find ("GameManager").GetComponent<ButtonManager> ();
		cardCreater = GameObject.Find ("GameManager").GetComponent<CardCreater> ();
		judgeText = GameObject.Find ("Judge").GetComponent<Text>();

		loseCount = 0;
	
		GameLogicSetting ();

	}
	
	// Update is called once per frame
	void Update () {
		switch (gamePhase) {
		case GamePhase.GAME_BIGIN:
			buttonManager.SetRestartButtonActive (false);
			buttonManager.SetGoTitleButtonActive (false);
			judgeText.text = "";

			cards.DeckShuffle ();
		
			cards.DrawCard ();
			playerScore += cards.AddScore ();

			buttonManager.SetHitAndStandButtonActive (true);

			gamePhase = GamePhase.DEALER_TURN;

			break;

		case GamePhase.PLAYER_TURN:
			
			if (playerStatus == PlayerStatus.HIT) {
				cards.DrawCard ();
				playerScore += cards.AddScore ();

			}

			if (playerScore > BLACK_JACK) {
				playerStatus = PlayerStatus.BUST;
			}

			if (playerStatus == PlayerStatus.HIT || playerStatus == PlayerStatus.STAND) {
				gamePhase = GamePhase.DEALER_TURN;
			}

			if (playerStatus == PlayerStatus.BUST) {
				gamePhase = GamePhase.WIN_OR_LOSE;
			}
			break;

		case GamePhase.DEALER_TURN:
			if (dealerScore < 17) {
				//Debug.Log ("dealer draw");
				cards.DrawCard ();
				dealerScore += cards.AddScore ();

				//Debug.Log ("dealer score:" + dealerScore);
			}

			if (dealerScore > BLACK_JACK || playerStatus == PlayerStatus.STAND) {
				gamePhase = GamePhase.WIN_OR_LOSE;
			}

			if (playerStatus == PlayerStatus.HIT && gamePhase != GamePhase.WIN_OR_LOSE) {
				gamePhase = GamePhase.PLAYER_TURN;
				playerStatus = PlayerStatus.NONE;
			}

			break;

		case GamePhase.WIN_OR_LOSE:

			if (cards.playerAceCount > 0) {
				playerScore = Recalculation (playerScore, cards.playerAceCount);
				//Debug.Log ( playerScore );
			} else if (cards.dealerAceCount > 0) {
				dealerScore = Recalculation (dealerScore, cards.dealerAceCount);
			}

			for (int i = 0; i < cards.currentDealerHandsNumber; i++) {
				cardCreater.OpenDealerCards ( cards.dealerHands[i].cardImg, i );
			}

			buttonManager.SetHitAndStandButtonActive (false);

			int score1 = BLACK_JACK - playerScore;
			int score2 = BLACK_JACK - dealerScore;

			if (dealerScore > BLACK_JACK) {
				judge = Judge.WIN;
			} else if (playerStatus == PlayerStatus.BUST) {
				judge = Judge.LOSE;
			} else if (playerScore == BLACK_JACK && dealerScore == BLACK_JACK) {
				judge = Judge.LOSE;
			} else if (score1 == score2) {
				judge = Judge.DRAW;
			} else if (score1 > score2) {
				judge = Judge.LOSE;
			} else if (score1 < score2) {
				judge = Judge.WIN;
			}
				
			if (judge != Judge.NONE) {
				ChangeJudgeText ();
				gamePhase = GamePhase.RESULT;
			}
			break;

		case GamePhase.RESULT:
			if (loseCount < 3) {
				buttonManager.SetRestartButtonActive (true);
			} else {
				buttonManager.SetGoTitleButtonActive (true);
			}

			break;
		}
	}

	// エースの処理をする関数
	int Recalculation( int score, int aceCount ) {
		int tmpScore = score;
		int notAceScore = score - aceCount;

		if (notAceScore + (aceCount - 1) <= 10) {
			tmpScore = notAceScore + (aceCount - 1) + 11;
		}

		return tmpScore;

	}

	// 初期化用の関数
	private void GameLogicSetting() {
		gamePhase = GamePhase.GAME_BIGIN;
		playerStatus = PlayerStatus.HIT;
		judge = Judge.NONE;

		playerScore = 0;
		dealerScore = 0;

	}

	// 再戦時の処理をする関数
	public void GameRestart() {
		//Debug.Log ("Restart");
		GameLogicSetting ();
		cards.CardsSetting ();
		cardCreater.DestroyCards ();
	}

	//勝敗用のテキストを変更する関数
	public void ChangeJudgeText() {
		switch (judge) {
		case Judge.WIN:
			judgeText.text = "WIN";
			break;

		case Judge.LOSE:
			loseCount++;
			judgeText.text = "LOSE";
			break;

		case Judge.DRAW:
			judgeText.text = "DRAW";
			break;
		}
	}

}
