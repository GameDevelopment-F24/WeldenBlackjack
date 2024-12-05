using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CardSelector : MonoBehaviour
{

   public Button exitButton;
   public Button whiteCardButton;
   public Button blackCardButton;
   public Button whiteCardButton2;
   public Button blackCardButton2;

   void Start()
   {
      exitButton.onClick.AddListener(Exit);
      whiteCardButton.onClick.AddListener(ShowWhiteCard);
      blackCardButton.onClick.AddListener(ShowBlackCard);
      whiteCardButton2.onClick.AddListener(ShowWhiteCard);
      blackCardButton2.onClick.AddListener(ShowBlackCard);
   }

   void Exit()
   {
        SceneManager.LoadScene("Menu");
   }

   void ShowWhiteCard()
   {
        CardType.CardColor white = CardType.CardColor.White;
        CardType.SetCardType(white);
   }

   void ShowBlackCard()
   {
        CardType.CardColor black = CardType.CardColor.Black;
        CardType.SetCardType(black);
   }

}
