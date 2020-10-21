using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This class is made to controll the information window 
public class InfoWindowController : MonoBehaviour
{

    public AudioSource ClickSound;


// This method called when play button is clicked 
public void PlayButton()
    {
        ClickSound.Play();
        SceneManager.LoadScene(1);

    }


}
