using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Cell : Tile
{
	public int cost = 0;
	public Dictionary<string, Resource> resources;


    // Initialize resources dictionary.
    protected Cell()
	{
		resources = new Dictionary<string, Resource>();

		foreach (string resource in new string[] {
				"cash", "population", "food", "energy", "pollution"
			}) {
			resources.Add(resource, new Resource(resource));
		}
	}
    

    
}
