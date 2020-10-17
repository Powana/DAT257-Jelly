using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Demolish : Cell
{
    public Demolish()
    {
        spritePath = "Sprites/Demolish4k";
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.gameObject = Resources.Load<GameObject>("Prefabs/Basic/Demolish");
    }

    public override bool validPosition(Tilemap tilemap, Vector3Int pos)
    {
        return true;
    }
}
