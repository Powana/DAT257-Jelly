using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that can be attached to cell gameobjects for added functionality.
public class CellScript : MonoBehaviour
{

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
        // Save original tint
        startcolour = GetComponent<SpriteRenderer>().color;
        // Add a slightly dark tint to show highlight
        GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f);
    }
    void OnMouseExit()
    {
        // Reset tint
        GetComponent<SpriteRenderer>().color = startcolour;
    }
}