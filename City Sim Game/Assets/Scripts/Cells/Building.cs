using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Building : Cell
{
	void OnEnable()
	{
		cost = 100;
		resources["cash"].delta = 1;
		sprite = Resources.Load<Sprite>("industry_tile");
	}

	public override void HandleClick()
	{
		//
	}
}
