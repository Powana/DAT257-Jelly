using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GrassTile : Tile
{
	void Start()
	{
		sprite = (Sprite)Resources.Load("/Tiles/isometric_pixel_0004");
	}
}
