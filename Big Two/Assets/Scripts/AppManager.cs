using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AppManager
{
    public static PlayerActionState playerActionState;
    public static int MovingCard;
    public static GameState gameState;
    public static Texture[] avatarTexture;
    public static AnimationBehavior[] avatars;
    public static int PlayerSelectedAvatar;
    public static AudioManager audioManager;

    static AppManager() {
        //init things
    }

    public static string GetPockerText(CardStruct[] Deck1)
    {
        if (PokerChecker.isFlush(Deck1) && PokerChecker.isStraight(Deck1))
        {
            return "STRAIGHT FLUSH";
        }
        else if (PokerChecker.is4s(Deck1))
        {
            return "FOUR OF A KIND";
        }
        else if (PokerChecker.isFullHouse(Deck1))
        {
            return "FULL HOUSE";
        }
        else if (PokerChecker.isFlush(Deck1))
        {
            return "FLUSH";
        }
        else if (PokerChecker.isStraight(Deck1))
        {
            return "STRAIGHT";
        }
        else if (PokerChecker.is3s(Deck1))
        {
            return "THREE OF KIND";
        }
        else if (PokerChecker.is22s(Deck1))
        {
            return "TWO PAIRS";
        }
        else if (PokerChecker.is2s(Deck1))
        {
            return "ONE PAIR";
        }
        else
        {
            return "HIGH CARD";
        }
    }
}
