using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeleteCardScript : MonoBehaviour
{
    public DeckPersistenceScript deckManager;
    private CardSwapperScript swapper;
    private CardScript currentDisplayCard;
    public CardDisplayScript displayScript;
    
    // Start is called before the first frame update
    void Start()
    {
        swapper = GetComponent<CardSwapperScript>();
    }
    
    public void FindActiveDeckAndRemoveCard()
    {
        DeleteCurrentCardFromActiveDeck(deckManager.getActiveDeck());
    }

    private void DeleteCurrentCardFromActiveDeck(Deck activeDeck)
    {
        currentDisplayCard = displayScript.currentCard;
        swapper.DisplayNextCard();
        foreach (CardScript currentCard in activeDeck.getCardList())
        {
            if (currentCard.card_name == currentDisplayCard.card_name 
                && currentCard.card_description == currentDisplayCard.card_description
                && currentCard.age == currentDisplayCard.age
                && currentCard.b64image == currentDisplayCard.b64image)
            {
                List<CardScript> newlist = activeDeck.getCardList();
                newlist.Remove(currentCard);
                activeDeck.setCardList(newlist);
                deckManager.updateCardDeckList(currentCard);
            }
        }
    }
}
