using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTile : Tile
{
	public override void HandleClick()
	{
		Debug.Log("Hey! I'm a water on (" + x + "," + y + ")");
	}
}
