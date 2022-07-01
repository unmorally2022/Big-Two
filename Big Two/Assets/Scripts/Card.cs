using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    /*
     * public static int SPADE = 4;
     * public static int HEART = 3;
     * public static int CLUB = 2;
     * public static int DIAMOND = 1;
     */

    private Vector3 screenPoint;
    private Vector3 offset;

    public CardStruct cardStruct;
    //public int id;

    public int CurrentDeckIndex;
    //public int SelectedDeckIndex;

    public List<int> hittedDeck;
    //variable use to save deck that have been hitted by card, and when trigger exit and the index of the deck is contained on this varible, then remove 

    //public GameObject CurrentDeck;
    //public GameObject SelectedDeck;

    public Vector3 _startPos, _endPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCardImage()
    {
        string spriteName = "Cards2/card";

        switch (cardStruct.Suit)
        {
            case 1:
                spriteName = spriteName + "Diamonds";
                break;
            case 2:
                spriteName = spriteName + "Clubs";
                break;
            case 3:
                spriteName = spriteName + "Hearts";
                break;
            case 4:
                spriteName = spriteName + "Spades";
                break;
        }

        if (cardStruct.Rank < 10 && cardStruct.Rank > -1)
        {
            spriteName = spriteName + cardStruct.Rank;
        }
        else if (cardStruct.Rank == 10)
        {
            spriteName = spriteName + cardStruct.Rank;
        }
        else if (cardStruct.Rank == 11)
        {
            spriteName = spriteName + "J";
        }
        else if (cardStruct.Rank == 12)
        {
            spriteName = spriteName + "Q";
        }
        else if (cardStruct.Rank == 13)
        {
            spriteName = spriteName + "K";
        }
        else if (cardStruct.Rank == 14)
        {
            spriteName = spriteName + "A";
        }
        else if (cardStruct.Rank == 15)
        {
            spriteName = spriteName + "2";
        }

        //Load a Texture (Assets/Resources/Textures/texture01.png)
        var texture = Resources.Load<Texture2D>(spriteName);
        transform.GetChild(1).GetComponent<RawImage>().texture = texture;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (AppManager.playerActionState == PlayerActionState.Dragging && AppManager.gameState == GameState.ArrangingCard)
        {
            //Debug.Log("OnTriggerEnter2D");
            if (collision.tag == "CardDeck1")
            {
                collision.transform.GetChild(0).gameObject.SetActive(true);
                hittedDeck.Add(collision.GetComponent<Deck>().Index);

                if (hittedDeck.Count > 1)
                {
                    GameObject DeckBefore = EventManager.gameplayManager_GetDeckGameObject(hittedDeck[hittedDeck.Count - 1 - 1]);

                    if (DeckBefore != null)
                    {
                        DeckBefore.transform.GetChild(0).gameObject.SetActive(false);
                    }
                }

                //if before is hit the deck
                //if (SelectedDeckIndex >= 0)
                //{
                //    GameObject DeckBefore = EventManager.gameplayManager_GetDeckGameObject(SelectedDeckIndex);
                //    if (DeckBefore != null)
                //    {
                //        DeckBefore.transform.GetChild(0).gameObject.SetActive(false);
                //    }

                //    SelectedDeckIndex = collision.GetComponent<Deck>().Index;
                //}
                //else
                //{
                //    //hit new one and not the current deck index

                //    SelectedDeckIndex = collision.GetComponent<Deck>().Index;
                //    collision.transform.GetChild(0).gameObject.SetActive(true);

                //}
                //SelectedDeck = EventManager.gameplayManager_GetDeckGameObject(collision.gameObject.GetComponent<Card>().CurrentDeckIndex);

                //EventManager.gameplayManager_OnTriggerEnter2D(collision);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerExit2D");
        if (AppManager.playerActionState == PlayerActionState.Dragging && AppManager.gameState == GameState.ArrangingCard)
        {
            if (collision.tag == "CardDeck1")
            {

                int IndexToRemove = hittedDeck.LastIndexOf(collision.GetComponent<Deck>().Index);// hittedDeck.Find(x => x == collision.GetComponent<Deck>().Index);
                Debug.Log(IndexToRemove);
                if (IndexToRemove > -1)
                {//if found
                    GameObject DeckToDeactive = EventManager.gameplayManager_GetDeckGameObject(hittedDeck[IndexToRemove]);
                    if (DeckToDeactive != null)
                    {
                        DeckToDeactive.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    hittedDeck.RemoveAt(IndexToRemove);
                }


                //    //if before hit collider
                //    if (SelectedDeckIndex == CurrentDeckIndex)
                //    {
                //        SelectedDeckIndex = -1;
                //        //collision.transform.GetChild(0).gameObject.SetActive(false);
                //    }
                //    else {
                //        GameObject DeckBefore = EventManager.gameplayManager_GetDeckGameObject(SelectedDeckIndex);
                //        if (DeckBefore != null)
                //        {
                //            DeckBefore.transform.GetChild(0).gameObject.SetActive(false);
                //        }
                //    }
                //    //SelectedDeck = null;
                //    //EventManager.gameplayManager_OnTriggerExit2D(collision);
            }
        }
    }

    void OnMouseDown()
    {
        //Debug.Log(AppManager.actionState);
        //Debug.Log(AppManager.actionState == ActionState.none);
        if (AppManager.playerActionState == PlayerActionState.None && AppManager.gameState == GameState.ArrangingCard)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                AppManager.MovingCard = 0;
                //SelectedDeckIndex = -1;
                hittedDeck.Clear();//clear list of hitted deck
                screenPoint = Camera.main.WorldToScreenPoint(targetObject.transform.position);
                offset = targetObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

                //Debug.Log(targetObject.name);
                //EventManager.gameplayManager_SetSelectedCard(targetObject.transform.gameObject);
                AppManager.playerActionState = PlayerActionState.Dragging;
            }
        }
    }

    void OnMouseDrag()
    {
        if (AppManager.playerActionState == PlayerActionState.Dragging && AppManager.gameState == GameState.ArrangingCard)
        {
            MoveCard();
            //EventManager.gameplayManager_DragCard(curPosition);
        }

    }

    void OnMouseUp()
    {
        Debug.Log("OnMouseUp");
        if (AppManager.playerActionState == PlayerActionState.Dragging && AppManager.gameState == GameState.ArrangingCard)
        {
            //AppManager.actionState = ActionState.none;
            //EventManager.gameplayManager_DropCard(this);
            DropCard();
        }
        else
        {
            //AppManager.actionState = ActionState.none;
        }
    }

    void MoveCard()
    {
        if (AppManager.playerActionState == PlayerActionState.Dragging && AppManager.gameState == GameState.ArrangingCard)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            gameObject.transform.position = new Vector3(curPosition.x, curPosition.y, 0);
        }
    }

    void DropCard()
    {
        AppManager.playerActionState = PlayerActionState.MovingCard;
        if (hittedDeck.Count > 0)
        {
            if (hittedDeck[hittedDeck.Count - 1] == CurrentDeckIndex)
            {
                PutBackCard();
            }
            else
            {
                SwapCard(hittedDeck[hittedDeck.Count - 1]);
            }

            hittedDeck.Clear();
        }
        else
        {
            PutBackCard();
        }
    }

    void PutBackCard()
    {

        //GameObject CurrentDeck = EventManager.gameplayManager_GetDeckGameObject(CurrentDeckIndex);
        //Vector3 startPosition = transform.position;

        //Vector3 endPosition = Camera.main.WorldToScreenPoint(CurrentDeck.transform.position);
        //Vector3 point = Camera.main.ScreenToWorldPoint(endPosition);

        //transform.position = point;// Vector3.Lerp(startPosition, point, 0.75f);
        Debug.Log("PutBackCard" + CurrentDeckIndex.ToString());
        AppManager.MovingCard += 1;
        StartCoroutine(MoveToPosition());

    }

    void SwapCardFromOther(int Index)
    {
        //Debug.Log("SwapCardFromOther " + Index.ToString());
        AppManager.MovingCard += 1;

        CurrentDeckIndex = Index;
        StartCoroutine(MoveToPosition());
    }

    void SwapCard(int Index)
    {
        //Debug.Log("SwapCard " + Index.ToString());
        AppManager.MovingCard += 1;

        //CurrentDeckIndex = Index;
        GameObject _CardToSwap = EventManager.gameplayManager_GetCardOnDeck(Index);
        _CardToSwap.GetComponent<Card>().SwapCardFromOther(CurrentDeckIndex);

        CurrentDeckIndex = Index;
        StartCoroutine(MoveToPosition());
    }

    IEnumerator MoveToPosition()
    {
        AppManager.audioManager.PlayOnce(1);

        //Make sure there is only one instance of this function running
        //if (isMoving)
        //{
        //    yield break; ///exit if this is still running
        //}
        //isMoving = true;

        float counter = 0;

        GameObject CurrentDeck = EventManager.gameplayManager_GetDeckGameObject(CurrentDeckIndex);
        _startPos = transform.position;

        Vector3 endPosition = Camera.main.WorldToScreenPoint(CurrentDeck.transform.position);
        _endPos = Camera.main.ScreenToWorldPoint(endPosition);

        //transform.position = point;// Vector3.Lerp(startPosition, point, 0.75f);
        //transform.position = Vector3.Lerp(_startPosPos, _endPosPos, counter / _duration);

        while (counter < 1f)
        {
            counter += Time.deltaTime / 0.01f;
            transform.position = Vector3.Lerp(_startPos, _endPos, 0.05f);
            yield return null;
        }

        //isMoving = false;
        //CurrentDeckIndex = CurrentDeck.GetComponent<Deck>().Index;
        GameObject DeckToDeactive = EventManager.gameplayManager_GetDeckGameObject(CurrentDeckIndex);
        if (DeckToDeactive != null)
        {
            DeckToDeactive.transform.GetChild(0).gameObject.SetActive(false);
            DeckToDeactive.GetComponent<Deck>().card = this.gameObject;
        }
        transform.position = _endPos;

        AppManager.MovingCard -= 1;
        if (AppManager.MovingCard <= 0)
        {
            AppManager.playerActionState = PlayerActionState.None;
            EventManager.gameplayManager_CheckPlayerPokerCard();
        }
        //afterPoping();

    }

}
