using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SearchByName(string name)
    {
        List<CardScript> fullList = GetComponent<CardLoaderScript>().getCardList();
        List<CardScript> tmpList = new List<CardScript>();
        foreach (CardScript card in fullList)
        {
            if (card.card_name.ToLowerInvariant().Contains(name.ToLowerInvariant()))
            {
                tmpList.Add(card);
            }
        }
        GetComponent<ScrollScript>().GenerateItemList(tmpList);
    }
}
