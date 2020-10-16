﻿using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.Tilemaps;

//This class is for making houses so people can live in
public class House : Building
{
	public House()
	{
		spritePath = "";
		stringName = "Generic House";
		cost = 2000;

		// Free residences
		resources["residences"].value = 250;

		// Upkeep (costs per tick)
		resources["cash"].upkeep = 10;

		// Total potential production if all job spots are filled.
		resources["pollution"].delta = 1;
		resources["energy"].delta = -4;
	}

	// Set sprite and/or gameobject for rendering, this method is useful as context can be used to determine the desired sprite/gameobject
	public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
	{
		tileData.gameObject = Resources.Load<GameObject>("Prefabs/Buildings/PrefabName");
	}

	// Refresh yourself
	public override void RefreshTile(Vector3Int position, ITilemap tilemap)
	{
		tilemap.RefreshTile(position);
	}
}
