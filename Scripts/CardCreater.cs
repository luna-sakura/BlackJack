using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCreater : MonoBehaviour {

	private GameObject playerCardsZone;
	private GameObject dealerCardsZone;

	// Use this for initialization
	void Start () {
		playerCardsZone = GameObject.Find ("PlayerCardsZone");
		dealerCardsZone = GameObject.Find ("DealerCardsZone");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// プレイヤー側のカードゾーンにカードを生成
	public void CreatePlayerCard(string imgName, int cardCount) {
		GameObject prefab = (GameObject)Resources.Load("Prefab/Card");

		GameObject obj = Instantiate (prefab, playerCardsZone.transform);
		obj.transform.localScale = new Vector2 (35f, 35f);
		obj.AddComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Trump/" + imgName);

		obj.name = "playerCard" + cardCount.ToString ();
	}

	// ディーラー側のカードゾーンにカードを生成(裏向き)
	public void CreateDealerCard(int cardCount) {
		GameObject prefab = (GameObject)Resources.Load("Prefab/Card");

		GameObject obj = Instantiate (prefab, dealerCardsZone.transform);
		obj.transform.localScale = new Vector2 (35f, 35f);
		obj.AddComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Trump/z01");

		obj.name = "dealerCard" + cardCount.ToString();
	}

	// ディーラーのカードを表向きにする
	public void OpenDealerCards( string imgName, int cardNum ) {
		GameObject obj = GameObject.Find ( "dealerCard" + cardNum.ToString() );

		obj.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Trump/" + imgName);

	}

	// 生成したカードを破棄する関数
	public void DestroyCards() {
		for (int i = 0; i < playerCardsZone.transform.childCount; i++) {
			GameObject.Destroy( playerCardsZone.transform.GetChild( i ).gameObject );
		}

		for (int i = 0; i < dealerCardsZone.transform.childCount; i++) {
			GameObject.Destroy( dealerCardsZone.transform.GetChild( i ).gameObject );
		}
	}

}
