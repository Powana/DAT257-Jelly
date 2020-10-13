using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Abstract class to be used when implenting buildings.
abstract public class Building : Cell
{
	// Name of the building, as used in shop interface etc.
	protected string stringName;

	// Building-specific stats are set in the constructor.
	protected Building()
	{
		// Use placeholder constructer to make sure constructer is defined in inhereted building classes.
		stringName = "UNDEFINED";
	}

	public override bool validPosition(Tilemap tilemap, Vector3Int pos)
	{
		if (tilemap.GetTile(pos) is Grass)
		{
			// Make sure there is at least one road
			if (Map.GetSurroundingWallCount<Road>(tilemap, pos.x, pos.y) >= 1)
			{
				return true;
			}
		}
		return false;
	}

	public string GetName(){
		return stringName;
	}

}
