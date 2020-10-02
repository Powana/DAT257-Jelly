using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

abstract public class Waterbuilding : Building
{
    protected Waterbuilding()
    {

    }

    public override bool validPosition(Tilemap tilemap, Vector3Int pos)
    {
        if (tilemap.GetTile(pos) is Water)
        {
            return true;
        }
        return false;
    }
}