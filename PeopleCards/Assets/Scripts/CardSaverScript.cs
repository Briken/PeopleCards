using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CardSaverScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public string SaveImageAsBase64(string filePath)
    {
        byte[] fileData;
        string retVal = "";
        if (File.Exists(filePath)) 
        {
            fileData = File.ReadAllBytes(filePath);
            retVal = Convert.ToBase64String(fileData);
        }

        return retVal;
    }

    
}
