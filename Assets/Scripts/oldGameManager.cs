using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;

public class OldGameManager : MonoBehaviour
{
	public Button dealButton;
	public Button hitButton;
	public Button standButton;

	public Button betFifty;
	public Button betHundered;
	public Button betTwentyFive;

	public Player player;
	public Player dealer;

	public TextMeshProUGUI playerScore;
	public TextMeshProUGUI dealerScore;
	public TextMeshProUGUI playerMoney;
	public TextMeshProUGUI betAmount;
	public TextMeshProUGUI winnerText;

	public GameObject hideCard;

	int totalBet;
	int pot = 0;
	bool roundOver = false;

    	void Start()
    	{
        	dealButton.onClick.AddListener(() => DealClicked());
        	hitButton.onClick.AddListener(() => HitClicked());
        	standButton.onClick.AddListener(() => HitDealer());
            betFifty.onClick.AddListener(() => Fifty());
            betHundered.onClick.AddListener(() => Hundered());
            betTwentyFive.onClick.AddListener(() => TwentyFive());
            betAmount.text = "$" + pot.ToString();
		    playerMoney.text = "$" + player.GetMoney().ToString();
		
    	}
     private void Fifty()
     {
         totalBet += 50;
         player.AdjustMoney(-totalBet);
         playerMoney.text = "$" + player.GetMoney().ToString();
         pot += (totalBet*2);
         betAmount.text = "Bets: $" + totalBet.ToString();
     }
     private void Hundered()
     {
         totalBet += 100;
         player.AdjustMoney(-totalBet);
         playerMoney.text = "$" + player.GetMoney().ToString();
         pot += (totalBet*2);
         betAmount.text = "Bets: $" + totalBet.ToString();
     }
     private void TwentyFive()
     {
         totalBet += 25;
         player.AdjustMoney(-totalBet);
         playerMoney.text = "$" + player.GetMoney().ToString();
         pot += (totalBet*2);
         betAmount.text = "Bets: $" + totalBet.ToString();
     }
	private void DealClicked()
	{
		player.ResetHand();
		dealer.ResetHand();
		dealerScore.gameObject.SetActive(false);
		winnerText.gameObject.SetActive(false);

		hideCard.gameObject.SetActive(true);
		GameObject.Find("Deck").GetComponent<Deck>().Shuffle();
		player.StartHand();
		dealer.StartHand();
		dealerScore.text = "Hand: " + dealer.handVal.ToString();
		playerScore.text = "Hand: " + player.handVal.ToString();

		hideCard.GetComponent<Renderer>().enabled = true;
		dealButton.interactable = false;
		hitButton.interactable = true;
		standButton.interactable = true;
		// pot = 40;
		betAmount.text = "$" + pot.ToString();
		player.AdjustMoney(-20);
		playerMoney.text = "$" + player.GetMoney().ToString();
	}
	private void HitClicked()
	{
		if (player.cardIndex <= 10){
			player.GetCard();
			playerScore.text = "Hand: " + player.handVal.ToString();
			if (player.handVal > 20) RoundOver();
		}
	}
	private void HitDealer()
	{
		// while (dealer.handVal < 17 && dealer.cardIndex < 10)
		// {
			dealer.GetCard();
			dealerScore.text = "Hand: " + dealer.handVal.ToString();
			if (dealer.handVal > 20) RoundOver();
		// }
	}

	void RoundOver()
	{
	   bool playerBust = player.handVal > 21;
		bool dealerBust = dealer.handVal > 21;
		bool player21 = player.handVal == 21;
		bool dealer21 = dealer.handVal == 21;
		if(playerBust && dealerBust){
			winnerText.text = "Both players bust!";
			player.AdjustMoney(pot/2);
		}else if(playerBust || (!dealerBust && dealer.handVal > player.handVal)){
			roundOver = true;
			winnerText.text = "Dealer wins!";
		}else if(dealerBust || player.handVal > dealer.handVal){
			roundOver = true;
			winnerText.text = "Player wins!";
			player.AdjustMoney(pot);
		}else if(player.handVal == dealer.handVal){
			roundOver = true;
			winnerText.text = "It's a tie!";
			player.AdjustMoney(pot/2);
		}else{
			roundOver = false;
		}if (roundOver){
			hitButton.interactable = false;
			standButton.interactable = false;
			dealButton.interactable = true;
			winnerText.gameObject.SetActive(true);
			dealerScore.gameObject.SetActive(true);
			hideCard.GetComponent<Renderer>().enabled = false;
			playerMoney.text = "$" + player.GetMoney().ToString();
			totalBet = 0;
		}

	}
}
