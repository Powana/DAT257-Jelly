using System.Collections;
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
    private Cell clickedTile;
    private Dictionary<string, Resource> resources;
    
    

    // Start is called before the first frame update
    void Start()
    {
        // Hide the panel at start
        propertiesPanel.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        // Receive cliked tile from map if it is a building
        // open the panel
        clickedTile = map.SendCell();
        if(clickedTile is Building)
        {
            showInformation();
        }
      
    }
    
    // Close the panel
    public void closePanel()
    {
        propertiesPanel.gameObject.SetActive(false);
        name.text = "";
        cost.text = "";
        reso.text = "";
        employees.text = "";
    }

    public void showInformation()
    {
        if (!propertiesPanel.activeInHierarchy)
        {
            propertiesPanel.gameObject.SetActive(true);
            Building current = (Building)clickedTile;
            name.text = current.getName();
            cost.text = "Price:" + current.getCost().ToString();
          
            foreach (KeyValuePair<string, Resource> kvp in current.resources)
            {
                reso.text += kvp.Key + ":" + kvp.Value.delta + "\n";
            }

            employees.text = "Employees: \n" + current.resources["workers"].value.ToString() + "/" + current.availableJobs.ToString();

        }
    }
    // Add employee to building
    public void addEmployee()
    {
        clickedTile.HireWorkers(1);
    }
    // Remove employee to building
    public void removeEmployee()
    {
        clickedTile.FireWorkers(1);
    }
}
