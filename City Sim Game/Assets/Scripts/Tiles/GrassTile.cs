﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : Tile
{
	public override void HandleClick()
	{
		Debug.Log("Hey! I'm on (" + x + "," + y + ")");
	}
}
