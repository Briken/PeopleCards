using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControlScript : MonoBehaviour
{
    private Vector2 startTouchPos, endTouchPos;
    private CardSwapperScript swapper;

    void Start()
    {
        swapper = GetComponent<CardSwapperScript>();
    } 
    
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPos = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPos = Input.GetTouch(0).position;
        }

        if (endTouchPos.x < startTouchPos.x && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            swapper.DisplayPrevCard();
            nullTouchPos();
        }
        if (endTouchPos.x > startTouchPos.x && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            swapper.DisplayNextCard();
            nullTouchPos();
        }
    }

    private void nullTouchPos()
    {
            startTouchPos =new Vector2();
            endTouchPos = new Vector2();
    }
}
