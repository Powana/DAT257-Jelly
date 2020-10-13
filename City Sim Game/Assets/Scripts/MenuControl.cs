using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public Map map;
    public LoadGamePanel panel;
    public GameObject CreditsPanel;

    public void Start()
    {
        CreditsPanel.gameObject.SetActive(false);

    }
    public void ButtonStart()
    {
        
        SceneManager.LoadScene(1);
        //player = new PlayerProfile("name", map.GetManager());

    }

    public void ButtonLoad()
    {

        panel.openPanel();
    }

    public void ButtonCredits()
    {
        if(CreditsPanel.gameObject.activeSelf == false)
        {
            CreditsPanel.gameObject.SetActive(true);
        }
        else
        {
            CreditsPanel.gameObject.SetActive(false);
        }
        
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }

}   
