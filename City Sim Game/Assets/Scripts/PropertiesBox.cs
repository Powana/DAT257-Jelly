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
    public GameObject propertiesPanel;
    private Cell clickedTile;
    private int currentEmployees;
    private int maxEmployees;
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
        // Receive cliked tile from map
        clickedTile = map.SendCell();
        // Debug.Log(clickedTile);
        if(!propertiesPanel.activeInHierarchy)
        {
            showInformation();
        }
      
    }
    
    // Close the panel
    public void closePanel()
    {
        propertiesPanel.gameObject.SetActive(false);
    }

    public void showInformation()
    {
        if (clickedTile is Building)
        {
            propertiesPanel.gameObject.SetActive(true);
            Building current = (Building)clickedTile;
            name.text = current.getName();
            cost.text = "Cost:" + current.getCost().ToString();
            resources = current.resources;
            foreach (KeyValuePair<string, Resource> kvp in current.resources)
            {
                reso.text += kvp.Key + ":" + kvp.Value.delta + "\n";
            }



        }
    }
}
