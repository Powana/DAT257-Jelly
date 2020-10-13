﻿using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
//using System.Diagnostics;
//using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class PropertiesBox : MonoBehaviour
{
	public Map map;
	public Text name;
	public Text cost;
	public Text reso;
	public Text employees;
	public GameObject propertiesPanel;
	private Building current;
	private Cell clickedTile;
	private Dictionary<string, Resource> resources;

	// Start is called before the first frame update
	void Start()
	{
		// Hide the panel at start
		propertiesPanel.gameObject.SetActive(false);
	}

	void Update()
	{
		// Only check for clicked tile if the mouse has been clicked
		if (!propertiesPanel.activeInHierarchy && Input.GetMouseButtonDown(0)) {
			// Receive clicked tile from map if it is a building
			// open the panel
			clickedTile = map.SendCell();

			if(clickedTile is Building) {
				ShowInformation();
			}
		} else if (Input.GetKeyDown("escape")) {
			ClosePanel();
		}

	}

	// Close the panel and clear all text fields.
	public void ClosePanel()
	{
		propertiesPanel.gameObject.SetActive(false);
		name.text = "";
		cost.text = "";
		reso.text = "";
		employees.text = "";
	}

	// Print information about the current building in the properties panel.
	public void ShowInformation()
	{
		// Show the panel if it is already displayed
		if (!propertiesPanel.activeInHierarchy) {
			propertiesPanel.gameObject.SetActive(true);
			current = (Building)clickedTile;
			name.text = current.GetName();
			cost.text = "Price:" + current.GetCost().ToString();

			// Print all resources of the bulding.
			foreach (KeyValuePair<string, Resource> kvp in current.resources) {
				reso.text += kvp.Key + ":" + kvp.Value.delta + "\n";
			}

			UpdateInformation(current);
		}
	}

	public void UpdateInformation(Building building)
	{
		employees.text = "Employees: \n" + building.resources["workers"].value.ToString() + "/" + building.availableJobs.ToString();
	}

	public void UpdateInformation()
	{
		UpdateInformation(current);
	}

	// Add employee to building
	public void HireWorker()
	{
		Map.resourceManager.HireWorkers(clickedTile, 1);
		UpdateInformation((Building)clickedTile);
	}

	// Remove employee to building
	public void FireWorker()
	{
		Map.resourceManager.FireWorkers(clickedTile, 1);
		UpdateInformation((Building)clickedTile);
	}
}
