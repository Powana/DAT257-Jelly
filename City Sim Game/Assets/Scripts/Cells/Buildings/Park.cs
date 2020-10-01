using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Park District
public class Park : Building
{
	// Building-specific stats are set in the constructor.
	public Park()
	{
		stringName = "Park";
		cost = 100;
		resources["cash"].upkeep = 1;
		resources["pollution"].upkeep = 2;
		resources["cash"].delta = 10;
		resources["food"].delta = 20;
		availableJobs = 10;
		takenJobs = 0;
	}

	// Set sprite and/or gameobject for rendering, this method is useful as context can be used to determine the desired sprite/gameobject
	public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
	{
		tileData.gameObject = Resources.Load<GameObject>("Prefabs/Buildings/Park");
	}

	// Refresh yourself
	public override void RefreshTile(Vector3Int position, ITilemap tilemap)
	{
		tilemap.RefreshTile(position);
	}
}
