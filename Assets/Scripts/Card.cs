using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
	public int cardValue = 0;

	public int GetCardValue(){
		return cardValue;
	}
	public void SetCardValue(int value){
		cardValue = value;
	}
	public string GetCardName(){
		return GetComponent<SpriteRenderer>().sprite.name;
	}
	public void ChangeSprite(Sprite newSprite){
		GetComponent<SpriteRenderer>().sprite = newSprite;
	}
	public void ResetCard(){
		Sprite CardBack = GameObject.Find("Deck").GetComponent<Deck>().CardBack();
		gameObject.GetComponent<SpriteRenderer>().sprite = CardBack;
	}
}
