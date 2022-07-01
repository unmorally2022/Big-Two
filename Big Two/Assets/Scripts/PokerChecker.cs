using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//http://www.mathcs.emory.edu/~cheung/Courses/170/Syllabus/10/pokerCheck.html


public static class PokerChecker
{
    public static int STRAIGHT_FLUSH = 8000000;
    // + valueHighCard()
    public static int FOUR_OF_A_KIND = 7000000;
    // + Quads Card Rank
    public static int FULL_HOUSE = 6000000;
    // + SET card rank
    public static int FLUSH = 5000000;
    // + valueHighCard()
    public static int STRAIGHT = 4000000;
    // + valueHighCard()
    public static int SET = 3000000;
    // + Set card value
    public static int TWO_PAIRS = 2000000;
    // + High2*14^4+ Low2*14^2 + card
    public static int ONE_PAIR = 1000000;
    // + high*14^2 + high2*14^1 + low

    static PokerChecker()
    {

    }

    /***********************************************************
     Methods used to determine a certain Poker hand
    ***********************************************************/

    /* --------------------------------------------------------
       valueHand(): return value of a hand
       -------------------------------------------------------- */
    public static int valueHand(CardStruct[] h)
    {
        if (isFlush(h) && isStraight(h))
            return valueStraightFlush(h);
        else if (is4s(h))
            return valueFourOfAKind(h);
        else if (isFullHouse(h))
            return valueFullHouse(h);
        else if (isFlush(h))
            return valueFlush(h);
        else if (isStraight(h))
            return valueStraight(h);
        else if (is3s(h))
            return valueSet(h);
        else if (is22s(h))
            return valueTwoPairs(h);
        else if (is2s(h))
            return valueOnePair(h);
        else
            return valueHighCard(h);
    }


    /* -----------------------------------------------------
       valueFlush(): return value of a Flush hand

             value = FLUSH + valueHighCard()
       ----------------------------------------------------- */
    public static int valueStraightFlush(CardStruct[] h)
    {
        return STRAIGHT_FLUSH + valueHighCard(h);
    }

    /* -----------------------------------------------------
       valueFlush(): return value of a Flush hand

             value = FLUSH + valueHighCard()
       ----------------------------------------------------- */
    public static int valueFlush(CardStruct[] h)
    {
        return FLUSH + valueHighCard(h);
    }

    /* -----------------------------------------------------
       valueStraight(): return value of a Straight hand

             value = STRAIGHT + valueHighCard()
       ----------------------------------------------------- */
    public static int valueStraight(CardStruct[] h)
    {
        return STRAIGHT + valueHighCard(h);
    }

    /* ---------------------------------------------------------
       valueFourOfAKind(): return value of a 4 of a kind hand

             value = FOUR_OF_A_KIND + 4sCardRank

       Trick: card h[2] is always a card that is part of 
              the 4-of-a-kind hand
          There is ONLY ONE hand with a quads of a given rank.
       --------------------------------------------------------- */
    public static int valueFourOfAKind(CardStruct[] h)
    {
        sortByRank(h);

        return FOUR_OF_A_KIND + h[2].Rank;
    }

    /* -----------------------------------------------------------
       valueFullHouse(): return value of a Full House hand

             value = FULL_HOUSE + SetCardRank

       Trick: card h[2] is always a card that is part of
              the 3-of-a-kind in the full house hand
          There is ONLY ONE hand with a FH of a given set.
       ----------------------------------------------------------- */
    public static int valueFullHouse(CardStruct[] h)
    {
        sortByRank(h);

        return FULL_HOUSE + h[2].Rank;
    }

    /* ---------------------------------------------------------------
       valueSet(): return value of a Set hand

             value = SET + SetCardRank

       Trick: card h[2] is always a card that is part of the set hand
          There is ONLY ONE hand with a set of a given rank.
       --------------------------------------------------------------- */
    public static int valueSet(CardStruct[] h)
    {
        sortByRank(h);

        return SET + h[2].Rank;
    }

    /* -----------------------------------------------------
       valueTwoPairs(): return value of a Two-Pairs hand

             value = TWO_PAIRS
                    + 14*14*HighPairCard
                    + 14*LowPairCard
                    + UnmatchedCard
       ----------------------------------------------------- */
    public static int valueTwoPairs(CardStruct[] h)
    {
        int val = 0;

        sortByRank(h);

        if (h[0].Rank == h[1].Rank &&
             h[2].Rank == h[3].Rank)
            val = 14 * 14 * h[2].Rank + 14 * h[0].Rank + h[4].Rank;
        else if (h[0].Rank == h[1].Rank &&
                  h[3].Rank == h[4].Rank)
            val = 14 * 14 * h[3].Rank + 14 * h[0].Rank + h[2].Rank;
        else
            val = 14 * 14 * h[3].Rank + 14 * h[1].Rank + h[0].Rank;

        return TWO_PAIRS + val;
    }

    /* -----------------------------------------------------
       valueOnePair(): return value of a One-Pair hand

             value = ONE_PAIR 
                    + 14^3*PairCard
                    + 14^2*HighestCard
                    + 14*MiddleCard
                    + LowestCard
       ----------------------------------------------------- */
    public static int valueOnePair(CardStruct[] h)
    {
        int val = 0;

        sortByRank(h);

        if (h[0].Rank == h[1].Rank)
            val = 14 * 14 * 14 * h[0].Rank +
                   +h[2].Rank + 14 * h[3].Rank + 14 * 14 * h[4].Rank;
        else if (h[1].Rank == h[2].Rank)
            val = 14 * 14 * 14 * h[1].Rank +
                   +h[0].Rank + 14 * h[3].Rank + 14 * 14 * h[4].Rank;
        else if (h[2].Rank == h[3].Rank)
            val = 14 * 14 * 14 * h[2].Rank +
                   +h[0].Rank + 14 * h[1].Rank + 14 * 14 * h[4].Rank;
        else
            val = 14 * 14 * 14 * h[3].Rank +
                   +h[0].Rank + 14 * h[1].Rank + 14 * 14 * h[2].Rank;

        return ONE_PAIR + val;
    }

    /* -----------------------------------------------------
       valueHighCard(): return value of a high card hand

             value =  14^4*highestCard 
                    + 14^3*2ndHighestCard
                    + 14^2*3rdHighestCard
                    + 14^1*4thHighestCard
                    + LowestCard
       ----------------------------------------------------- */
    public static int valueHighCard(CardStruct[] h)
    {
        int val;

        sortByRank(h);

        val = h[0].Rank + 14 * h[1].Rank + 14 * 14 * h[2].Rank
              + 14 * 14 * 14 * h[3].Rank + 14 * 14 * 14 * 14 * h[4].Rank;

        return val;
    }


    /***********************************************************
      Methods used to determine a certain Poker hand
     ***********************************************************/


    /* ---------------------------------------------
       is4s(): true if h has 4 of a kind
               false otherwise
       --------------------------------------------- */
    public static bool is4s(CardStruct[] h)
    {
        bool a1, a2;

        if (h.Length != 5)
            return (false);

        sortByRank(h);

        a1 = h[0].Rank == h[1].Rank &&
             h[1].Rank == h[2].Rank &&
             h[2].Rank == h[3].Rank;

        a2 = h[1].Rank == h[2].Rank &&
             h[2].Rank == h[3].Rank &&
             h[3].Rank == h[4].Rank;

        return (a1 || a2);
    }


    /* ----------------------------------------------------
       isFullHouse(): true if h has Full House
                      false otherwise
       ---------------------------------------------------- */
    public static bool isFullHouse(CardStruct[] h)
    {
        bool a1, a2;

        if (h.Length != 5)
            return (false);

        sortByRank(h);

        a1 = h[0].Rank == h[1].Rank &&  //  x x x y y
             h[1].Rank == h[2].Rank &&
             h[3].Rank == h[4].Rank;

        a2 = h[0].Rank == h[1].Rank &&  //  x x y y y
             h[2].Rank == h[3].Rank &&
             h[3].Rank == h[4].Rank;

        return (a1 || a2);
    }



    /* ----------------------------------------------------
       is3s(): true if h has 3 of a kind
               false otherwise

       **** Note: use is3s() ONLY if you know the hand
                  does not have 4 of a kind 
       ---------------------------------------------------- */
    public static bool is3s(CardStruct[] h)
    {
        bool a1, a2, a3;

        if (h.Length != 5)
            return (false);

        if (is4s(h) || isFullHouse(h))
            return (false);        // The hand is not 3 of a kind (but better)

        /* ----------------------------------------------------------
           Now we know the hand is not 4 of a kind or a full house !
           ---------------------------------------------------------- */
        sortByRank(h);

        a1 = h[0].Rank == h[1].Rank &&
             h[1].Rank == h[2].Rank;

        a2 = h[1].Rank == h[2].Rank &&
             h[2].Rank == h[3].Rank;

        a3 = h[2].Rank == h[3].Rank &&
             h[3].Rank == h[4].Rank;

        return (a1 || a2 || a3);
    }

    /* -----------------------------------------------------
       is22s(): true if h has 2 pairs
                false otherwise

       **** Note: use is22s() ONLY if you know the hand
                  does not have 3 of a kind or better
       ----------------------------------------------------- */
    public static bool is22s(CardStruct[] h)
    {
        bool a1, a2, a3;

        if (h.Length != 5)
            return (false);

        if (is4s(h) || isFullHouse(h) || is3s(h))
            return (false);        // The hand is not 2 pairs (but better)

        sortByRank(h);

        a1 = h[0].Rank == h[1].Rank &&
             h[2].Rank == h[3].Rank;

        a2 = h[0].Rank == h[1].Rank &&
             h[3].Rank == h[4].Rank;

        a3 = h[1].Rank == h[2].Rank &&
             h[3].Rank == h[4].Rank;

        return (a1 || a2 || a3);
    }


    /* -----------------------------------------------------
       is2s(): true if h has one pair
               false otherwise

       **** Note: use is22s() ONLY if you know the hand
                  does not have 2 pairs or better
       ----------------------------------------------------- */
    public static bool is2s(CardStruct[] h)
    {
        bool a1, a2, a3, a4;

        if (h.Length != 5)
            return (false);

        if (is4s(h) || isFullHouse(h) || is3s(h) || is22s(h))
            return (false);        // The hand is not one pair (but better)

        sortByRank(h);

        a1 = h[0].Rank == h[1].Rank;
        a2 = h[1].Rank == h[2].Rank;
        a3 = h[2].Rank == h[3].Rank;
        a4 = h[3].Rank == h[4].Rank;

        return (a1 || a2 || a3 || a4);
    }


    /* ---------------------------------------------
       isFlush(): true if h has a flush
                  false otherwise
       --------------------------------------------- */
    public static bool isFlush(CardStruct[] h)
    {
        if (h.Length != 5)
            return (false);

        sortBySuit(h);

        return (h[0].Suit == h[4].Suit);   // All cards has same suit
    }


    /* ---------------------------------------------
       isStraight(): true if h is a Straight
                     false otherwise
       --------------------------------------------- */
    public static bool isStraight(CardStruct[] h)
    {
        int i, testRank;

        if (h.Length != 5)
            return (false);

        sortByRank(h);

        /* ===========================
           Check if hand has an Ace
           =========================== */
        if (h[4].Rank == 14)
        {
            /* =================================
               Check straight using an Ace
               ================================= */
            bool a = h[0].Rank == 2 && h[1].Rank == 3 &&
                        h[2].Rank == 4 && h[3].Rank == 5;
            bool b = h[0].Rank == 10 && h[1].Rank == 11 &&
                        h[2].Rank == 12 && h[3].Rank == 13;

            return (a || b);
        }
        else
        {
            /* ===========================================
               General case: check for increasing values
               =========================================== */
            testRank = h[0].Rank + 1;

            for (i = 1; i < 5; i++)
            {
                if (h[i].Rank != testRank)
                    return (false);        // Straight failed...

                testRank++;
            }

            return (true);        // Straight found !
        }
    }

    /* ===========================================================
       Helper methods
       =========================================================== */

    /* ---------------------------------------------
       Sort hand by rank:

           smallest ranked card first .... 

       (Finding a straight is eaiser that way)
       --------------------------------------------- */
    public static void sortByRank(CardStruct[] h)
    {
        int i, j, min_j;

        /* ---------------------------------------------------
           The selection sort algorithm
           --------------------------------------------------- */
        for (i = 0; i < h.Length; i++)
        {
            /* ---------------------------------------------------
               Find array element with min. value among
               h[i], h[i+1], ..., h[n-1]
               --------------------------------------------------- */
            min_j = i;   // Assume elem i (h[i]) is the minimum

            for (j = i + 1; j < h.Length; j++)
            {
                if (h[j].Rank < h[min_j].Rank)
                {
                    min_j = j;    // We found a smaller minimum, update min_j     
                }
            }

            /* ---------------------------------------------------
               Swap a[i] and a[min_j]
               --------------------------------------------------- */
            CardStruct help = h[i];
            h[i] = h[min_j];
            h[min_j] = help;
        }

        //debug purpose
        string s = "";
        for (i = 0; i < h.Length; i++)
        {
            s = s + h[i].ToString() + "-";
        }
        //debug purpose end;
    }

    /* ---------------------------------------------
       Sort hand by suit:

           smallest suit card first .... 

       (Finding a flush is eaiser that way)
       --------------------------------------------- */
    public static void sortBySuit(CardStruct[] h)
    {
        int i, j, min_j;

        /* ---------------------------------------------------
           The selection sort algorithm
           --------------------------------------------------- */
        for (i = 0; i < h.Length; i++)
        {
            /* ---------------------------------------------------
               Find array element with min. value among
               h[i], h[i+1], ..., h[n-1]
               --------------------------------------------------- */
            min_j = i;   // Assume elem i (h[i]) is the minimum

            for (j = i + 1; j < h.Length; j++)
            {
                if (h[j].Suit < h[min_j].Suit)
                {
                    min_j = j;    // We found a smaller minimum, update min_j     
                }
            }

            /* ---------------------------------------------------
               Swap a[i] and a[min_j]
               --------------------------------------------------- */
            CardStruct help = h[i];
            h[i] = h[min_j];
            h[min_j] = help;
        }
    }
}
