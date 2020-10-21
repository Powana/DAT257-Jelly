using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{

    public GameObject pauseMenu;

    public void open()
    {
        pauseMenu.SetActive(true);
    }

    public void close()
    {
        pauseMenu.SetActive(false);
    }

    public void help()
    {
        GameObject.Find("InfoWindowMaster").transform.GetChild(0).gameObject.SetActive(true);
        close();
    }

    public void quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
