using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Windpower
public class Wind : Building
{
    // Building-specific stats are set in the constructor.
    public Wind()
    {
        spritePath = "Sprites/Windpower4k";
        stringName = "Windpower";
        cost = 100000;
        availableJobs = 8;
        resources["cash"].delta = 100;
        resources["cash"].upkeep = 10;
        resources["pollution"].delta = -2;
        resources["energy"].delta = 10;
        resources["food"].delta = -8;
    }

    // Set sprite and/or gameobject for rendering, this method is useful as context can be used to determine the desired sprite/gameobject
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.gameObject = Resources.Load<GameObject>("Prefabs/Buildings/Wind");
    }

    // Refresh yourself
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        tilemap.RefreshTile(position);
    }
}