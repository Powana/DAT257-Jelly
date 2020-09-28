using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Road : Cell
{

    // Set sprite and/or gameobject for rendering, this method is useful as context can be used to determine the desired sprite/gameobject
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        string spritePath = "";

        // Create an object to be copied by the tile.
        GameObject go = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Basic/Road"));
        SpriteRenderer roadSpriteR = go.GetComponentInChildren<SpriteRenderer>();

        // Reset flips
        roadSpriteR.flipX = false;
        roadSpriteR.flipY = false;

        // Check surrounding tiles for connecting roads
        bool north = tilemap.GetTile(position + new Vector3Int(1, 0, 0)) is Road;
        bool east = tilemap.GetTile(position + new Vector3Int(0, -1, 0)) is Road;
        bool south = tilemap.GetTile(position + new Vector3Int(-1, 0, 0)) is Road;
        bool west = tilemap.GetTile(position + new Vector3Int(0, 1, 0)) is Road;

        bool[] connected = { north, east, south, west };


        // I like dis
        switch (connected.Count(x => x))
        {
            // No roads connected
            case 0:
                spritePath = "road_tile_none";
                break;

            // Dead end
            case 1:
                spritePath = "road_tile_S";
                if (east)       { roadSpriteR.flipX = true; }
                else if (west)  { roadSpriteR.flipY = true; }
                else if (north) { roadSpriteR.flipX = true;
                                  roadSpriteR.flipY = true; }
                break;

            case 2:
                // Assume N/S straight road
                spritePath = "road_tile_NS";
                if (east && west) { roadSpriteR.flipX = true; }
                // If not straight
                else if (north && east) { spritePath = "road_tile_NE"; }
                else if (south && west) { spritePath = "road_tile_NE";
                                          roadSpriteR.flipX = true; }

                else if (north && west) { spritePath = "road_tile_NW"; }
                else if (south && east) { spritePath = "road_tile_NW";
                                          roadSpriteR.flipY = true; }
                break;

            // T-junction
            case 3:
                spritePath = "road_tile_NEW";
                if (!north)      { roadSpriteR.flipX = true;
                                   roadSpriteR.flipY = true; }
                else if (!east)  { roadSpriteR.flipX = true; }
                else if (!west)  { roadSpriteR.flipY = true; }
                break;

            // 4-way junction
            case 4:
                spritePath = "road_tile_NESW";
                break;
        }
        roadSpriteR.sprite = Resources.Load<Sprite>("Sprites/roads/" + spritePath);

        // This is retarded, gameObject needs to be set to an instaniated gameobject so that it can create its own instance of that object (?????????)
        tileData.gameObject = go;
        // Therefore we must destroy the instiated object to avoid duplicates
        Destroy(go);
    }

    // This can be used if refreshing a tile should also refresh adjacent tiles.
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        // Refresh surrounding tiles and itself.
        tilemap.RefreshTile(position);
        tilemap.RefreshTile(position + new Vector3Int(1, 0, 0));
        tilemap.RefreshTile(position + new Vector3Int(0, -1, 0));
        tilemap.RefreshTile(position + new Vector3Int(-1, 0, 0));
        tilemap.RefreshTile(position + new Vector3Int(0, 1, 0));

    }
}
