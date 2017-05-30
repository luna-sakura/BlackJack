using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour {
	public const int MAX_CARD = 52;

	private CardCreater cardCreater;
	private GameLogic gameLogic;

	public struct PlayingCards {
		public int number;
		public int mark;
		public string cardImg;

		public PlayingCards(int Number, int Mark, string CardImg) {
			number = Number;
			mark = Mark;
			cardImg = CardImg;
		}

	}

	private int deckNumber;
	public int currentPlayerHandsNumber;
	public int currentDealerHandsNumber;

	public int playerAceCount;
	public int dealerAceCount;

	public PlayingCards[] playingCards = new PlayingCards[MAX_CARD];
	public PlayingCards[] playerHands = new PlayingCards[MAX_CARD / 2];
	public PlayingCards[] dealerHands = new PlayingCards[MAX_CARD / 2];

	// Use this for initialization
	void Start () {

		gameLogic = GameObject.Find ("GameManager").GetComponent<GameLogic> ();
		cardCreater = GameObject.Find ("GameManager").GetComponent<CardCreater> ();


		CardsSetting ();

		for (int i = 0; i < MAX_CARD; i++) {
			playingCards [i].number = i%13+1;
			playingCards[i].mark = i/13;

			switch (playingCards [i].mark) {
			case 0:
				playingCards [i].cardImg = "c" + playingCards [i].number.ToString ("00");
				break;

			case 1:
				playingCards [i].cardImg = "d" + playingCards [i].number.ToString ("00");
				break;

			case 2:
				playingCards [i].cardImg = "h" + playingCards [i].number.ToString ("00");
				break;

			case 3:
				playingCards [i].cardImg = "s" + playingCards [i].number.ToString ("00");
				break;
			}

			if (playingCards [i].number > 10) {
				playingCards [i].number = 10;
			}

		}

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void DeckShuffle() {
		for (int i = 0; i < MAX_CARD; i++) {
			int j = Random.Range(i, MAX_CARD - 1);
			PlayingCards tmpCard;

			tmpCard = playingCards [i];
			playingCards [i] = playingCards [j];
			playingCards [j] = tmpCard;

		}
	}

	// カードを引く関数
	public void DrawCard() {

		switch (gameLogic.gamePhase) {
		case GameLogic.GamePhase.GAME_BIGIN:
		case GameLogic.GamePhase.PLAYER_TURN:

			if (playingCards [deckNumber].number == 1) {
				playerAceCount++;
				//Debug.Log ( "draw ace" );
			}

			playerHands [currentPlayerHandsNumber] = playingCards [deckNumber];

			cardCreater.CreatePlayerCard (playerHands [currentPlayerHandsNumber].cardImg, currentPlayerHandsNumber);

			currentPlayerHandsNumber++;
			deckNumber++;
			break;

		case GameLogic.GamePhase.DEALER_TURN:

			if (playingCards [deckNumber].number == 1) {
				dealerAceCount++;
			}

			dealerHands [currentDealerHandsNumber] = playingCards [deckNumber];

			cardCreater.CreateDealerCard (currentDealerHandsNumber);

			currentDealerHandsNumber++;
			deckNumber++;
			break;
		}
			
	}
		
	// スコアを加算する関数
	// 戻り値：引いたカードの点数
	public int AddScore() {

		switch (gameLogic.gamePhase) {
		case GameLogic.GamePhase.GAME_BIGIN:
		case GameLogic.GamePhase.PLAYER_TURN:

			//Debug.Log (playerHands[currentPlayerHandsNumber - 1].cardImg);

			return playerHands [currentPlayerHandsNumber - 1].number;

			break;

		case GameLogic.GamePhase.DEALER_TURN:

			//Debug.Log (dealerHands[currentDealerHandsNumber - 1].cardImg);

			return dealerHands [currentDealerHandsNumber - 1].number;

			break;
		}

		return 0;

	}

	//　カードの初期化用の関数
	public void CardsSetting() {

		deckNumber = 0;
		currentPlayerHandsNumber = 0;
		currentDealerHandsNumber = 0;

		playerAceCount = 0;
		dealerAceCount = 0;
	}

}
