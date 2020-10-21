using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopInfoPanel : MonoBehaviour
{

	public GameObject ShopPanel;
	public Text name;
	public Text cost;
	public Image image;
	public Text resourcesUpkeep;
	public Text resourcesDelta;
	private Building current;
	private GameObject TilemapObject;
	private Map MapScript;
	// Start is called before the first frame update
	void Start()
	{
		TilemapObject = GameObject.Find("Tilemap");
		MapScript = TilemapObject.GetComponent<Map>();
	}

	// Update is called once per frame
	void Update()
	{
	  if(MapScript.getHeld()==null){
		  hideInfo();
		}
	}

	public void showInfo(){
		current = (Building)MapScript.getHeld();
		name.text=current.GetName();
		cost.text="Cost: " + current.GetCost()+" SEK";
		image.sprite = Resources.Load<Sprite>(current.getSpritePath());
		image.color = new Color(255,255,255,255);

		resourcesUpkeep.text="Upkeep: \n";
		resourcesDelta.text="Production at full capacity: \n";
		// Print resource upkeep of buiding
			foreach (KeyValuePair<string, Resource> kvp in current.resources)
			{
				int upkeep = kvp.Value.upkeep;

				if(upkeep!=0)
					resourcesUpkeep.text += "   " + kvp.Key + ":  " + (kvp.Key == "pollution" ? -upkeep : upkeep) + "\n";
				if(kvp.Value.delta!=0)
					 resourcesDelta.text  += "   " + kvp.Key + ":  " + kvp.Value.delta + "\n";
			}

	}

	public void hideInfo(){
		name.text="";
		cost.text="";
		resourcesUpkeep.text="";
		resourcesDelta.text="";
		image.sprite = null;
		image.color = new Color(0,0,0,0);
	}

}
