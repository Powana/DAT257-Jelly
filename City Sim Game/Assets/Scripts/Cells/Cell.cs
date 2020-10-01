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

    // Check if the given position is valid for cell placement
    // TODO: Maybe this should return a string to be used in the warning message for invalid placement.
    public abstract bool validPosition(Tilemap tilemap, Vector3Int pos);
    
    public bool validPosition(Tilemap tilemap, int x, int y)
    {
        return validPosition(tilemap, new Vector3Int(x, y, 0));
    }
    
}
