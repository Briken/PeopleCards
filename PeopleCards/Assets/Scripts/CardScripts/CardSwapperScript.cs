using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CardSwapperScript : MonoBehaviour
{
    private int _currentCardID = 0;

  public CardDisplayScript displayScript;

  public DeckPersistenceScript deckManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayNextCard()
    {
        if (_currentCardID >=  - 1)
        {
            _currentCardID = 0;
        }
        else
        {
            _currentCardID++;
        }

        ValiddateAndDisplayCard(_currentCardID);
    }

    public void DisplayPrevCard()
    {
        if (_currentCardID == 0)
        {
            _currentCardID = deckManager.getActiveDeck().getCardList().Count - 1;
        }
        else
        {
            _currentCardID--;
        }
        ValiddateAndDisplayCard(_currentCardID);
    }
    private void ValiddateAndDisplayCard(int id)
    {
        CardScript displayCard = deckManager.getActiveDeck().getCardList()[id];
        displayCard = checkNulls(displayCard);
        displayScript.DisplayNewCard(displayCard);
    }
    private CardScript checkNulls(CardScript validCard)
    {
        if (validCard.b64image == "nullImage")
        {
            validCard.b64image = DefaultCardValues.NO_IMG_FOUND;
        }

        if (validCard.card_name == "nullName")
        {
            validCard.card_name = DefaultCardValues.NO_NAME_FOUND;
        }

        if (validCard.card_description == "nullDesc")
        {
            validCard.card_description = DefaultCardValues.NO_DESC_FOUND;
        }
        return validCard;    
    }
    
}
