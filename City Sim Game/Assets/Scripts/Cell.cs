using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Cell : Tile
{
	public abstract void HandleClick();
}
