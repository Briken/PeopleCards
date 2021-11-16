using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class CardDisplayScript : MonoBehaviour
{
    public Image art;
    public Text ageText;
    public Text descriptionText;
    public Text nameText; 
    

    // Start is called before the first frame update
    void Start()
    {
    }
    
    public string ConvertSpriteIntoBase64(Texture2D sprite)
    {
        string retVal = "";
        byte[] imageData = sprite.GetRawTextureData();
        retVal = Convert.ToBase64String(imageData);
        return retVal;
    }
    
    public Sprite ConvertImageFromString(string b64image)
    {
        byte[] imageBytes = Convert.FromBase64String(b64image);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage( imageBytes );
        return Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

    public void DisplayNewCard(CardScript card)
    {
        if (card.age == -1)
        {
            ageText.text = DefaultCardValues.NO_AGE_FOUND;
        }
        else
        {
            ageText.text = $"{card.age}";   
        }
        nameText.text = card.card_name;
        descriptionText.text = card.card_description;
        art.sprite = ConvertImageFromString(card.b64image);
    }
}
