using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class PreferenceManagementScript : MonoBehaviour
{
    public GameObject nextButton;
    public GameObject prevButton;
    // Start is called before the first frame update
    void Start()
    {
        if(Application.platform == RuntimePlatform.Android){
            deactivateButtons();
            if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead)||
                !Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
            {
                Permission.RequestUserPermission(Permission.ExternalStorageRead);
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            }
        }
    }

    private void deactivateButtons()
    {
        nextButton.SetActive(false);
        prevButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
