using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollScript : MonoBehaviour
{

    public ScrollRect scrollView;
    public GameObject scrollContent;
    public GameObject scrollItemPrefab;
    public List<GameObject> currentList;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {



    }

    public void GenerateItemList(List<CardScript> cards)
    {
        clearList();
        int numberOfItems = cards.Count;
        for (int i = 0; i < numberOfItems; i++)
        {
            GameObject scrollItemObj = Instantiate(scrollItemPrefab);
            scrollItemObj.transform.SetParent(scrollContent.transform, false);
            Sprite tmpSprite = GetComponent<CardDisplayScript>().ConvertImageFromString(cards[i].b64image);
            scrollItemObj.GetComponentInChildren<Image>().sprite = tmpSprite;
            scrollItemObj.GetComponentInChildren<Text>().text = cards[i].card_name + ", " +
            " amount allowed: " + cards[i].age +
            ", description: " + cards[i].card_description;
            currentList.Add(scrollItemObj);
        }
    }

    private void clearList()
    {
        foreach (GameObject item in currentList)
        {
            Destroy(item.gameObject);
        }
    }
}
