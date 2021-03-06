﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuInteraction : MonoBehaviour
{

	public GameObject EnergyPanel;
	public GameObject EnvironmentPanel;
	public GameObject CommercePanel;
	public GameObject DistrictPanel;
	public GameObject infoPanel;
	public GameObject gameOver;

	public GameObject MenuPanel;

	private GameObject CurrentlyOpenPanel;

    // Start is called before the first frame update
    void Start()
    {
		EnvironmentPanel.SetActive(false);
		CommercePanel.SetActive(false);
		DistrictPanel.SetActive(false);
		CurrentlyOpenPanel=EnergyPanel;
		gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		// If lake had no health left, exit the game.
		if ((Map.resourceManager.resources["lake"].value <= 0) || (Map.resourceManager.resources["population"].value <= 0)) {
			openPanel(gameOver);
		}
    }

	public void openPanel(GameObject Panel){
		
		if(!Panel.activeInHierarchy){
			CurrentlyOpenPanel.SetActive(false);
			Panel.SetActive(true);
			CurrentlyOpenPanel=Panel;
		}
		
	}


	public void minimizeMenu(){
		if(!MenuPanel.activeInHierarchy==true){
			MenuPanel.SetActive(true);
		}else{
			MenuPanel.SetActive(false);
		}
	}
	
	public void hideInfoPanel() {
		infoPanel.SetActive(false);
	}

}
