using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This class handel the game over scene
public class GameOverControllar : MonoBehaviour
{
    public AudioSource ClickSound;

   

    //This metod for play agin button 
    public void PlayButton()
    {
        ClickSound.Play();
        SceneManager.LoadScene(1);

    }
    //This metod for going back to main menu button
    public void MainMenuButton()
    {
        ClickSound.Play();
        SceneManager.LoadScene(0);

    }
    //This metod for quiting the game button
    public void QuitButton()
    {
        ClickSound.Play();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }

}


