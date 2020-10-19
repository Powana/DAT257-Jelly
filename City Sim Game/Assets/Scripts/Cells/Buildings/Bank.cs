using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Bank
public class Bank : Building
{
    // Building-specific stats are set in the constructor.
    public Bank()
    {
        spritePath = "Sprites/Bank4k";
        stringName = "Bank";
        cost = 2000;
        availableJobs = 6;
        resources["cash"].delta = 200;
        resources["cash"].upkeep = 10;
        resources["pollution"].upkeep = 0;
        resources["energy"].delta = -3;
        
    }

    // Set sprite and/or gameobject for rendering, this method is useful as context can be used to determine the desired sprite/gameobject
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.gameObject = Resources.Load<GameObject>("Prefabs/Buildings/Bank");
    }

    // Refresh yourself
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        tilemap.RefreshTile(position);
    }
}