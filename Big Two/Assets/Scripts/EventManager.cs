using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{

    //public delegate void GameplayManager_SetSelectedCard(GameObject card);
    //public static GameplayManager_SetSelectedCard gameplayManager_SetSelectedCard;

    //public delegate void GameplayManager_DragCard(Vector3 curPosition);
    //public static GameplayManager_DragCard gameplayManager_DragCard;

    //public delegate void GameplayManager_DropCard(Vector3 curPosition);
    //public static GameplayManager_DropCard gameplayManager_DropCard;

    //public delegate void GameplayManager_Card_Selected(Card card);
    //public static GameplayManager_Card_Selected card_Selected;

    //public delegate void GameplayManager_SetSelectedDeck(int DeckIndex);
    //public static GameplayManager_SetSelectedDeck gameplayManager_SetSelectedDeck;

    //public delegate void GameplayManager_OnTriggerEnter2D(Collider2D collision);
    //public static GameplayManager_OnTriggerEnter2D gameplayManager_OnTriggerEnter2D;

    //public delegate void GameplayManager_OnTriggerExit2D(Collider2D collision);
    //public static GameplayManager_OnTriggerExit2D gameplayManager_OnTriggerExit2D;

    public delegate GameObject GameplayManager_GetDeckGameObject(int Index);
    public static GameplayManager_GetDeckGameObject gameplayManager_GetDeckGameObject;

    public delegate GameObject GameplayManager_GetCardOnDeck(int index);
    public static GameplayManager_GetCardOnDeck gameplayManager_GetCardOnDeck;

    public delegate void GameplayManager_CheckPlayerPokerCard();
    public static GameplayManager_CheckPlayerPokerCard gameplayManager_CheckPlayerPokerCard;

}
