using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Museum
public class Museum : Building
{
    // Building-specific stats are set in the constructor.
    public Museum()
    {
        spritePath = "Sprites/museum4k";
        stringName = "Museum";
        cost = 1500;
        availableJobs = 5;
        resources["cash"].delta = 100;
        resources["cash"].upkeep = 10;
        resources["pollution"].upkeep = -1;
        resources["energy"].delta = 0;
        resources["food"].delta = -3;
    }

    // Set sprite and/or gameobject for rendering, this method is useful as context can be used to determine the desired sprite/gameobject
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.gameObject = Resources.Load<GameObject>("Prefabs/Buildings/Museum");
    }

    // Refresh yourself
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        tilemap.RefreshTile(position);
    }
}