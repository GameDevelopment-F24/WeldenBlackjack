using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Button dealButton;
    public Button hitButton;
    public Button standButton;
    public Button ExitButton;
    public Button betFifty;
    public Button betHundered;
    public Button betTwentyFive;
    public TextMeshProUGUI playerScore;
    public TextMeshProUGUI dealerScore;
    public TextMeshProUGUI playerMoney;
    public TextMeshProUGUI betAmount;
    public TextMeshProUGUI winnerText;

    public Player player;
    public Player dealer;
    public Deck deck;

    int totalBet;
    bool roundOver = false;

    void Start()
    {
        InitButtons();
        InitGameObjects();
    }

    private void InitGameObjects()
    {
        deck = Instantiate(deck, new Vector2(-4.6f, 3.5f), Quaternion.identity);
        player = Instantiate(player, new Vector2(0, 0), Quaternion.identity);
        dealer = Instantiate(dealer, new Vector2(0, 0), Quaternion.identity);
        dealer.SetDealer(true);
    }

    private void InitButtons()
    {
        dealButton.onClick.AddListener(() => DealClicked());
        hitButton.onClick.AddListener(() => HitClicked());
        standButton.onClick.AddListener(() => StandClicked());
        betFifty.onClick.AddListener(() => Fifty());
        betHundered.onClick.AddListener(() => Hundered());
        betTwentyFive.onClick.AddListener(() => TwentyFive());
        ExitButton.onClick.AddListener(() => Exit());
        betAmount.text = "Bet: $" + totalBet.ToString();
        playerMoney.text = "$" + player.GetMoney().ToString();
    }

    private void Exit()
    {
        SceneManager.LoadScene("Menu");
    }

    private void Fifty()
    {
        if (player.GetMoney() >= 50)
        {
            totalBet += 50;
            player.AdjustMoney(-50);
            playerMoney.text = "$" + player.GetMoney().ToString();
            betAmount.text = "Bet: $" + totalBet.ToString();
        }
    }

    private void Hundered()
    {
        if (player.GetMoney() >= 100)
        {
            totalBet += 100;
            player.AdjustMoney(-100);
            playerMoney.text = "$" + player.GetMoney().ToString();
            betAmount.text = "Bets: $" + totalBet.ToString();
        }
    }

    private void TwentyFive()
    {
        if (player.GetMoney() >= 25)
        {
            totalBet += 25;
            player.AdjustMoney(-25);
            playerMoney.text = "$" + player.GetMoney().ToString();
            betAmount.text = "Bet: $" + totalBet.ToString();
        }
    }

    IEnumerator DealCards()
    {
        deck.DealCard(player);
        yield return new WaitForSeconds(0.5f);
        player.hand[0].FlipCard();
        player.handVal += player.hand[0].GetCardValue();
        playerScore.text = "Hand: " + player.handVal.ToString();
        yield return new WaitForSeconds(0.5f);
        deck.DealCard(dealer);
        yield return new WaitForSeconds(0.5f);
        dealer.hand[0].FlipCard();
        dealer.handVal += dealer.hand[0].GetCardValue();
        dealerScore.text = "Hand: " + dealer.handVal.ToString();
        yield return new WaitForSeconds(0.5f);
        deck.DealCard(player);
        yield return new WaitForSeconds(0.5f);
        player.hand[1].FlipCard();
        player.handVal += player.hand[1].GetCardValue();
        playerScore.text = "Hand: " + player.handVal.ToString();
        yield return new WaitForSeconds(0.5f);
        deck.DealCard(dealer);
    }

    private void DealClicked()
    {
        winnerText.gameObject.SetActive(false);
        StartCoroutine(DealCards());
        dealButton.interactable = false;
        betTwentyFive.interactable = false;
        betHundered.interactable = false;
        betFifty.interactable = false;
        hitButton.interactable = true;
        standButton.interactable = true;
        betAmount.text = "Bet: $" + totalBet.ToString();
        playerMoney.text = "$" + player.GetMoney().ToString();
    }

    IEnumerator HitCard(Player plr, Card card)
    {
        yield return new WaitForSeconds(1f);
        if (!card.faceUp)
        {
            card.FlipCard();
        }
        plr.handVal += card.GetCardValue();
        if (plr.isDealer)
        {
            dealerScore.text = "Hand: " + plr.handVal.ToString();
            if (dealer.handVal > player.handVal)
            {
                RoundOver();
            }
        }
        else
        {
            playerScore.text = "Hand: " + plr.handVal.ToString();
        }
        if (plr.handVal > 20)
        {
            if (firstStand)
            {
                dealer.hand[1].FlipCard();
                dealer.handVal += dealer.hand[1].GetCardValue();
                dealerScore.text = "Hand: " + dealer.handVal.ToString();
            }
            RoundOver();
        }
    }

    private void HitClicked()
    {
        if (player.hand.Count <= 10)
        {
            Card card = deck.DealCard(player);
            StartCoroutine(HitCard(player, card));
        }
    }

    IEnumerator HitCardDealer(Player plr)
    {
        if (dealer.handVal > player.handVal)
        {
            RoundOver();
        }
        else
        {
            while (dealer.handVal < 21 && dealer.handVal <= player.handVal)
            {
                yield return new WaitForSeconds(1f);
                Card card = deck.DealCard(plr);
                yield return new WaitForSeconds(0.5f);
                card.FlipCard();
                plr.handVal += card.GetCardValue();
                dealerScore.text = "Hand: " + plr.handVal.ToString();
            }
            if (dealer.handVal >= 21 || dealer.handVal > player.handVal) RoundOver();
        }
    }

    IEnumerator fstHitCardDealer(Player plr, Card card)
    {
        yield return new WaitForSeconds(1f);
        if (!card.faceUp)
        {
            card.FlipCard();
        }
        plr.handVal += card.GetCardValue();
        if (plr.isDealer)
        {
            dealerScore.text = "Hand: " + plr.handVal.ToString();
            if (dealer.handVal > player.handVal)
            {
                RoundOver();
            }
            if (plr.handVal > 20)
            {
                if (firstStand)
                {
                    dealer.hand[1].FlipCard();
                    dealer.handVal += dealer.hand[1].GetCardValue();
                    dealerScore.text = "Hand: " + dealer.handVal.ToString();
                }
                RoundOver();
            }
            if (dealer.handVal <= player.handVal) StartCoroutine(HitCardDealer(dealer));
        }
    }

    private bool firstStand = true;
    private void StandClicked()
    {
        if (firstStand)
        {
            firstStand = false;
            StartCoroutine(fstHitCardDealer(dealer, dealer.hand[1]));
        }

        //it keeps hitting after the dealer has won on his second card


    }

    IEnumerator ResetCards()
    {
        yield return new WaitForSeconds(4f);
        foreach (Card card in player.hand)
        {
            card.FlipCard();
        }
        foreach (Card card in dealer.hand)
        {
            card.FlipCard();
        }
        yield return new WaitForSeconds(1f);
        player.ResetHand();
        dealer.ResetHand();
        deck.Shuffle();
        deck.currentIndex = 1;
        dealerScore.text = "Hand: 0";
        playerScore.text = "Hand: 0";
        dealButton.interactable = true;
    }

    private void NewRound()
    {
        firstStand = true;
        roundOver = false;
        StartCoroutine(ResetCards());
    }

    void RoundOver()
    {
        bool playerBust = player.handVal > 21;
        bool dealerBust = dealer.handVal > 21;
        bool player21 = player.handVal == 21;
        bool dealer21 = dealer.handVal == 21;
        if (playerBust && dealerBust)
        {
            winnerText.text = "Both players bust!";
            player.AdjustMoney(totalBet / 2);
        }
        else if (playerBust || (!dealerBust && dealer.handVal > player.handVal))
        {
            roundOver = true;
            winnerText.text = "Dealer wins!";
        }
        else if (dealerBust || player.handVal > dealer.handVal)
        {
            roundOver = true;
            winnerText.text = "Player wins!";
            player.AdjustMoney(totalBet * 2);
        }
        else if (player.handVal == dealer.handVal)
        {
            roundOver = true;
            winnerText.text = "It's a tie!";
            player.AdjustMoney(totalBet / 2);
        }
        else if (!firstStand && dealer.handVal > player.handVal)
        {
            roundOver = true;
            winnerText.text = "Dealer wins!";
        }
        else
        {
            roundOver = false;
        }
        if (roundOver)
        {
            hitButton.interactable = false;
            standButton.interactable = false;
            
            betTwentyFive.interactable = true;
            betHundered.interactable = true;
            betFifty.interactable = true;
            winnerText.gameObject.SetActive(true);
            dealerScore.gameObject.SetActive(true);
            betAmount.text = "Bet: $0";
            playerMoney.text = "$" + player.GetMoney().ToString();
            totalBet = 0;
            NewRound();
        }
        if (player.GetMoney() <= 0)
        {
            winnerText.text = "You're out of money!";
            playerScore.gameObject.SetActive(false);
            dealerScore.gameObject.SetActive(false);
            dealButton.interactable = false;
            betTwentyFive.interactable = false;
            betHundered.interactable = false;
            betFifty.interactable = false;
        }
    }
}
