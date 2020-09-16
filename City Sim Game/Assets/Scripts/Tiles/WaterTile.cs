using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterTile : Tile
{
	void OnEnable()
	{
		sprite = Resources.Load<Sprite>("isometric_pixel_0004");
	}
}
