using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InfoWindowControllar : MonoBehaviour
{

    public AudioSource ClickSound;




public void PlayButton()
    {
        ClickSound.Play();
        SceneManager.LoadScene(1);

    }




}
