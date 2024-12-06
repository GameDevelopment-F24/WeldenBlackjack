using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Rules : MonoBehaviour
{

   public Button exitButton;


   void Start()
   {
      exitButton.onClick.AddListener(Exit);
   }


   void Exit()
   {
        SceneManager.LoadScene("Menu");
   }


}
