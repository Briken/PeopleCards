using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckAddingScript : MonoBehaviour
{
    public DeckPersistenceScript deckManager;
    private Deck activeDeck;
    
    public void CreateNewDeck(string name)
    {

        activeDeck = new Deck(name);
        deckManager.setActiveDeck(activeDeck);
    }
}
