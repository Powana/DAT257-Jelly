using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
	// Reference to the Prefab. Drag a Prefab into this field in the Inspector.
	public GameObject prefab;
	public int cellSize = 1;
	public int width = 4;
	public int height = 4;

	// This script will simply instantiate the Prefab when the game starts.
	void Start()
	{
		// Instantiate at position (0, 0, 0) and zero rotation.
		int[,] gridArray = new int[width, height];
		for (int x = 0; x < gridArray.GetLength(0); x++) {
			for (int y = 0; y < gridArray.GetLength(1); y++) {
				RenderSprite(prefab, x, y);
			}
		}
	}

	public void RenderSprite(GameObject sprite, int x, int y)
	{
		Instantiate(sprite, GetPosition(x, y), Quaternion.identity);
	}

	private Vector2 GetPosition(int x, int y)
	{
		return new Vector2(x, y) * cellSize;
	}
}
