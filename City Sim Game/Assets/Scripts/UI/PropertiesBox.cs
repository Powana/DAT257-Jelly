using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
//using System.Diagnostics;
//using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PropertiesBox : MonoBehaviour
{
	public Map map;
	public Text name;
	public Text cost;
	public Text cash;
	public Text food;
	public Text energy;
	public Text pollution;
	public Text employees;
	public Text residences;
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
		// Only check for clicked tile if the mouse has been clicked and not on the UI
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
			// Receive clicked tile from map if it is a building
			// open the panel
			clickedTile = map.SendCell();

			if(clickedTile is Building) {
				ClosePanel();
				ShowInformation();
                Map.openPanel = true;
			}
		} else if (Input.GetKeyDown("escape") || (Input.GetMouseButtonDown(1))) {
			ClosePanel();
            Map.openPanel = false;
        }

    }

	// Close the panel and clear all text fields.
	public void ClosePanel()
	{
		propertiesPanel.gameObject.SetActive(false);
		name.text = "";
		cost.text = "";
		cash.text = "";
		food.text = "";
		energy.text = "";
		pollution.text = "";
		employees.text = "";
		residences.text = "";
	}

	// Print information about the current building in the properties panel.
	public void ShowInformation()
	{
		// Show the panel if it is already displayed
		if (!propertiesPanel.activeInHierarchy) {
			propertiesPanel.gameObject.SetActive(true);
			current = (Building)clickedTile;
			name.text = current.GetName();
			cost.text = "Cost: " + current.GetCost().ToString();
			cash.text = "Cash:" + current.resources["cash"].delta;
			food.text = "Food:" + current.resources["food"].delta;
			energy.text = "Energy:" + (-current.resources["energy"].upkeep + current.resources["energy"].delta * (current.resources["workers"].value / current.availableJobs));
			pollution.text = "Pollution:" + current.resources["pollution"].upkeep;

			if(current is Residential)
            {
				residences.text = "Residences:" + current.resources["residences"].value;
            }
			

			UpdateInformation(current);
		}
	}

	public void UpdateInformation(Building building)
	{
		employees.text = "Employees: \n" + building.resources["workers"].value.ToString() + " / " + building.availableJobs.ToString();
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
