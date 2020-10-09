using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Waterplant
public class Waterplant : Waterbuilding
{
    // Building-specific stats are set in the constructor.
    public Waterplant()
    {
        spritePath = "Sprites/Waterplant4k";
        stringName = "Waterplant";
        cost = 35000;
        resources["cash"].upkeep = 2;
        resources["pollution"].upkeep = -1;
        resources["cash"].delta = 0;
        resources["energy"].delta = 5;
        resources["food"].delta = -6;
        availableJobs = 6;
        takenJobs = 0;
    }

    // Set sprite and/or gameobject for rendering, this method is useful as context can be used to determine the desired sprite/gameobject
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.gameObject = Resources.Load<GameObject>("Prefabs/Buildings/Waterplant");
    }

    // Refresh yourself
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        tilemap.RefreshTile(position);
    }
}