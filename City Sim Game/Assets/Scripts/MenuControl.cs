using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public Map map;
    public PlayerProfile player;
    public void ButtonStart()
    {
        
        SceneManager.LoadScene(2);
        //player = new PlayerProfile("name", map.GetManager());

    }

    public void ButtonLoad()
    {
        //player = SaveData.Load();
        SceneManager.LoadScene(1);
    }

    public void ButtonCredits()
    {
        SceneManager.LoadScene(2);
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }

}   
