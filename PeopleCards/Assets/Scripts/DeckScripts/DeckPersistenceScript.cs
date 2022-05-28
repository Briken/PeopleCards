using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DeckPersistenceScript : MonoBehaviour
{
    private Dictionary<string, Deck> allDecks;
    public List<CardScript> allCards;
    private Deck activeDeck;
    private CardLoaderScript cardLoader;
    // Start is called before the first frame update
    void Start()
    {
        cardLoader.loadCards();
        loadDecks();
        setActiveDeck(allDecks[DefaultCardValues.ALL_DECK_NAME]);
        DontDestroyOnLoad(this.gameObject);    
    }

    private void loadDecks()
    {
        List<CardScript> currentList;
        foreach (CardScript card in allCards)
        {
            foreach (string deckName in card.listofdecks)
            {
                if (allDecks.ContainsKey(deckName))
                {
                    currentList = allDecks[deckName].getCardList();
                    currentList.Add(card);
                    allDecks[deckName].setCardList(currentList);
                }
                else
                {
                    currentList = new List<CardScript>();
                    currentList.Add(card);
                    allDecks.Add(deckName, new Deck(currentList, deckName));
                }

            }
        }
    }

    public Deck getActiveDeck()
    {
        return activeDeck;
    }

    public void setActiveDeck(Deck deck)
    {
        activeDeck = deck;
    }

    public void updateCardDeckList(CardScript currentCard)
    {
        if (currentCard.listofdecks.Length < 2)
        {
            deleteFromFullList(currentCard);
        }
        else
        {
            List<string> newlist;
            newlist = currentCard.listofdecks.ToList();
            newlist.Remove(activeDeck.getDeckName());
            currentCard.listofdecks = newlist.ToArray();
        }
    }

    public void deleteFromFullList(CardScript card)
    {
        this.allCards.Remove(card);
    }
}
