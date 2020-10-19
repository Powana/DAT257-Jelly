using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Recycle
public class Recycle : Building
{
    // Building-specific stats are set in the constructor.
    public Recycle()
    {
        spritePath = "Sprites/Recycle4k";
        stringName = "Recycle";
        cost = 80;
        availableJobs = 1;
        resources["cash"].delta = 0;
        resources["cash"].upkeep = 10;
        resources["pollution"].upkeep = 3;
        resources["energy"].delta = 0;
        
    }

    // Set sprite and/or gameobject for rendering, this method is useful as context can be used to determine the desired sprite/gameobject
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.gameObject = Resources.Load<GameObject>("Prefabs/Buildings/Recycle");
    }

    // Refresh yourself
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        tilemap.RefreshTile(position);
    }
}