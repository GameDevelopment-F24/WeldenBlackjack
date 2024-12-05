using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
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
	bool roundOver = false;


    void Start()
    	{
        	dealButton.onClick.AddListener(() => DealClicked());
        	hitButton.onClick.AddListener(() => HitClicked());
        	standButton.onClick.AddListener(() => StandClicked());
            betFifty.onClick.AddListener(() => Fifty());
            betHundered.onClick.AddListener(() => Hundered());
            betTwentyFive.onClick.AddListener(() => TwentyFive());
            betAmount.text = "Bet: $" + totalBet.ToString();
		    playerMoney.text = "$" + player.GetMoney().ToString();
    	}
     private void Fifty()
     {
		if (player.GetMoney() >= 50){
         totalBet += 50;
         player.AdjustMoney(-50);
         playerMoney.text = "$" + player.GetMoney().ToString();
         betAmount.text = "Bet: $" + totalBet.ToString();
		}
     }
     private void Hundered()
     {
		if (player.GetMoney() >= 100){
         totalBet += 100;
         player.AdjustMoney(-100);
         playerMoney.text = "$" + player.GetMoney().ToString();
         betAmount.text = "Bets: $" + totalBet.ToString();
		}
     }
     private void TwentyFive()
     {
		if (player.GetMoney() >= 25){
         totalBet += 25;
         player.AdjustMoney(-25);
         playerMoney.text = "$" + player.GetMoney().ToString();
         betAmount.text = "Bet: $" + totalBet.ToString();
		}
     }

    private void DealClicked()
	{
        player.ResetHand();
        dealer.ResetHand();
        winnerText.gameObject.SetActive(false);
        hideCard.gameObject.SetActive(true);
		GameObject.Find("Deck").GetComponent<Deck>().Shuffle();
		player.StartHand();
        playerScore.text = "Hand: " + player.handVal.ToString();
		dealerScore.text = "Hand: " + dealer.StartHandDealer().ToString();
		StartCoroutine(UpdateHandVal());

		IEnumerator UpdateHandVal() {
			yield return new WaitForSeconds(1f);
			playerScore.text = "Hand: " + player.handVal.ToString();
			if (player.handVal >= 21) RoundOver();
		}

        hideCard.GetComponent<Renderer>().enabled = true;
        dealButton.interactable = false;
		betTwentyFive.interactable = false;
		betHundered.interactable = false;
		betFifty.interactable = false;
        hitButton.interactable = true;
        standButton.interactable = true;

        betAmount.text = "Bet: $" + totalBet.ToString();

		playerMoney.text = "$" + player.GetMoney().ToString();

	}
	private void HitClicked()
	{
		if (player.cardIndex <= 10){
			player.GetCard();
			playerScore.text = "Hand: " + player.handVal.ToString();
			
			if (player.handVal > 20) StartCoroutine(DelayRoundOver());

			IEnumerator DelayRoundOver() {
				yield return new WaitForSeconds(1f);
				RoundOver();
			}
		}
	}
	private void StandClicked()
	{
        hideCard.GetComponent<Renderer>().enabled = false;
        dealerScore.text = "Hand: " + dealer.handVal.ToString();
        StartCoroutine(DealerDrawCard());
        IEnumerator DealerDrawCard() {
            while (dealer.handVal < 21) {
                yield return new WaitForSeconds(1f);
                dealer.GetCard();
                dealerScore.text = "Hand: " + dealer.handVal.ToString();
				if (dealer.handVal >= 21) RoundOver();
            }
        }
	}

	void RoundOver()
	{ 
	   bool playerBust = player.handVal > 21;
		bool dealerBust = dealer.handVal > 21;
		bool player21 = player.handVal == 21;
		bool dealer21 = dealer.handVal == 21;
		if(playerBust && dealerBust){
			winnerText.text = "Both players bust!";

			player.AdjustMoney(totalBet/2);
		}else if(playerBust || (!dealerBust && dealer.handVal > player.handVal)){
			roundOver = true;
			winnerText.text = "Dealer wins!";

		}else if(dealerBust || player.handVal > dealer.handVal){
			roundOver = true;
			winnerText.text = "Player wins!";

			player.AdjustMoney(totalBet*2);
		}else if(player.handVal == dealer.handVal){
			roundOver = true;
			winnerText.text = "It's a tie!";

			player.AdjustMoney(totalBet/2);
		}else{
			roundOver = false;
		}if (roundOver){
			hitButton.interactable = false;
			standButton.interactable = false;
			dealButton.interactable = true;
			betTwentyFive.interactable = true;
			betHundered.interactable = true;
			betFifty.interactable = true;
			winnerText.gameObject.SetActive(true);
			dealerScore.gameObject.SetActive(true);
			hideCard.GetComponent<Renderer>().enabled = false;
			betAmount.text = "Bet: $0";
			playerMoney.text = "$" + player.GetMoney().ToString();
			totalBet = 0;
		}

	}
}
