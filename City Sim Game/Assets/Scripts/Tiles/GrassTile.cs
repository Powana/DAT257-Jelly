using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GrassTile : Tile
{
	void OnEnable()
	{
		// sprite = Resources.Load<Sprite>("isometric_pixel_0004");
        gameObject = Resources.Load<GameObject>("Prefabs/Test");
	}
}
