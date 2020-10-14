using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGamePanel : MonoBehaviour
{
    public GameObject LoadPanel;

    public void Start()
    {
        //Hide the panel at startup
        LoadPanel.gameObject.SetActive(false);
    }
    //Use this function to open the panel of saved games
    public void openPanel()
    {
        if(LoadPanel != null)
        {
            LoadPanel.gameObject.SetActive(true);
        
        }
    }
    //Use this function to close the panel
    public void closePanel()
    {
        LoadPanel.gameObject.SetActive(false);
    }
}
