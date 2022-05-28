using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class CardSaverScript : MonoBehaviour
{
    private bool toggleFields = false;
    private CardScript tempCard;
    public GameObject newCardFields; 
    public GameObject nextButton;
    public GameObject prevButton;
    public TextAsset jsonFile;
    public CardLoaderScript loadScript;
    private DeckPersistenceScript deckManager;
    
    public string SaveImageAsBase64(string filePath)
    {
        byte[] fileData;
        string retVal = "";
        if (File.Exists(filePath)) 
        {
            fileData = File.ReadAllBytes(filePath);
            retVal = Convert.ToBase64String(fileData);
        }
        else
        {
            retVal = "nullImage";
        }

        return retVal;
    }

    public void UpdateCardName(InputField val)
    {
        tempCard.card_name = val.text;
    }

    public void UpdateCardDesc(InputField val)
    {
        tempCard.card_description = val.text;
    }

    public void UpdateCardAge(InputField val)
    {
        try
        {
            tempCard.age = Int32.Parse(val.text);
        } catch (FormatException ex)
        {
            tempCard.age = -1;
        }
    }

    public void UpdateCardImage()
    {
        if (Application.isEditor)
        {
            tempCard.b64image = DefaultCardValues.NO_IMG_FOUND;
        }
        else
        {
            NativeGallery.GetImageFromGallery((path) =>
            {
                if (File.Exists(path)) 
                {
                    byte[] imageData = File.ReadAllBytes(path);
                    tempCard.b64image = Convert.ToBase64String(imageData);
                }
                else
                {
                    tempCard.b64image = "nullImage";
                }
            });
        }
    }

    public void ActivateFieldsDeactivateButtons()
    {
        if (!toggleFields)
        {
            tempCard = new CardScript();
            toggleFields = !toggleFields;
        }
        else
        {
            SaveCard(tempCard);
            toggleFields = !toggleFields;
        }
        newCardFields.SetActive(toggleFields);
        if(Application.platform != RuntimePlatform.Android){
            nextButton.SetActive(!toggleFields);
            prevButton.SetActive(!toggleFields);
        }
    }

    private void SaveCard(CardScript card)
    {
        if (!deckManager.allCards.Contains(card))
        {
            saveCardToFullList(card);
            card.listofdecks = new string[2]{DefaultCardValues.ALL_DECK_NAME, ""};
        }
        saveCardToActiveDeck(card);
    }

    private void saveCardToActiveDeck(CardScript card)
    {
        deckManager.getActiveDeck().getCardList().Add(card);
        if (card.listofdecks.Last().Equals(""))
        {
            card.listofdecks[card.listofdecks.Length-1] = deckManager.getActiveDeck().getDeckName();
        }
        else
        {
            string[] newlist = new string[card.listofdecks.Length + 1];
            for (int i = 0 ; i < card.listofdecks.Length; i++)
            {
                newlist[i] = card.listofdecks[i];
            }
            newlist[card.listofdecks.Length] = deckManager.getActiveDeck().getDeckName();
            card.listofdecks = newlist;
        }
    }

    private void saveCardToFullList(CardScript card)
    {
        deckManager.allCards.Add(card);
    }

    private void SaveDeck(CardScript card)
    {
        string fullPathWithExt = Application.persistentDataPath + 
                                 DefaultCardValues.DECK_PATH +
                                 DefaultCardValues.DECK_EXTENSION;
        string editorPath = DefaultCardValues.DECK_PATH +
                            DefaultCardValues.DECK_EXTENSION;
        string directoryPath =Application.persistentDataPath + 
                              DefaultCardValues.JSON_DIR;
        BinaryFormatter bf = new BinaryFormatter();
        loadScript.AddToCardList(card);
        String jsonString = JsonUtility.ToJson(deckManager.allCards.ToArray());
        FileStream file;
        if (Application.isEditor)
        {
            fullPathWithExt = editorPath;
            directoryPath = DefaultCardValues.DECK_PATH;
        }
        if (!File.Exists(fullPathWithExt))
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            file = File.Create(fullPathWithExt);
        }
        else
        {
            file = File.Open(fullPathWithExt, FileMode.Open);
        }

        bf.Serialize(file, jsonString);
        file.Close();
    }
}
