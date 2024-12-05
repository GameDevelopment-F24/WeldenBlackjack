using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	// public Card card;
	public Deck deck;

	public int handVal = 0;
	private int money = 1000;

	public Card[] hand;

	public int cardIndex = 1;

	List<Card> aceList = new List<Card>();

	public void StartHand()
	{
		int c1 = GetCard();
		StartCoroutine(DelaySecondCard());

		IEnumerator DelaySecondCard() {
			yield return new WaitForSeconds(1f);
			int c2 = GetCard();
		}
	}
	public int StartHandDealer()
	{
		int c1 = GetCard();
		int c2 = GetCard();
		return c1;
	}

	
	public int GetCard()
	{
		// Debug.Log(cardIndex);
		int cardVal = deck.DealCard(hand[cardIndex].GetComponent<Card>());
		hand[cardIndex].GetComponent<Renderer>().enabled = true;
		// Debug.Log("Flipped card: " + hand[cardIndex].GetComponent<Card>().GetCardName());
		handVal += cardVal;
		if (cardVal == 1)
		{
			aceList.Add(hand[cardIndex].GetComponent<Card>());
		}
		AceCheck();
		cardIndex++;
		return handVal;
	}
	public void AceCheck()
	{
		foreach (Card ace in aceList){
			if (handVal + 10 <= 21 && ace.GetCardValue() == 1)
			{
				handVal += 10;
				ace.SetCardValue(11);
			} else if (handVal > 21 && ace.GetCardValue() == 11){
				handVal -= 10;
				ace.SetCardValue(1);
			}
			
		}
	}
	public void AdjustMoney(int amount)
	{
		money += amount;
	}
	public int GetMoney()
	{
		return money;
	}
	public void ResetHand()
	{
		for(int i = 0; i < hand.Length; i++)
		{
			// hand[i].GetComponent<Card>().ResetCard();
			hand[i].ResetCard();
			hand[i].GetComponent<Renderer>().enabled = false;
		}
		handVal = 0;
		cardIndex = 0;
		aceList.Clear();
	}
}
