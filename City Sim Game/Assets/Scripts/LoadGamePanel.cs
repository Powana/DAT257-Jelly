using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGamePanel : MonoBehaviour
{
    public GameObject LoadPanel;
  

    public void openPanel()
    {
        if(LoadPanel != null)
        {
            LoadPanel.gameObject.SetActive(true);
        
        }
    }

    public void closePanel()
    {
        LoadPanel.gameObject.SetActive(false);
    }
}
