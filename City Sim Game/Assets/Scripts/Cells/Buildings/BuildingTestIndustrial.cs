using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Example of a building implementation
public class BuildingTestIndustrial : Building
{

    // Building-specific stats are set in the constructor.
    public BuildingTestIndustrial()
    {
        stringName = "Cool Factory";
        cost = 100;
        resources["cash"].delta = 1;
    }
    
    // Set sprite and/or gameobject for rendering, this method is useful as context can be used to determine the desired sprite/gameobject
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        // Setting the sprite is no longer needed, as it can be set on the associated gameobject.
        // tileData.sprite = Resources.Load<Sprite>("industry_tile");

        tileData.gameObject = Resources.Load<GameObject>("Prefabs/Buildings/Bob");
    }

    // Refresh yourself
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        tilemap.RefreshTile(position);
    }
}