using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardOnDeck : MonoBehaviour
{
    public CardStruct cardStruct;
    //public int id;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseCardImage()
    {
        string spriteName = "Cards2/cardBack";
        var texture = Resources.Load<Texture2D>(spriteName);
        transform.GetChild(1).GetComponent<RawImage>().texture = texture;
    }

    public void SetCardImage() {
        string spriteName = "Cards2/card";

        switch (cardStruct.Suit) {
            case 1: spriteName = spriteName + "Diamonds";
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

        if (cardStruct.Rank < 10 && cardStruct.Rank>-1) {
            spriteName = spriteName + cardStruct.Rank;
        }else if (cardStruct.Rank == 10)
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


}
