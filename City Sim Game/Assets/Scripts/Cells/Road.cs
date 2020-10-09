using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Road : Cell
{
    // Todo: The prefab is still one tile off in location for some reason

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

    // Set sprite and/or gameobject for rendering, this method is useful as context can be used to determine the desired sprite/gameobject
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        string spritePath = "";

        // Get a reference to the already instaiated object if one exists, this is the case when this tile is being updated as a result of a neighbouring update
        GameObject go = tilemap.GetComponent<Tilemap>().GetInstantiatedObject(position);
        // If there is none (A new road is being created), load the default road prefab
        if (go == null)
        {
            go = Resources.Load<GameObject>("Prefabs/Basic/Road");
            tileData.gameObject = go;
        }
        // If the object has been instantiated already, use that, but still set the tileData.gameObject to the original prefab (That was a headache to figure out)
        else
        {
            tileData.gameObject = tilemap.GetComponent<Tilemap>().GetObjectToInstantiate(position);
        }

        // Sprite renderer for the switchable top part of the road.
        SpriteRenderer roadSpriteR = go.GetComponent<SpriteRenderer>();

        // Hack for off positioned sprites, this sucks but I can't find the root of the issue
        go.transform.Translate(new Vector3(-1, 0.625f));

        // Check surrounding tiles for connecting roads
        bool north = tilemap.GetTile(position + new Vector3Int(1, 0, 0)) is Road;
        bool east = tilemap.GetTile(position + new Vector3Int(0, -1, 0)) is Road;
        bool south = tilemap.GetTile(position + new Vector3Int(-1, 0, 0)) is Road;
        bool west = tilemap.GetTile(position + new Vector3Int(0, 1, 0)) is Road;

        bool[] connected = { north, east, south, west };

        roadSpriteR.flipX = false;
        roadSpriteR.flipY = false;

        // I like dis
        switch (connected.Count(x => x))
        {
            // No roads connected
            case 0:
                spritePath = "road__tile_none";
                break;

            // Dead end
            case 1:
                spritePath = "road__tile_S";
                if (east) { roadSpriteR.flipX = true; }
                else if (west) { roadSpriteR.flipY = true; }
                else if (north)
                {
                    roadSpriteR.flipX = true;
                    roadSpriteR.flipY = true;
                }
                break;

            case 2:
                // Assume N/S straight road
                spritePath = "road__tile_NS";
                if (east && west) { roadSpriteR.flipX = true; }
                // If not straight
                else if (north && east) { spritePath = "road__tile_NE"; }
                else if (south && west)
                {
                    spritePath = "road__tile_NE";
                    roadSpriteR.flipX = true;
                }

                else if (north && west) { spritePath = "road__tile_NW"; }
                else if (south && east)
                {
                    spritePath = "road__tile_NW";
                    roadSpriteR.flipY = true;
                }
                break;

            // T-junction
            case 3:
                spritePath = "road__tile_NEW";
                if (!north)
                {
                    roadSpriteR.flipX = true;
                    roadSpriteR.flipY = true;
                }
                else if (!east) { roadSpriteR.flipX = true; }
                else if (!west) { roadSpriteR.flipY = true; }
                break;

            // 4-way junction
            case 4:
                spritePath = "road__tile_NESW";
                break;
        }
        roadSpriteR.sprite = Resources.Load<Sprite>("Sprites/roads/" + spritePath);
    }

    public override bool validPosition(Tilemap tilemap, Vector3Int pos)
    {
        if (tilemap.GetTile(pos) is Grass) return true;
        return false;
    }
}
