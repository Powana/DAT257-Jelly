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

	private GameObject CurrentlyOpenPanel;

    // Start is called before the first frame update
    void Start()
    {
		EnvironmentPanel.SetActive(false);
		CommercePanel.SetActive(false);
		DistrictPanel.SetActive(false);
		CurrentlyOpenPanel=EnergyPanel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void openPanel(GameObject Panel){
		
		if(!Panel.activeInHierarchy){
			CurrentlyOpenPanel.SetActive(false);
			Panel.SetActive(true);
			CurrentlyOpenPanel=Panel;
		}
	}

}