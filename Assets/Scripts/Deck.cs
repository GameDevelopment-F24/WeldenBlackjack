using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;


public class Deck : MonoBehaviour
{
	public Sprite[] cards;
	int[] cardValues = new int[53];
	int currentIndex = 1;

	void Start()
	{
		GetCardValues();
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
	public int DealCard(Card card)
	{
		card.SetCardValue(cardValues[currentIndex]);
		card.ChangeSprite(cards[currentIndex]);
		currentIndex++;
		return card.GetCardValue();
	}
	public Sprite CardBack()
	{
		return cards[0];
	}
}
