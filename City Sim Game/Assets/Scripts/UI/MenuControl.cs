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
    public AudioSource ClickSound;
    public void Start()
    {
        CreditsPanel.gameObject.SetActive(false);
        LoadPanel.gameObject.SetActive(false);

    }
    public void ButtonStart()
    {
        ClickSound.Play();
        SceneManager.LoadScene(1);

    }

    public void ButtonLoad()
    {
        ClickSound.Play();

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
        ClickSound.Play();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                 Application.Quit();
#endif
    }

}   
