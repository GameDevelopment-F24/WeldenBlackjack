using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public int drawNum = 1;
	public bool isDealer = false;

	public int handVal = 0;
	private int money = 1000;

	public List<Card> hand = new List<Card>();
	List<Card> aceList = new List<Card>();

	public void SetDealer(bool isDealer)
	{
		this.isDealer = isDealer;
	}

	public void AddCardToHand(Card card)
	{
		hand.Add(card);
		drawNum++;
	}

	public void FlipHand(){
		if (isDealer){
			hand[0].FlipCard();
			handVal += hand[0].GetCardValue();
		}else{
			foreach (Card card in hand){
				card.FlipCard();
				Debug.Log(card.GetCardValue());
				handVal += card.GetCardValue();
			}
		}
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
		foreach (Card card in hand){
			card.ReturnToDeck();
		}
		hand.Clear();
		handVal = 0;
		drawNum = 1;
		aceList.Clear();
	}
}
