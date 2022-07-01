using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject[] RowHandValue;
    public TMPro.TMP_Text[] tmp_HandValue;

    public GameObject GameResult;
    public TMPro.TMP_Text tmp_GameResult;
    public TMPro.TMP_Text tmp_GameResultCoin;
    

    public GameObject ImageClock;

    public AnimationBehavior Avatar;
    public RawImage AvatarRawImage;
    public TMPro.TMP_Text AvatarName;

    public int[] Score = { 0,0,0};
    public int[] RowScore = { 0, 0, 0 };
    public int RowTotalScore;
    public int CoinGained;

    public TMPro.TMP_Text[] tmp_Score;
    public TMPro.TMP_Text tmp_TotalScore;

    public TMPro.TMP_Text tmp_Coin;
    public int Coin;
    public GameObject[] RowCard;
    public List<CardStruct> cards;
    public List<CardOnDeck> cardsOnTable;

    public PlayerState playerState;

    //combination purpose
    int HighestScoreIdx;
    private List<CardStruct> cardsTemp;
    private List<List<CardStruct>> cardsPermutation;
    //combination purpose end;

    // Start is called before the first frame update
    void Start()
    {

        StartOver();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartOver()
    {
        playerState = PlayerState.Iddle;
        ImageClock.SetActive(false);
        CloseCardOnTable();
        ResetHandValue();
        HideGameResultText();
        UpdateCoinGUI();
        ResetRowScore();

        CoinGained = 0;

        for (int i = 0; i < Score.Length; i++)
            Score[i] = 0;

        Avatar.SetAnimation(MonsterState.iddle);
        //RowTotalScore = 0;
    }

    public void HideGameResultText() {
        tmp_GameResult.text = "";
        GameResult.SetActive(false);
    }

    public void ShowGameResult(bool isPlayer, int state) {
        string s = "";
        if (isPlayer)
        {
            if (state == -1)
            {
                s = s + " YOU LOSE!!!";
                Avatar.SetAnimation(MonsterState.loose);
            }
            else if (state == 1)
            { s = s + "YOU WIN!!";
                Avatar.SetAnimation(MonsterState.win);
            }

            
        }
        else {
            if (state == -1)
            {
                s = s + " LOSER!!!";
                Avatar.SetAnimation(MonsterState.loose);
            }
            else if (state == 1)
            { s = s + "WINER!!";
                Avatar.SetAnimation(MonsterState.win);
            }
        }

        if (state != 0) {            
            tmp_GameResult.text = s;
            if (CoinGained > 0)
            {
                tmp_GameResultCoin.text = "+" + CoinGained.ToString();
            }
            else
            {
                tmp_GameResultCoin.text = CoinGained.ToString();
            }
                GameResult.SetActive(true);
        }
    }

    public void CloseCardOnTable()
    {
        for (int i = 0; i < 13; i++)
        {
            cardsOnTable[i].GetComponent<CardOnDeck>().CloseCardImage();
        }
    }

    public void RefreshCardOnTable() {
        for (int i = 0; i < 13; i++)
        {
            //Card c = decks[i].GetComponent<Deck>().card.GetComponent<Card>().cardStruct
            cardsOnTable[i].GetComponent<CardOnDeck>().cardStruct.id = cards[i].id;
            cardsOnTable[i].GetComponent<CardOnDeck>().cardStruct.Rank = cards[i].Rank;
            cardsOnTable[i].GetComponent<CardOnDeck>().cardStruct.Suit = cards[i].Suit;
            cardsOnTable[i].GetComponent<CardOnDeck>().SetCardImage();
        }
    }

    IEnumerator ArrangeCard()
    {

        playerState = PlayerState.ArrangingCard;
        ImageClock.SetActive(true);
        cardsPermutation = new List<List<CardStruct>>();

        //Debug.Log(System.DateTime.Now);

        int n = cards.Count;
        CardStruct cardTemp;

        int n1 = 3;
        int n2 = 5;
        int i = 0;

        while (i < n1 && playerState == PlayerState.ArrangingCard)
        {
            int ii = n1;
            while (ii < n)
            {

                //copy array from its original init an array again
                List<CardStruct> cards1 = new List<CardStruct>();
                cards1 = cards.GetRange(0, cards.Count);

                cardTemp = cards1[ii];
                cards1[ii] = cards1[i];
                cards1[i] = cardTemp;

                int j = n1;
                while (j < n1 + 5)
                {
                    int jj = n1 + n2;
                    while (jj < n)
                    {
                        //init an array again
                        List<CardStruct> cards2 = new List<CardStruct>();
                        cards2 = cards1.GetRange(0, cards1.Count);

                        cardTemp = cards2[jj];
                        cards2[jj] = cards2[j];
                        cards2[j] = cardTemp;

                        cardsPermutation.Add(cards2);
                        
                        jj++;
                        
                    }
                    j++;
                    yield return new WaitForSeconds(Random.Range(1.0f, 2.0f) * 0.1f);
                }
                ii++;
            }
            i++;

            //yield return new WaitForSeconds(Random.Range(1.0f,2.0f));
            //yield return new WaitForSeconds(0.0f);
        }

        FinishArrangeCard();
    }

    void FinishArrangeCard() {
        //Debug.Log("FinishArrangeCard");

        //get higest combination
        HighestScoreIdx = 0;
        int TotalScore = 0;

        for (int i = 0; i < cardsPermutation.Count; i++)
        {
            cardsTemp = cardsPermutation[i];

            int score = 0;
            CardStruct[] Deck1 = new CardStruct[5];
            for (int d1 = 0; d1 < 5; d1++)
            {
                if (d1 < 3)
                {
                    Deck1[d1] = cardsTemp[d1];
                }
                else
                {
                    Deck1[d1].id = -1 * d1;
                    Deck1[d1].Rank = -1 * d1;
                    Deck1[d1].Suit = -1 * d1;
                }
            }
            score += PokerChecker.valueHand(Deck1);

            CardStruct[] Deck2 = new CardStruct[5];
            for (int d1 = 0; d1 < 5; d1++)
            {
                Deck2[d1] = cardsTemp[d1 + 3];
            }
            score += PokerChecker.valueHand(Deck2);

            CardStruct[] Deck3 = new CardStruct[5];
            for (int d1 = 0; d1 < 5; d1++)
            {
                Deck3[d1] = cardsTemp[d1 + 8];
            }
            score += PokerChecker.valueHand(Deck3);

            if (score > TotalScore)
            {
                HighestScoreIdx = i;
                TotalScore = score;
                //Debug.Log(TotalScore.ToString() + "<" + score.ToString());
            }
            else
            {
                //Debug.Log(TotalScore.ToString() + ">" + score.ToString());
            }
        }

        //change current card combination
        cards = cardsPermutation[HighestScoreIdx].GetRange(0, cardsPermutation[HighestScoreIdx].Count);

        //reveal card debug purpose
        //RevealCard();

        //Debug.Log(System.DateTime.Now);
        ImageClock.SetActive(false);
        playerState = PlayerState.Iddle;
    }

    public void RevealCard()
    {
        for (int i = 0; i < cardsOnTable.Count; i++)
        {
            cardsOnTable[i].GetComponent<CardOnDeck>().cardStruct.id = cards[i].id;
            cardsOnTable[i].GetComponent<CardOnDeck>().cardStruct.Rank = cards[i].Rank;
            cardsOnTable[i].GetComponent<CardOnDeck>().cardStruct.Suit = cards[i].Suit;
            cardsOnTable[i].GetComponent<CardOnDeck>().SetCardImage();
        }
    }

    public void ForceStopArrangingCard() {
        Debug.Log("ForceStopArrangingCard");

        playerState = PlayerState.StopingArranging;
        //FinishArrangeCard();        
    }

    //public void setBlink() {
    //    this.gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color32(0, 0, 255, 100);
    //}

    //public void resetBlink()
    //{
    //    this.gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color32(255, 255, 225, 100);
    //}

    public void getScoreRow(int RowIdx) {
        RowCard[RowIdx].transform.SetSiblingIndex(2);

        CardStruct[] Deck1 = new CardStruct[5];
        if (RowIdx == 0)
        {
            for (int d1 = 0; d1 < 5; d1++)
            {
                if (d1 < 3)
                {
                    Deck1[d1] = cards[d1];
                }
                else
                {
                    Deck1[d1].id = -1 * d1;
                    Deck1[d1].Rank = -1 * d1;
                    Deck1[d1].Suit = -1 * d1;
                }
            }
        }
        else if (RowIdx == 1) {
            for (int d1 = 0; d1 < 5; d1++)
            {   
                Deck1[d1] = cards[d1 + 3];
            }
        }
        else if (RowIdx == 2)
        {
            for (int d1 = 0; d1 < 5; d1++)
            {
                Deck1[d1] = cards[d1 + 8];
            }
        }


        //s += "/" + PrintArray(Deck1);
        tmp_HandValue[RowIdx].text = AppManager.GetPockerText(Deck1);
        RowHandValue[RowIdx].SetActive(true);

        Score[RowIdx] = PokerChecker.valueHand(Deck1);
    }

    public void CalculateRowTotalScore() {
        for (int i = 0; i < RowScore.Length; i++)
        {
            RowTotalScore += RowScore[i];
        }        
    }

    public void ResetHandValue()
    {
        for (int i = 0; i < RowHandValue.Length; i++)
        {
            RowCard[i].transform.SetSiblingIndex(i);
            RowHandValue[i].SetActive(false);
            tmp_HandValue[i].text = "";
        }
    }

    //public void resetRowColor()
    //{
    //    for (int i = 0; i < rows.Length; i++)
    //        rows[i].GetComponent<UnityEngine.UI.Image>().color = new Color32(255, 255, 225, 100);
    //}

    //debug purpose
    string PrintArray(CardStruct[] cardStructs)
    {
        string s = "";
        for (int i = 0; i < cardStructs.Length; i++)
            s = s + "[" + cardStructs[i].Rank + "-" + cardStructs[i].Suit + "],";

        return s;
        //Debug.Log(s);
    }

    string PrintList(List<CardStruct> cardStructs)
    {
        string s = "";
        for (int i = 0; i < cardStructs.Count; i++)
            s = s + "[" + cardStructs[i].Rank + "-" + cardStructs[i].Suit + "],";

        return s;
        //Debug.Log(s);
    }

    public void UpdateCoinGUI()
    {
        tmp_Coin.text = Coin.ToString();
    }

    public void UpdateRowScore() {
        int _RowTotalScore = 0;

        for (int i = 0; i < RowScore.Length; i++)
        {
            tmp_Score[i].text = RowScore[i].ToString();
            _RowTotalScore += RowScore[i];
        }

        tmp_TotalScore.text = _RowTotalScore.ToString();
    }

    public void ResetRowScore()
    {        
        for (int i = 0; i < RowScore.Length; i++)
        {
            RowScore[i] = 0;
            tmp_Score[i].text = RowScore[i].ToString();            
        }
        RowTotalScore = 0;
        tmp_TotalScore.text = RowTotalScore.ToString();
    }

    public void ArrangeCards()
    {
        StartCoroutine(ArrangeCard());
    }

    //debug purpose
    public void GUI_ArrangeCard()
    {
        StartCoroutine(ArrangeCard());
    }

}
