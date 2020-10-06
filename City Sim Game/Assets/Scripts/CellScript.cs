using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Script that can be attached to cell gameobjects for added functionality.
public class CellScript : MonoBehaviour
{
    private SpriteRenderer r;
    private Color startcolour;

	private Sprite startSprite;
	private GameObject TilemapObject;
	private Map MapScript;
	private Cell held;
	private Tilemap TilemapComponent;
	private Vector3Int gridPos;
	private bool previewOnMap;

	private Color red = new Color(1f,0.3f,0.3f,0.7f);
	private Color green = new Color(0.3f,1f,0.3f,0.7f);

    // Start is called before the first frame update
    void Start()
    {
        TilemapObject = GameObject.Find("Tilemap");
		MapScript = TilemapObject.GetComponent<Map>();
		TilemapComponent = TilemapObject.GetComponent<Tilemap>();
    }
	
    // Update is called once per frame
    void Update()
    {
		held = MapScript.getHeld();
		if(held==null && previewOnMap==true){
			// Reset tint
        	r.color = startcolour;
			// Reset sprite
			r.sprite = startSprite;
			previewOnMap=false;
		}
    }

    void OnMouseEnter()
    {	
		r = GetComponent<SpriteRenderer>();
		// In case of road, get child renderer
       	if (r == null) r = GetComponentInChildren<SpriteRenderer>();
	 	// Save original tint and sprite
       	startcolour = r.color;
		startSprite = r.sprite;
		if(held==null){
			//Add a slightly dark tint to show highlight
        	r.color = new Color(0.8f, 0.8f, 0.8f);
		}else{
			//Determine if hovered tile is a valid placement for the currently held cell
			gridPos = MapScript.getGridPosition();
			if (held.validPosition(TilemapComponent,gridPos)){
					//Green if valid position.
					r.color=green;
				}else{
					//Red if invalid position.
					r.color=red;
				}
			//PreviewOnMap flag is set to help determine if preview needs to be cleared in update method.
			previewOnMap=true;
			r.sprite = Resources.Load<Sprite>(held.getSpritePath());
		}
    }
    void OnMouseExit()
    {
		 	// Reset tint
        	r.color = startcolour;
			// Reset sprite
			r.sprite = startSprite;
    }
}