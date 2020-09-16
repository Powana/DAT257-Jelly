using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

// Class responsible for rendering and managing a tile map.
public class Map : MonoBehaviour
{
	// Container for tiles placed on this map.
	private Dictionary<(int, int), GameObject> tiles;

	private Tilemap map;

	// World size of a tile.
	public int cellSize = 1;

	// Width (in cells) of map.
	public int width = 8;	// Game object representing the base map tile.

	// Height (in cells) of map.
	public int height = 8;

	// Instantiates the base tiles and fills the tiles dictionary.
	void Start()
	{
		// Initialize dictionary.
		tiles = new Dictionary<(int, int), GameObject>();
		map = GetComponent<Tilemap>();

		// Iterate columns.
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				GrassTile tile = ScriptableObject.CreateInstance<GrassTile>();
				Debug.Log(tile);
				map.SetTile(new Vector3Int(x, y, 0), tile);
			}
		}
	}

	// Handle mouse clicks on the map.
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3Int gridPosition = map.WorldToCell(mousePosition);
			TileBase clickedTile = map.GetTile(gridPosition);
			map.SetTile(gridPosition, null);
		}
	}
}
