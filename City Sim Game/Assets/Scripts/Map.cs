using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
	public GameObject grass;
	public int cellSize = 1;
	public int width = 8;
	public int height = 8;

	// This script will simply instantiate the Prefab when the game starts.
	void Start()
	{
		// Instantiate at position (0, 0, 0) and zero rotation.
		int[,] gridArray = new int[width, height];
		for (int x = 0; x < gridArray.GetLength(0); x++) {
			for (int y = 0; y < gridArray.GetLength(1); y++) {
				RenderSprite(grass, x, y);
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
