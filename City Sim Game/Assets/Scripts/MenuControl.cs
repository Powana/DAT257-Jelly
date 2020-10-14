using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    
    public GameObject CreditsPanel;
    public GameObject LoadPanel;

    public void Start()
    {
        CreditsPanel.gameObject.SetActive(false);
        LoadPanel.gameObject.SetActive(false);

    }
    public void ButtonStart()
    {
        
        SceneManager.LoadScene(2);
        //player = new PlayerProfile("name", map.GetManager());

    }

    public void ButtonLoad()
    {
        if (LoadPanel.gameObject.activeSelf)
        {
            LoadPanel.gameObject.SetActive(false);
        }
        else
        {
            LoadPanel.gameObject.SetActive(true);
        }
    }

    public void ButtonCredits()
    {
        if(CreditsPanel.gameObject.activeSelf)
        {
            CreditsPanel.gameObject.SetActive(false);
        }
        else
        {
            CreditsPanel.gameObject.SetActive(true);
        }
        
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }

}   
