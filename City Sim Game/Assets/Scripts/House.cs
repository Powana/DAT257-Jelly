using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.Tilemaps;

//This class is for making houses so people can live in 
public class Greenliving : Building
{

    public Greenliving()
    {
        spritePath = "Sprites/greenliving4k";
        stringName = "Green Living Complex";
        cost = 2000;

        Map.resourceManager.resources["settlements"].value += 250;

        resources["cash"].upkeep = 10;
        resources["pollution"].delta = 1;
        resources["energy"].delta = -4;

    }
    
    // Set sprite and/or gameobject for rendering, this method is useful as context can be used to determine the desired sprite/gameobject
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.gameObject = Resources.Load<GameObject>("Prefabs/Buildings/Greenliving");
    }

    // Refresh yourself
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        tilemap.RefreshTile(position);
    }
}