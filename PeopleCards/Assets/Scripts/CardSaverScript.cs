using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CardSaverScript : MonoBehaviour
{
    private bool toggleFields = false;
    private CardScript tempCard;
    public GameObject newCardFields; 
    public GameObject nextButton;
    public GameObject prevButton;
    public TextAsset jsonFile;
    public CardLoaderScript loadScript;
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

    public void UpdateCardImage(InputField val)
    {
        tempCard.b64image = SaveImageAsBase64(val.text);
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
        nextButton.SetActive(!toggleFields);
        prevButton.SetActive(!toggleFields);
    }

    private void SaveCard(CardScript card)
    {
        string fullPathWithExt = Application.persistentDataPath + DefaultCardValues.DECK_PATH +
                                 DefaultCardValues.DECK_EXTENSION;
        BinaryFormatter bf = new BinaryFormatter();
        loadScript.AddToCardList(card);
        String jsonString = JsonUtility.ToJson(loadScript.getCardArray());
        FileStream file;
        if (!File.Exists(DefaultCardValues.DECK_PATH + DefaultCardValues.DECK_EXTENSION))
        {
            if (!Directory.Exists(Application.persistentDataPath + DefaultCardValues.JSON_DIR))
            {
                Directory.CreateDirectory(Application.persistentDataPath + DefaultCardValues.JSON_DIR);
            }

            file = File.Create(fullPathWithExt);
        }
        else
        {
            file = File.Open(Application.persistentDataPath, FileMode.Open);
        }

        bf.Serialize(file, jsonString);
        file.Close();
    }
}
