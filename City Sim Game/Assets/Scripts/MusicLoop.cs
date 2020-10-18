using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicLoop : MonoBehaviour
{
	AudioSource m_AudioSource;
	public Toggle m_Toggle;
	public Button m_Button;

	void Start()
	{
		//Fetch the AudioSource component of the GameObject (make sure there is one in the Inspector)
		m_AudioSource = GetComponent<AudioSource>();
		//Stop the Audio playing
		m_AudioSource.Stop();
		//Call the PlayButton function when you click this Button
		m_Button.onClick.AddListener(PlayButton);
	}

	void Update()
	{
		//Turn the loop on and off depending on the Toggle status
		m_AudioSource.loop = m_Toggle.isOn;
	}

	//This plays the Audio clip when you press the Button
	void PlayButton()
	{
		m_AudioSource.Play();
	}
}
