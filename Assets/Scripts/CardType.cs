using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardType 
{

    public enum CardColor
    {
        White,
        Black
    }
    public static CardColor SelectedDeck = CardColor.Black;

    public static void SetCardType(CardColor input){
        if(input == CardColor.White){
            SelectedDeck = CardColor.White;
        }
        else if(input == CardColor.Black){
            SelectedDeck = CardColor.Black;
        }
    } 
    public static CardColor GetCardType(){
        return SelectedDeck;
    }
}
