using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public List<CardStruct> cards;

    public GameObject SelectedCard;
    public GameObject SelectedDeck;
    public GameObject FromDeck;

    public int PlayerIndex = 0;
    public Player[] Players;
    
    //player card deck
    public GameObject PanelDeck;
    public GameObject PlayerCardDeckPrefab;
    public List<GameObject> PlayerCardInDecks;
    public GameObject CardParent;
    public List<GameObject> decks;
    public TMPro.TMP_Text[] TMP_Poker;
    //player card deck end

    //sharing card animation
    public Transform BackCardParent;
    public GameObject BackCardPrefab;
    private GameObject[] BackCards;
    public Transform[] BackCardPos;
    private int BackCardInAnimation;
    //sharing card animation end

    //cheking row
    private int IdxToCompare, IdxFromCompare;
    //cheking row end;

    //time counter
    private float TimeCounter;
    private float MaxArrangingCard = 50;
    public GameObject[] TimeObject;
    //time counter end

    public float[] TimeCheckRow;//max time to check deck 1 and deck 2

    private void Awake()
    {
        //EventManager.gameplayManager_SetSelectedCard += GameplayManager_SetSelectedCard;
        //EventManager.gameplayManager_DragCard += GameplayManager_DragCard;
        //EventManager.gameplayManager_DropCard += GameplayManager_DropCard;
        //EventManager.gameplayManager_OnTriggerEnter2D += GameplayManager_OnTriggerEnter2D;
        //EventManager.gameplayManager_OnTriggerExit2D += GameplayManager_OnTriggerExit2D;

        EventManager.gameplayManager_GetDeckGameObject += GameplayManager_GetDeckGameObject;
        EventManager.gameplayManager_GetCardOnDeck += GameplayManager_GetCardOnDeck;
        EventManager.gameplayManager_CheckPlayerPokerCard += GameplayManager_CheckPlayerPokerCard;


    }

    // Start is called before the first frame update
    void Start()
    {
        AppManager.gameState = GameState.Pause;
        AppManager.playerActionState = PlayerActionState.None;
        
        CreateCard();
        PanelDeck.SetActive(false);
        TimeObject[0].SetActive(false);

        //prepare avatar

        int[] AvatarIndex = new int[AppManager.avatars.Length];        
        for (int i = 0; i < Players.Length;i++) {
            AvatarIndex[i] = i;
        }

        for (int i = 0; i < Players.Length; i++)
        {
            if (AvatarIndex[i] == AppManager.PlayerSelectedAvatar)
            {
                int ii = AvatarIndex[0];
                AvatarIndex[0] = AvatarIndex[i];
                AvatarIndex[i] = ii;
                break;
            }
        }

        for (int i = 0; i < Players.Length; i++) {
            Players[i].Avatar = AppManager.avatars[AvatarIndex[i]];
            Players[i].AvatarRawImage.texture = AppManager.avatarTexture[AvatarIndex[i]];
            Players[i].AvatarName.text = AppManager.avatars[AvatarIndex[i]].MonsterName;
        }

        //prepare avatar end

        StartGame();


        //GeneratePlayerCardDeck();
    }

    void CreateCard()
    {
        //enemies = new Enemy[3];
        //create  card array
        int IdCounter = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 13; j++)
            {

                CardStruct _CS;
                _CS.id = IdCounter;
                _CS.Rank = j + 1;
                if (_CS.Rank == 1)
                {
                    //Card ace
                    _CS.Rank = 14;
                }
                else if (_CS.Rank == 2)
                {
                    //card 2
                    _CS.Rank = 15;
                }
                _CS.Suit = i + 1;
                cards.Add(_CS);
                //GameObject _GO = Instantiate(PrefabCard, CardParent.transform);
                //_GO.name = "card_" + i.ToString() + "_" + j.ToString();
                //_GO.GetComponent<Card>().cardStruct.id = IdCounter;
                //_GO.GetComponent<Card>().cardStruct.Rank = j + 1;
                //_GO.GetComponent<Card>().cardStruct.Suit = i + 1;
                //_GO.GetComponent<Card>().SetCardImage();
                //cards.Add(_GO);

                //_GO.transform.position = new Vector3(-10, -1, 0);
                IdCounter++;
                //_GO.transform.SetParent(CardParent.transform);
            }
        }
    }

    void SuffleCard()
    {
        AppManager.gameState = GameState.SufflingCard;

        for (int i = 0; i < Players.Length; i++) {
            for (int j = 0; j < Players[i].cardsOnTable.Count; j++)
                Players[i].cardsOnTable[j].gameObject.SetActive(false);
        }

        int RandomIdx = 0;
        for (int i = 0; i < cards.Count; i++)
        {
            RandomIdx = Random.Range(0, cards.Count);
            CardStruct _CS = cards[i];
            cards[i] = cards[RandomIdx];
            cards[RandomIdx] = _CS;
        }

        StartCoroutine( ShareCard());
    }

    IEnumerator ShareCard()
    {
        AppManager.gameState = GameState.SharingCard;
        //share card to player and enemy        
        for (int j = 0; j < Players.Length; j++)
        {
            Players[j].cards.Clear();
            for (int i = 0; i < 13; i++)
            {
                CardStruct _CS = cards[i + (j * 13)];
                Players[j].cards.Add(_CS);
            }

            //enemies[j].RefreshCardOnTable();//debug purpose
        }

        if (BackCards == null) {
            BackCards = new GameObject[52];
            int jj = 0;
            //create backcard object
            for (int j = 0; j < 52; j++)
            {
                GameObject _GO = Instantiate(BackCardPrefab, BackCardParent);
                _GO.transform.SetParent(BackCardParent);
                BackCards[j] = _GO;

                BackCards[j].transform.localPosition = new Vector3(383, 192, 0);
                BackCards[j].SetActive(true);
                BackCards[j].GetComponent<BackCard>().MoveTo(BackCardPos[jj].position, BackCardAfterInPos);
                AppManager.audioManager.PlayOnce(1);
                //wait
                yield return new WaitForSeconds(0.1f);

                BackCardInAnimation += 1;
                jj += 1;
                if (jj >= Players.Length)
                    jj = 0;
            }
        }
        else {
            int jj = 0;
            for (int j = 0; j < 52; j++)
            {
                BackCards[j].transform.localPosition = new Vector3(383, 192, 0);
                BackCards[j].SetActive(true);
                BackCards[j].GetComponent<BackCard>().MoveTo(BackCardPos[jj].position, BackCardAfterInPos);
                AppManager.audioManager.PlayOnce(1);
                //wait
                yield return new WaitForSeconds(0.1f);

                BackCardInAnimation += 1;
                jj += 1;
                if (jj >= Players.Length)
                    jj = 0;
            }
        }

        //start card animation

            //Invoke("ShowPlayerDeck", 1f);
            //Players[PlayerIndex].playerState = PlayerState.ArrangingCard;
        
    }

    void BackCardAfterInPos() {
        BackCardInAnimation -= 1;
        //Debug.Log(BackCardInAnimation);
        if(BackCardInAnimation <= 0){            
            StartCoroutine(BackCardAfterInPosWait());
        }
    }

    IEnumerator BackCardAfterInPosWait()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            for (int j = 0; j < Players[i].cardsOnTable.Count; j++)
                Players[i].cardsOnTable[j].gameObject.SetActive(true);
        }

        for (int j = 0; j < 52; j++)
        {
            BackCards[j].SetActive(false);
        }

        yield return new WaitForSeconds(1.0f);
        ShowPlayerDeck();
        StartCoroutine(PlayerArrangingCard());
        
    }

    void ShowPlayerDeck()
    {
        PanelDeck.SetActive(true);
        Players[PlayerIndex].playerState = PlayerState.ArrangingCard;

        if (PlayerCardInDecks.Count <= 0)
        {
            for (int i = 0; i < 13; i++)
            {
                GameObject _GO = Instantiate(PlayerCardDeckPrefab, CardParent.transform);
                _GO.name = "card_" + i.ToString();
                _GO.GetComponent<Card>().cardStruct.id = Players[PlayerIndex].cards[i].id;
                _GO.GetComponent<Card>().cardStruct.Rank = Players[PlayerIndex].cards[i].Rank;
                _GO.GetComponent<Card>().cardStruct.Suit = Players[PlayerIndex].cards[i].Suit;
                _GO.GetComponent<Card>().SetCardImage();
                PlayerCardInDecks.Add(_GO);
            }
        }
        else
        {
            for (int i = 0; i < 13; i++)
            {
                PlayerCardInDecks[i].GetComponent<Card>().cardStruct.id = Players[PlayerIndex].cards[i].id;
                PlayerCardInDecks[i].GetComponent<Card>().cardStruct.Rank = Players[PlayerIndex].cards[i].Rank;
                PlayerCardInDecks[i].GetComponent<Card>().cardStruct.Suit = Players[PlayerIndex].cards[i].Suit;
                PlayerCardInDecks[i].GetComponent<Card>().SetCardImage();
            }
        }

        //positioning card on deck
        for (int i = 0; i < 13; i++)
        {
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(decks[i].transform.position);
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, 0));

            PlayerCardInDecks[i].transform.position = new Vector3(point.x, point.y, 0);

            PlayerCardInDecks[i].GetComponent<Card>().CurrentDeckIndex = i;
            decks[i].GetComponent<Deck>().card = PlayerCardInDecks[i];
        }
    }

    IEnumerator CompareRow(int RowIdx)
    {

        AppManager.gameState = GameState.CheckingRow;
        IdxToCompare = 0;
        IdxFromCompare = 0;

        while (IdxFromCompare < Players.Length && AppManager.gameState == GameState.CheckingRow)
        {
            IdxToCompare = 0;
            while (IdxToCompare < Players.Length && AppManager.gameState == GameState.CheckingRow)
            {
                if (IdxFromCompare != IdxToCompare)
                {
                    Debug.Log(IdxToCompare.ToString() + " from " + IdxFromCompare.ToString() + "idx " + RowIdx.ToString());

                    //Players[IdxToCompare].setBlink();
                    Players[IdxFromCompare].getScoreRow(RowIdx);

                    //Players[IdxFromCompare].setBlink();
                    Players[IdxToCompare].getScoreRow(RowIdx);

                    if (Players[IdxFromCompare].Score[RowIdx] < Players[IdxToCompare].Score[RowIdx])
                    {
                        Players[IdxFromCompare].RowScore[RowIdx] -= 1;
                        //coin
                        Players[IdxFromCompare].CoinGained -= 10;
                        //Players[IdxToCompare].CoinGained += 10;
                    }
                    else
                    {
                        Players[IdxFromCompare].RowScore[RowIdx] += 1;
                        //coin
                        Players[IdxFromCompare].CoinGained += 10;
                        //Players[IdxToCompare].CoinGained -= 10;
                    }

                    Players[IdxFromCompare].UpdateRowScore();

                }

                IdxToCompare += 1;
            }

            IdxFromCompare += 1;
        }

        yield return new WaitForSeconds(2.0f);

        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].ResetHandValue();
        }

        //continue to check next row
        if (RowIdx < 2)
        {
            StartCoroutine(CompareRow(RowIdx + 1));
        }
        else
        {
            StartCoroutine(CheckFinal());
        }

    }



    IEnumerator CheckFinal()
    {
        AppManager.gameState = GameState.CheckingFinal;
        IdxToCompare = 0;
        IdxFromCompare = 0;

        while (IdxFromCompare < Players.Length)
        {
            IdxToCompare = 0;
            while (IdxToCompare < Players.Length)
            {
                Debug.Log(IdxToCompare.ToString() + " from " + IdxFromCompare.ToString());

                //check if win from all player then payment x4, get from each player
                if (Players[IdxFromCompare].RowScore[0] == 3 && Players[IdxFromCompare].RowScore[1] == 3 && Players[IdxFromCompare].RowScore[2] == 3)
                {
                    int CoinGained = Players[IdxFromCompare].CoinGained * 4;

                    //get all payment from all player
                    for (int i = 0; i < Players.Length; i++)
                    {
                        if (i != IdxFromCompare)
                        {
                            Players[IdxFromCompare].CoinGained += CoinGained;
                            Players[IdxToCompare].CoinGained -= CoinGained;
                        }
                    }
                }

                IdxToCompare += 1;
            }
            IdxFromCompare += 1;
        }

        //show win or loose result and the coin
        for (int i = 0; i < Players.Length; i++)
        {
            //calculate total score
            Players[i].CalculateRowTotalScore();
            bool isplayer = i == PlayerIndex;

            if (Players[i].RowTotalScore < 0)
            {
                Players[i].ShowGameResult(isplayer, -1);
            }
            else if (Players[i].RowTotalScore > 0)
            {
                Players[i].ShowGameResult(isplayer, 1);
            }
            else
            {
                Players[i].ShowGameResult(isplayer, 0);
            }

            //calculate each player coin
            Players[i].Coin += Players[i].CoinGained;
        }


        //wait
        yield return new WaitForSeconds(3.0f);

        StartOver();
    }

    //IEnumerator PlayingGame() {
    //    while (true)
    //    {
    //        //Debug.Log(AppManager.playerActionState.ToString() + " " + AppManager.gameState.ToString());
    //        switch (AppManager.gameState)
    //        {
    //            case GameState.Pause:
    //                break;
    //            case GameState.SharingCard:
    //                break;
    //            case GameState.ArrangingCard:
    //                break;
    //            case GameState.CheckAllCard:
    //                break;
    //        }
    //        TimeCounter += Time.deltaTime;
    //    }
    //}

    //void GeneratePlayerCardDeck() {
    //    //cards = new List<GameObject>();
    //    //int IdCounter = 0;
    //    //for (int i = 0; i < 4; i++)
    //    //{
    //    //    for (int j = 0; j < 13; j++)
    //    //    {
    //    //        GameObject _GO = Instantiate(PrefabCard, CardParent.transform);
    //    //        _GO.name = "card_" + i.ToString() + "_" + j.ToString();
    //    //        _GO.GetComponent<Card>().cardStruct.id = IdCounter;
    //    //        _GO.GetComponent<Card>().cardStruct.Rank = j + 1;
    //    //        _GO.GetComponent<Card>().cardStruct.Suit = i + 1;
    //    //        _GO.GetComponent<Card>().SetCardImage();
    //    //        cards.Add(_GO);

    //    //        _GO.transform.position = new Vector3(-10, -1, 0);
    //    //            IdCounter++;
    //    //        //_GO.transform.SetParent(CardParent.transform);
    //    //    }
    //    //}
    //}

    void StartGame()
    {
        AppManager.gameState = GameState.Pause;

        //give starter coin
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].Coin = 1000;
            Players[i].StartOver();
        }

        SuffleCard();        
    }

    void StartOver()
    {
        PanelDeck.SetActive(false);

        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].StartOver();
        }

        SuffleCard();
    }

    void PlayerFinishArrangeCard()
    {
        PanelDeck.SetActive(false);

        //copy card value into card player deck
        Players[PlayerIndex].RefreshCardOnTable();

        Players[PlayerIndex].playerState = PlayerState.Iddle;
    }

    public GameObject GameplayManager_GetDeckGameObject(int index)
    {
        //Debug.Log(index);
        //Debug.Log(decks[index].name);
        return decks[index];
    }

    public GameObject GameplayManager_GetCardOnDeck(int index)
    {
        return decks[index].GetComponent<Deck>().card;
    }

    public void GameplayManager_CheckPlayerPokerCard()
    {
        CheckPlayerPokerCard();
    }

    public void CheckPlayerPokerCard()
    {
        if (AppManager.gameState == GameState.ArrangingCard)
        {
            //change player card value from deck that have been changed
            for (int i = 0; i < 13; i++)
            {
                Players[PlayerIndex].cards[i] = decks[i].GetComponent<Deck>().card.GetComponent<Card>().cardStruct;
            }

            CardStruct[] Deck1 = new CardStruct[5];
            for (int i = 0; i < 5; i++)
            {
                if (i < 3)
                {
                    Deck1[i] = Players[PlayerIndex].cards[i];
                }
                else
                {
                    Deck1[i].id = -1 * i;
                    Deck1[i].Rank = -1 * i;
                    Deck1[i].Suit = -1 * i;
                }
            }

            CardStruct[] Deck2 = new CardStruct[5];
            for (int i = 0; i < 5; i++)
            {
                Deck2[i] = Players[PlayerIndex].cards[i + 3];
            }

            CardStruct[] Deck3 = new CardStruct[5];
            for (int i = 0; i < 5; i++)
            {
                Deck3[i] = Players[PlayerIndex].cards[i + 8];
            }


            TMP_Poker[0].text = AppManager.GetPockerText(Deck1);
            TMP_Poker[1].text = AppManager.GetPockerText(Deck2);
            TMP_Poker[2].text = AppManager.GetPockerText(Deck3);
        }
    }


    IEnumerator PlayerArrangingCard()
    {

        AppManager.gameState = GameState.ArrangingCard;
        TimeObject[0].SetActive(true);

        TimeCounter = 0;

        for (int i = 0; i < Players.Length; i++)
        {
            if (i != PlayerIndex)
                Players[i].ArrangeCards();
        }

        int j = 0;
        while (j < Players.Length && TimeCounter < MaxArrangingCard)
        {
            //Debug.Log(Players[j].playerState);
            UpdateTimer();
            
            TimeCounter += 1.0f;
            //Debug.Log(TimeCounter.ToString() + "-" + j.ToString());
            j = 0;
            for (int i = 0; i < Players.Length; i++)
            {
                if (Players[i].playerState == PlayerState.Iddle)
                    j += 1;
            }
            yield return new WaitForSeconds(1.0f);

            Debug.Log(j < Players.Length && TimeCounter < MaxArrangingCard);
        }

        if (TimeCounter >= MaxArrangingCard)
        {
            for (int i = 0; i < Players.Length; i++)
            {
                if (i != PlayerIndex)
                {
                    if (Players[i].playerState == PlayerState.ArrangingCard)
                        Players[i].ForceStopArrangingCard();
                }
                else {
                    PlayerFinishArrangeCard();
                }
            }

            //wait all enemy to finish last job
            j = 0;
            while (j < Players.Length)
            {
                //Debug.Log(Players[j].playerState);                
                if (Players[j].playerState == PlayerState.Iddle)
                    j += 1;                
            }
        }

        TimeObject[0].SetActive(false);

        //wait
        yield return new WaitForSeconds(2.0f);

        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i].playerState == PlayerState.Iddle)
                Players[i].RevealCard();
        }
        
        //wait
        yield return new WaitForSeconds(1.0f);
        //start check the row
        StartCoroutine(CompareRow(0));
    }

    void UpdateTimer() {
        TimeObject[0].transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = Mathf.RoundToInt(MaxArrangingCard - TimeCounter).ToString();
        TimeObject[1].transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = Mathf.RoundToInt(MaxArrangingCard - TimeCounter).ToString();
    }

    //GUI event
    public void GUI_Button1()
    {
        SuffleCard();
        ShareCard();
    }

    public void GUI_Button2()
    {
        StartCoroutine(PlayerArrangingCard());
    }

    public void GUI_ButtonStartgame()
    {
        StartGame();
    }

    //player click finish button in deck
    public void GUI_ButtonArrangePlayerCardFinish()
    {
        PlayerFinishArrangeCard();
    }

    public void GUI_ButtonArrangePlayerCardEdit()
    {
        if(AppManager.gameState == GameState.ArrangingCard)
            ShowPlayerDeck();
    }

    public void GUI_ButtonStartOver()
    {
        StartOver();
    }

    public void GUI_ButtonArrangePlayerCard()
    {
        Players[PlayerIndex].ArrangeCards();
        Players[PlayerIndex].RevealCard();
    }
    //GUI event end
}
