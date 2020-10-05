using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that can be attached to cell gameobjects for added functionality.
public class CellScript : MonoBehaviour
{
    private SpriteRenderer r;
    private Color startcolour;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        
        r = GetComponent<SpriteRenderer>();
        // In case of road, get child renderer
        if (r == null) r = GetComponentInChildren<SpriteRenderer>();
        // Save original tint
        startcolour = r.color;
        // Add a slightly dark tint to show highlight
        r.color = new Color(0.8f, 0.8f, 0.8f);
    }
    void OnMouseExit()
    {
        // Reset tint
        r.color = startcolour;
    }
}