//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Enemy : MonoBehaviour
//{
//    public GameObject[] rows;
//    public int[] Score = { 0, 0, 0 };
//    public int TotalScore;    

//    public TMPro.TMP_Text[] tmp_Score;
//    public TMPro.TMP_Text tmp_TotalScore;

//    public TMPro.TMP_Text tmp_Coin;
//    public int Coin;

//    public List<CardStruct> cards;
//    public List<CardOnDeck> cardsOnTable;

//    //combination purpose
//    int HighestScoreIdx;
//    public List<CardStruct> cardsTemp;
//    public List<List<CardStruct>> cardsPermutation;
//    //combination purpose end;

//    //public int[] sss = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

//    // Start is called before the first frame update
//    void Start()
//    {
//        CloseCardOnTable();
//        UpdateCoinGUI();
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    public void StartOver()
//    {
//        CloseCardOnTable();
//        resetRowColor();

//        for (int i = 0; i < Score.Length; i++)
//            Score[i] = 0;
//        TotalScore = 0;
//    }

//    public void CloseCardOnTable()
//    {
//        for (int i = 0; i < 13; i++)
//        {
//            cardsOnTable[i].GetComponent<CardOnDeck>().CloseCardImage();
//        }
//    }

//    public void RefreshCardOnTable()
//    {
//        for (int i = 0; i < 13; i++)
//        {
//            //Card c = decks[i].GetComponent<Deck>().card.GetComponent<Card>().cardStruct
//            cardsOnTable[i].GetComponent<CardOnDeck>().cardStruct.id = cards[i].id;
//            cardsOnTable[i].GetComponent<CardOnDeck>().cardStruct.Rank = cards[i].Rank;
//            cardsOnTable[i].GetComponent<CardOnDeck>().cardStruct.Suit = cards[i].Suit;
//            cardsOnTable[i].GetComponent<CardOnDeck>().SetCardImage();
//        }
//    }

//    IEnumerator ArrangeCard()
//    {
//        //cardsPermutation = new List<ListCardPermutationStruct>();

//        //int n = 13;//list length
//        //int[] c = new int[n];

//        //ListCardPermutationStruct _LCPS;
//        //cardsTemp = cards.GetRange(0, cards.Count);

//        ////output(A)
//        //PrintList(cardsTemp);        
//        //_LCPS.cards = cardsTemp.GetRange(0, cards.Count);
//        //cardsPermutation.Add(_LCPS);

//        //// i acts similarly to a stack pointer
//        //int i = 0;

//        //int n1 = 3;
//        //while (i < n1)
//        //{

//        //    //make 3 combination from 13

//        //    i++;
//        //    yield return new WaitForSeconds(0.1f);            
//        //}

//        cardsPermutation = new List<List<CardStruct>>();

//        Debug.Log(System.DateTime.Now);

//        int n = cards.Count;
//        CardStruct cardTemp;

//        int n1 = 3;
//        int n2 = 5;
//        int i = 0;


//        while (i < n1)
//        {
//            int ii = n1;
//            while (ii < n)
//            {

//                //copy array from its original init an array again
//                List<CardStruct> cards1 = new List<CardStruct>();
//                cards1 = cards.GetRange(0, cards.Count);

//                cardTemp = cards1[ii];
//                cards1[ii] = cards1[i];
//                cards1[i] = cardTemp;

//                int j = n1;
//                while (j < n1 + 5)
//                {
//                    int jj = n1 + n2;
//                    while (jj < n)
//                    {
//                        //init an array again
//                        List<CardStruct> cards2 = new List<CardStruct>();
//                        cards2 = cards1.GetRange(0, cards1.Count);

//                        cardTemp = cards2[jj];
//                        cards2[jj] = cards2[j];
//                        cards2[j] = cardTemp;

//                        cardsPermutation.Add(cards2);
//                        //PrintList(cards2);
//                        //Debug.Log(PrintList(sss) + "-" + i.ToString() + "-" + ii.ToString() + "-" + j.ToString() + "-" + jj.ToString());
//                        //Debug.Log(PrintList(sss2));

//                        jj++;
//                    }
//                    j++;
//                }
//                ii++;
//            }
//            i++;

//            yield return new WaitForSeconds(0.0f);
//        }

//        //get higest combination
//        HighestScoreIdx = 0;
//        int TotalScore = 0;

//        for (i = 0; i < cardsPermutation.Count;i++) {
//            cardsTemp = cardsPermutation[i];

//            int score = 0;
//            CardStruct[] Deck1 = new CardStruct[5];
//            for (int d1 = 0; d1 < 5; d1++)
//            {
//                if (d1 < 3)
//                {
//                    Deck1[d1] = cardsTemp[d1];
//                }
//                else
//                {
//                    Deck1[d1].id = -1 * d1;
//                    Deck1[d1].Rank = -1 * d1;
//                    Deck1[d1].Suit = -1 * d1;
//                }
//            }
//            score += PokerChecker.valueHand(Deck1);

//            CardStruct[] Deck2 = new CardStruct[5];
//            for (int d1 = 0; d1 < 5; d1++)
//            {
//                Deck2[d1] = cardsTemp[d1+3];
//            }
//            score += PokerChecker.valueHand(Deck2);

//            CardStruct[] Deck3 = new CardStruct[5];
//            for (int d1 = 0; d1 < 5; d1++)
//            {
//                Deck3[d1] = cardsTemp[d1+8];
//            }
//            score += PokerChecker.valueHand(Deck3);

//            if (score > TotalScore)
//            {
//                HighestScoreIdx = i;
//                TotalScore = score;
//                //Debug.Log(TotalScore.ToString() + "<" + score.ToString());
//            }
//            else {
//                //Debug.Log(TotalScore.ToString() + ">" + score.ToString());
//            }
//        }

//        //change current card combination
//        cards = cardsPermutation[HighestScoreIdx].GetRange(0, cardsPermutation[HighestScoreIdx].Count);

//        //reveal card debug purpose
//        //RevealCard();

//        //int n = sss.Length;
//        //int xx=-1;

//        //int n1 = 3;
//        //int n2 = 5;
//        //int i = 0;


//        //while (i < n1) {
//        //    int ii = n1;
//        //    while (ii < n) {

//        //        //copy array from its original init an array again
//        //        int[] sss1 = new int[sss.Length];
//        //        System.Array.Copy(sss, sss1, sss.Length);

//        //        xx = sss1[ii];
//        //        sss1[ii] = sss1[i];
//        //        sss1[i] = xx;

//        //        int j = n1;
//        //        while (j < n1+5)
//        //        {
//        //            int jj = n1+n2;
//        //            while (jj < n)
//        //            {
//        //                //init an array again
//        //                int[] sss2 = new int[sss1.Length];
//        //                System.Array.Copy(sss1, sss2, sss1.Length);

//        //                xx = sss2[jj];
//        //                sss2[jj] = sss2[j];
//        //                sss2[j] = xx;

//        //                //Debug.Log(PrintList(sss) + "-" + i.ToString() + "-" + ii.ToString() + "-" + j.ToString() + "-" + jj.ToString());
//        //                Debug.Log(PrintList(sss2));

//        //                jj++;
//        //            }
//        //            j++;
//        //        }
//        //        ii++;
//        //    }
//        //    i++;

//        //    yield return new WaitForSeconds(0.0f);
//        //}

//        Debug.Log(System.DateTime.Now);
//    }

//    public void RevealCard() {
//        for (int i = 0; i < cardsOnTable.Count; i++)
//        {
//            cardsOnTable[i].GetComponent<CardOnDeck>().cardStruct.id = cards[i].id;
//            cardsOnTable[i].GetComponent<CardOnDeck>().cardStruct.Rank = cards[i].Rank;
//            cardsOnTable[i].GetComponent<CardOnDeck>().cardStruct.Suit = cards[i].Suit;
//            cardsOnTable[i].GetComponent<CardOnDeck>().SetCardImage();
//        }
//    }

//    public void getScoreRow1()
//    {
//        rows[0].GetComponent<UnityEngine.UI.Image>().color = new Color32(83, 150, 255, 100);

//        CardStruct[] Deck1 = new CardStruct[5];
//        for (int d1 = 0; d1 < 5; d1++)
//        {
//            if (d1 < 3)
//            {
//                Deck1[d1] = cardsTemp[d1];
//            }
//            else
//            {
//                Deck1[d1].id = -1 * d1;
//                Deck1[d1].Rank = -1 * d1;
//                Deck1[d1].Suit = -1 * d1;
//            }
//        }
//        Score[0] = PokerChecker.valueHand(Deck1);
//    }

//    public void resetRowColor()
//    {
//        for (int i = 0; i < rows.Length; i++)
//            rows[i].GetComponent<UnityEngine.UI.Image>().color = new Color32(255, 255, 225, 100);
//    }

//    //debug purpose
//    void PrintList(List<CardStruct> cardStructs) {
//        string s = "";
//        for (int i = 0; i < cardStructs.Count; i++)
//            s = s + "[" +cardStructs[i].Rank +"-"+ cardStructs[i].Suit +"],";

//            Debug.Log(s);
//    }

//    public void UpdateCoinGUI() {
//        tmp_Coin.text = Coin.ToString();
//    }

//    public void ArrangeCards() {
//        StartCoroutine(ArrangeCard());
//    }

//    //debug purpose
//    public void GUI_ArrangeCard()
//    {
//        StartCoroutine(ArrangeCard());
//    }
//}
