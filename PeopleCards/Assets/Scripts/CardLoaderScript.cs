using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class CardLoaderScript : MonoBehaviour
{
    public CardDisplayScript displayScript;
    public TextAsset jsonFile;
    public List<CardScript> cards = new List<CardScript>();
    // Start is called before the first frame update
    void Start()
    {
        loadCards();
    }

    private void loadCards()
    {
        CardArray cardsFromJson = JsonUtility.FromJson<CardArray>(jsonFile.text);
        Debug.Log("testLog");
        foreach (CardScript card in cardsFromJson.card_array)
        {
            Debug.Log(card.card_name);
            cards.Add(card);
        }

        displayScript.DisplayNewCard(cards[0]);
    }

    public List<CardScript> getCardList()
    {
        return cards;
    }
}
