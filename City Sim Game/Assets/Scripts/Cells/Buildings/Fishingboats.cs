using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Fishingboats
public class Fishingboats : Waterbuilding
{
    // Building-specific stats are set in the constructor.
    public Fishingboats()
    {
        spritePath = "Sprites/Fishboats4k";
        stringName = "Fishingboats";
        cost = 30000;
        resources["cash"].upkeep = 2;
        resources["pollution"].upkeep = -1;
        resources["cash"].delta = 0;
        resources["energy"].delta = 5;
        resources["food"].delta = 100;
        availableJobs = 6;
        takenJobs = 0;
    }

    // Set sprite and/or gameobject for rendering, this method is useful as context can be used to determine the desired sprite/gameobject
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.gameObject = Resources.Load<GameObject>("Prefabs/Buildings/Fishingboats");
    }

    // Refresh yourself
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        tilemap.RefreshTile(position);
    }
}