using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public Button startButton;
   public Button rulesButton;
   public Button pickCardButton;

   void Start()
   {
      startButton.onClick.AddListener(StartGame);
      rulesButton.onClick.AddListener(ShowRules);
      pickCardButton.onClick.AddListener(PickACard);
   }

   void StartGame()
   {
        SceneManager.LoadScene("Blackjack");
   }

   void Exit()
   {
        SceneManager.LoadScene("Menu");
   }

   void ShowRules()
   {
        SceneManager.LoadScene("Rules");
   }

   void PickACard()
   {
        SceneManager.LoadScene("PickACard");
   }

}
