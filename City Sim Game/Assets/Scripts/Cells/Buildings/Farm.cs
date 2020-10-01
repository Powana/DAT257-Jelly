using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Farm District
public class Farm : Building
{
	// Building-specific stats are set in the constructor.
	public Farm()
	{
		stringName = "Farm";
		cost = 100;
		resources["cash"].upkeep = 1;
	}

	// Set sprite and/or gameobject for rendering, this method is useful as context can be used to determine the desired sprite/gameobject
	public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
	{
		tileData.gameObject = Resources.Load<GameObject>("Prefabs/Buildings/Farm");
	}

	// Refresh yourself
	public override void RefreshTile(Vector3Int position, ITilemap tilemap)
	{
		tilemap.RefreshTile(position);
	}
}
