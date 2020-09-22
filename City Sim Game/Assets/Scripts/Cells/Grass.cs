using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grass : Cell
{
	void OnEnable()
	{
		sprite = Resources.Load<Sprite>("grass_tile");
	}
}
