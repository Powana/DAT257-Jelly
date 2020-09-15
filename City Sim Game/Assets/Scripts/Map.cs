using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Map : MonoBehaviour
{
	private Dictionary<(int, int), GameObject> tiles;
	private int resolution = 16;

	public GameObject grass;
	public int cellSize = 1;
	public int width = 8;
	public int height = 8;

	// This script will simply instantiate the Prefab when the game starts.
	void Start()
	{
		// Initialize dictionary.
		tiles = new Dictionary<(int, int), GameObject>();

		// Instantiate at position (0, 0, 0) and zero rotation.
		int[,] gridArray = new int[width, height];
		for (int x = 0; x < gridArray.GetLength(0); x++) {
			for (int y = 0; y < gridArray.GetLength(1); y++) {
				tiles.Add((x, y), RenderSprite(grass, x, y));
			}
		}
	}

	void Update()
	{
		// Check if the left mouse button was clicked
		if (Input.GetMouseButtonDown(0))
		{
			Camera cam = Camera.main;
			Vector2 mousePos = Input.mousePosition;
			Vector2 point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

			int x = (int)Math.Floor(point.x + (float)cellSize / 2);
			int y = (int)Math.Floor(point.y + (float)cellSize / 2);

			if (tiles.ContainsKey((x, y))) {
				GameObject tile = GetTile(x, y);
				Debug.Log(tile);
				tiles.Remove((x, y));
				Destroy(tile);
			} else {
				Debug.Log("No tile found.");
			}
		}
	}

	public GameObject RenderSprite(GameObject sprite, int x, int y)
	{
		return Instantiate(sprite, GetPosition(x, y), Quaternion.identity, transform);
	}

	private Vector2 GetPosition(int x, int y)
	{
		return new Vector2(x, y) * cellSize;
	}

	private GameObject GetTile(int x, int y)
	{
		return tiles[(x, y)];
	}
}
