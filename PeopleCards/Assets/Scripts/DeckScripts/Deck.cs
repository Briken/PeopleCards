using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck 
{
    private List<CardScript> deck;
    private string deckName;

    public Deck(List<CardScript> deck, string deckName)
    {
        this.deck = deck;
        this.deckName = deckName;
    }
    public Deck(string deckName)
    {
        this.deckName = deckName;
        this.deck = new List<CardScript>();
    }

    public List<CardScript> getCardList()
    {
        return deck;
    }

    public void setCardList(List<CardScript> newDeck)
    {
        deck = newDeck;
    }

    public string getDeckName()
    {
        return deckName;
    }
    
}
