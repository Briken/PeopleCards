using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class CardLoaderScript : MonoBehaviour
{
    public CardDisplayScript displayScript;
    public TextAsset jsonFile;
    public Deck deck;
    public DeckPersistenceScript deckManager;
    // Start is called before the first frame update
    void Start()
    {
        deck = deckManager.getActiveDeck();
    }

    public Deck getCardList()
    {
        return deck;
    }

    public void loadCards()
    {
        string json = "";
        string fullPathWithExt = Application.persistentDataPath + DefaultCardValues.DECK_PATH +
                                 DefaultCardValues.DECK_EXTENSION;
        if (File.Exists(fullPathWithExt))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(fullPathWithExt, FileMode.Open);
            json = (string) bf.Deserialize(file);
            file.Close();
        }
        else
        {
            json = Resources.Load<TextAsset>(DefaultCardValues.TEST_DECK).text;
        }

        CardArray cardsFromJson = JsonUtility.FromJson<CardArray>(json);
        foreach (CardScript card in cardsFromJson.card_array)
        {
            Debug.Log(card.card_name);
            deckManager.allCards.Add(card);
        }
    }
    public void AddToCardList(CardScript card)
    {
        deck.getCardList().Add(card);
    }
}
