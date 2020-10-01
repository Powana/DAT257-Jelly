using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Water : Cell
{
	public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
	{
		tileData.gameObject = Resources.Load<GameObject>("Prefabs/Basic/Water");
	}

    // Water can be place anywhere
    public override bool validPosition(Tilemap tilemap, Vector3Int pos)
    {
        return true;
    }
}
