using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Residential District
public class Residential : Building
{
	// Building-specific stats are set in the constructor.
	public Residential()
	{
		spritePath = "Sprites/Residential4k";
		stringName = "Residential";
		cost = 100;

		// Available residences
		resources["residences"].value = 250;

		// Upkeep
		resources["cash"].upkeep = 1;
		resources["pollution"].upkeep = 1;

		// Potential production
		resources["food"].delta = 10;

		// Jobs
		availableJobs = 0;
	}

	// Set sprite and/or gameobject for rendering, this method is useful as context can be used to determine the desired sprite/gameobject
	public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
	{
		tileData.gameObject = Resources.Load<GameObject>("Prefabs/Buildings/Residential");
	}

	// Refresh yourself
	public override void RefreshTile(Vector3Int position, ITilemap tilemap)
	{
		tilemap.RefreshTile(position);
	}
}
