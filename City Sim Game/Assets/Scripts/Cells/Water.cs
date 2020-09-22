using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Water : Cell
{
	void OnEnable()
	{
		sprite = Resources.Load<Sprite>("water_tile");
	}

	public override void HandleClick()
	{
		Debug.Log("Vatten!");
	}
}
