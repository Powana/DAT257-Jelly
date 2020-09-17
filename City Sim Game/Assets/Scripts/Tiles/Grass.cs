using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grass : Cell
{
	void OnEnable()
	{
		sprite = Resources.Load<Sprite>("grass");
	}

	public override void HandleClick()
	{
		Debug.Log("Hej!");
	}
}
