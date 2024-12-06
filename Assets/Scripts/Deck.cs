using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;


public class Deck : MonoBehaviour
{
	public Card cardBack;
	public Sprite whiteCardBack;
	public Sprite blackCardBack;

	public Sprite[] cards;
	int[] cardValues = new int[53];
	int currentIndex = 1;


	public Vector2 deckPos;

	void Start()
	{
		SetCardBack();
		GetCardValues();
		Shuffle();
	}

	public void SetCardBack()
	{
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		if (CardType.GetCardType() == CardType.CardColor.White)
		{
			cardBack.ChangeBackSprite(whiteCardBack);
			cards[0] = whiteCardBack;
			sr.sprite = whiteCardBack;
		}
		else if (CardType.GetCardType() == CardType.CardColor.Black)
		{
			cardBack.ChangeBackSprite(blackCardBack);
			cards[0] = blackCardBack;
			sr.sprite = blackCardBack;
		}
	}

	void GetCardValues()
	{
		int num = 0;
		for (int i =0; i < cards.Length; i++){
			num = i;
			num %=13;
			if (num > 10 || num == 0){
				num = 10;
			}
			cardValues[i] = num++;
		}
	}

	public void Shuffle()
	{
		for(int i = cards.Length - 2; i > 1; i--)
		{
			int r = Mathf.FloorToInt(Random.Range(1.0f, i + 1));
			Sprite temp = cards[i];
			int valTemp = cardValues[i];
			cards[i] = cards[r];
			cardValues[i] = cardValues[r];
			cards[r] = temp;
			cardValues[r] = valTemp;
		}
	}
	public Vector2 CalculateHandPostion(Player player){
	float x = -1.6f + (player.drawNum * 1.1f);
		float y = 0;
		if (!player.isDealer){
			if (player.drawNum <= 2){
				y = -3f;
			}else{
				y = -2.5f;
			}
		}else if (player.isDealer){	
			if( player.drawNum <= 2){
				y = 3f;
			}else{
				y = 2.5f;
			}
		}
		return new Vector2(x, y);
	}

	public Card DealCard(Player player)
	{
		Vector2 pos = CalculateHandPostion(player);
		Card card = Instantiate(cardBack, pos, Quaternion.identity);
		card.ChangeBackSprite(cards[0]); //seems redundant
		card.SetCardValue(cardValues[currentIndex]);
		// Debug.Log(cardValues[currentIndex]);
		card.ChangeFrontSprite(cards[currentIndex]);
		card.Deal(pos); 
		player.AddCardToHand(card);
		currentIndex++;
		return card;
	}
	public Sprite CardBack()
	{
		return cards[0];
	}
}
