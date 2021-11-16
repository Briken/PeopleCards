using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSwapperScript : MonoBehaviour
{
    private int currentCard = 0;

  public CardDisplayScript _displayScript;

  public CardLoaderScript _loaderScript;
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
        if (currentCard == _loaderScript.getCardList().Count - 1)
        {
            currentCard = 0;
        }
        else
        {
            currentCard++;
        }
        _displayScript.DisplayNewCard(_loaderScript.getCardList()[currentCard]);
    }

    public void DisplayPrevCard()
    {
        if (currentCard == 0)
        {
            currentCard = _loaderScript.getCardList().Count - 1;
        }
        else
        {
            currentCard++;
        }
        _displayScript.DisplayNewCard(_loaderScript.getCardList()[currentCard]);
    }
}
