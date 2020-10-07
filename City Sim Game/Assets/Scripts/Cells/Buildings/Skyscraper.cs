using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Commersial Skyscraper
public class Skyscraper : Building
{
    // Building-specific stats are set in the constructor.
    public Skyscraper()
    {   
        spritePath = "Sprites/skyscrape4k";
        stringName = "Skyscraper";
        cost = 1200;
        availableJobs = 5;
        resources["cash"].delta = 100;
        resources["cash"].upkeep = 10;
        resources["pollution"].delta = -2;
        resources["energy"].delta = -4;
        resources["food"].delta = -5;
    }

    // Set sprite and/or gameobject for rendering, this method is useful as context can be used to determine the desired sprite/gameobject
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.gameObject = Resources.Load<GameObject>("Prefabs/Buildings/Skyscraper");
    }

    // Refresh yourself
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        tilemap.RefreshTile(position);
    }
}