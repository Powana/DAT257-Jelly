using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Class responsible for rendering and managing a tile map.
public class Map : MonoBehaviour
{
	// Container for tiles placed on this map.
	private Dictionary<(int, int), GameObject> tiles;

	// Game object representing the base map tile.
	public GameObject grass;
	public GameObject water;

	// World size of a tile.
	public int cellSize = 1;

	// Width (in cells) of map.
	public int width = 8;

	// Height (in cells) of map.
	public int height = 8;

	// Instantiates the base tiles and fills the tiles dictionary.
	void Start()
	{
		// Initialize dictionary.
		tiles = new Dictionary<(int, int), GameObject>();

		// Iterate columns.
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				GameObject tile = AddTile(x, y);

				// Set component data.
				Tile component = GetComp(tile);
				component.x = x;
				component.y = y;
			}
		}
	}

	// Handle mouse clicks on the map.
	void Update()
	{
		// Check if the left mouse button was clicked
		if (Input.GetMouseButtonDown(0))
		{
			// Fetch world position.
			Vector2 pos = GetWorldPosition(Input.mousePosition);

			// If world contains that key pair, handle it.
			if (tiles.ContainsKey(((int)pos.x, (int)pos.y))) {
				GetComp(pos).HandleClick();
				SwitchTile(water, pos);
			} else {
				Debug.Log("No tile found.");
			}
		}
	}

	// Takes the screens coordinates and returns the world position.
	private Vector2 GetWorldPosition(Vector2 pos)
	{
		// Fetch main camera.
		Camera cam = Camera.main;

		// Fetch world point.
		Vector2 point = cam.ScreenToWorldPoint(pos);

		// Initialize world points and account for cell size margin.
		int x = (int)Math.Floor(point.x + (float)cellSize / 2);
		int y = (int)Math.Floor(point.y + (float)cellSize / 2);

		return new Vector2(x, y);
	}

	private Vector2 GetWorldPosition(int x, int y)
	{
		return GetWorldPosition(new Vector2(x, y));
	}

	// Returns position and takes cell size into account.
	private Vector2 GetPosition(int x, int y)
	{
		return new Vector2(x, y) * cellSize;
	}

	// Returns tile belonging to the specified coordinates, if any.
	private GameObject GetTile(int x, int y)
	{
		return tiles[(x, y)];
	}

	private GameObject GetTile(Vector2 pos)
	{
		return GetTile((int)pos.x, (int)pos.y);
	}

	// Returns the belonging tile component.
	private Tile GetComp(GameObject tile)
	{
		return tile.gameObject.GetComponent(typeof(Tile)) as Tile;
	}

	private Tile GetComp(Vector2 pos)
	{
		return GetComp(GetTile(pos));
	}

	private Tile GetComp(int x, int y)
	{
		return GetComp(new Vector2(x, y));
	}

	// Renders a tile at the specified coordinates and adds the instance to the
	// dictionary.
	public GameObject AddTile(GameObject tile, int x, int y)
	{
		GameObject instance = Instantiate(tile, GetPosition(x, y),

										  // Default rotation.
										  Quaternion.identity,

										  // Set this map as parent.
										  transform);

		// Add the tile instance to the dictionary.
		tiles.Add((x, y), instance);

		return instance;
	}

	// Adds a grass tile at the specified coordinates.
	public GameObject AddTile(int x, int y)
	{
		return AddTile(grass, x, y);
	}

	// Destroys the tile instance and pulls the entry from the dictionary.
	public void RemoveTile(int x, int y)
	{
		GameObject tile = GetTile(x, y);
		tiles.Remove((x, y));
		Destroy(tile);
	}

	public void RemoveTile(Vector2 pos)
	{
		RemoveTile((int)pos.x, (int)pos.y);
	}

	// Switch tile on the specified position.
	public GameObject SwitchTile(GameObject tile, int x, int y)
	{
		// Remove previous entry and destroy the tile.
		RemoveTile(x, y);

		// Add new tile.
		return AddTile(tile, x, y);
	}

	public GameObject SwitchTile(GameObject tile, Vector2 pos)
	{
		return SwitchTile(tile, (int)pos.x, (int)pos.y);
	}
}
