using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Foodfactory
public class Foodfactory : Building
{
    // Building-specific stats are set in the constructor.
    public Foodfactory()
    {
        spritePath = "Sprites/Foodfactory4k";
        stringName = "Foodfactory";
        cost = 1000;
        availableJobs = 6;
        resources["cash"].delta = 0;
        resources["cash"].upkeep = 10;
        resources["pollution"].upkeep = -3;
        resources["energy"].delta = -4;
        resources["food"].delta = 8;
    }

    // Set sprite and/or gameobject for rendering, this method is useful as context can be used to determine the desired sprite/gameobject
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.gameObject = Resources.Load<GameObject>("Prefabs/Buildings/Foodfactory");
    }

    // Refresh yourself
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        tilemap.RefreshTile(position);
    }
}