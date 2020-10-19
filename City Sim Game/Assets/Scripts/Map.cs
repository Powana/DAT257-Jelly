using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

// Class responsible for rendering and managing a tile map.
public class Map : MonoBehaviour
{
	[Range(0, 100)]
	public int lake;

	[Range(0, 100)]
	public int lakeSize;

	// Container for tiles placed on this map.
	private Dictionary<(int, int), Cell> tiles;

	// Currently held cell.
	private Cell held;

	// Currenly clicked tile
	private Cell clickedTile;

	// Tilemap
	private Tilemap map;

	// Manager of resources.
	public static ResourceManager resourceManager;

	// Canvas
	public Canvas messages;

	// World size of a tile.
	public int cellSize = 1;

	// Width (in cells) of map.
	public int width = 8;   // Game object representing the base map tile.

	// Height (in cells) of map.
	public int height = 8;

	// Time until next tick.
	private float tickTime = 0;

	// Time between ticks.
	private float period = 1f;

	// Used to make sure warnings arent spammed each frame
	public static Vector3Int prevWarnedCellPos;

	// Position of mouse in world and on grid.
	Vector2 mousePosition;
	Vector3Int gridPosition;

	public AudioSource BuildingSound;

	// Instantiates the base tiles and fills the tiles dictionary.
	void Start()
	{
		// Nothing held by default.
		held = null;

		// Initialize resource manager.
		resourceManager = new ResourceManager();

		// Initialize dictionary.
		tiles = new Dictionary<(int, int), Cell>();
		map = GetComponent<Tilemap>();

		// Generate grass.
		GenerateMap();

		// Generate water.
		GenerateLake();

		// Generate starting road
		GenerateRoad();
	}

	void GenerateMap()
	{
		// Generate grass
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				Grass tile = AddCell<Grass>(x, y) as Grass;
			}
		}
	}

	void GenerateLake()
	{
		// Spawns i number of cells on random places.
		for (int i = 0; i < lake; i++) {
			SwapCell<Water>(new Vector3Int(Random.Range(0, width), Random.Range(0, height), 0));
		}

		int lakeTiles = 0;

		while (lakeTiles < lakeSize) {
			int xCord = Random.Range(0, width);
			int yCord = Random.Range(0, height);

			int neighbour = GetSurroundingWallCount<Water>(xCord, yCord);
			// Spawns Lakesize number of cells on the map if there are any adjacent cells with water.
			if(neighbour > 0)
			{
				SwapCell<Water>(new Vector3Int(xCord, yCord, 0));
				lakeTiles++;
			}
		}

	}

	void GenerateRoad()
	{
		for (int i = 0; i < width; i++)
		{
			Vector3Int pos = new Vector3Int(1, i, 0);
			SwapCell<Road>(pos);
		}
	}

	//Checks how many surrounding blocks are T
	public static int GetSurroundingWallCount<T>(Tilemap tilemap, int x, int y) where T: Cell
	{
		int wallcount = 0;

		// if we are not at the top of the map
		// check the northern tile
		if (tilemap.GetTile(new Vector3Int(x, y, 0) + new Vector3Int(1, 0, 0)) is T)
			wallcount++;

		// if we are not at the bottom of the map
		// check the southern tile
		//if (y != map.height) {
		if(tilemap.GetTile(new Vector3Int(x, y, 0) + new Vector3Int(-1, 0, 0)) is T)
			wallcount++;

		// if we are not at the left of the map
		// check the western tile
		//if (x != 0) {
		if (tilemap.GetTile(new Vector3Int(x, y, 0) + new Vector3Int(0, 1, 0)) is T)
			wallcount++;

		// if we are not at the right of the map
		// check the eastern tile
		//	if (x != height) {
		if (tilemap.GetTile(new Vector3Int(x, y, 0) + new Vector3Int(0, -1, 0)) is T)
			wallcount++;

		return wallcount;
	}

	int GetSurroundingWallCount<T>(int x, int y) where T : Cell
	{
		return GetSurroundingWallCount<T>(this.map, x, y);
	}


	void Update()
	{
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		gridPosition = GetGridPosition();
		Cell hoveredTile = map.GetTile<Cell>(gridPosition);

		// Handle mouse clicks on the map.
		if (Input.GetMouseButton(0)) {

            // Clicked on the UI, so ignore the click and let menu handler handle it
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            // Fetch clicked tile, if any.
            clickedTile = hoveredTile;

			// If no tile is present, return.
			if (clickedTile == null) {
				return;
			}

			// If user is holding a cell (from the shop)
			if (held != null) {
				if(held is Demolish){
					Sell(gridPosition);
					AddCell<Grass>(gridPosition);
				}else{
					ObjectPlacement();
					
				}
			}
		}

		// Testing purposes. Sell when middle click.
		if (Input.GetMouseButtonDown(2)) {
			Sell(gridPosition);
			AddCell<Grass>(gridPosition);
		}

		// Release an object from your feeble grasp with right mouse button
		if (Input.GetMouseButtonDown(1))
		{
			held = null;
		}

		// Call tick every period.
		if (Time.time >= tickTime) {
			tickTime = Time.time + period;
			Tick();
		}
	}

	// Is called every period.
	public void Tick()
	{
		// Debug.Log(resourceManager.ToString());
		resourceManager.Tick();
	}

	// Returns the currently clicked Tile.
	public Cell SendCell()
	{
		return clickedTile;
	}

	// Sells the cell at the given position.
	private void Sell(Vector3Int pos)
	{
		// Update global resources.
		resourceManager.Sell(GetCell(pos));

		// Remove cell from dictionary and map.
		RemoveCell(pos);
	}

	private void Sell(int x, int y)
	{
		Sell(new Vector3Int(x, y, 0));
	}

	// Purchases specified cell at given position, if possible.
	private bool TryPurchase(Cell cell, Vector3Int pos)
	{
        bool warn = true;
        if (prevWarnedCellPos == pos) warn = false;
        prevWarnedCellPos = pos;

		return resourceManager.TryPurchase(cell, warn);
	}

	// Instantiates the given cell on the given position.
	private Cell AddCell(Cell cell, Vector3Int pos)
	{
		// Instantiate new object for each tile instead of copying from the map.
		cell = Instantiate(cell);

		map.SetTile(pos, cell);

		// Add tile to dictionary.
		tiles.Add((pos.x, pos.y), cell);

		return cell;
	}

	private Cell AddCell(Cell cell, int x, int y)
	{
		return AddCell(cell, new Vector3Int(x, y, 0));
	}

	// Instantiates, on the given position, a cell of the given type.
	private Cell AddCell<T>(Vector3Int pos) where T : Cell
	{
		T tile = ScriptableObject.CreateInstance<T>();
		return AddCell(tile, pos);
	}

	private Cell AddCell<T>(int x, int y) where T : Cell
	{
		return AddCell<T>(new Vector3Int(x, y, 0));
	}

	// Clears the cell on the given position and removes it from the dictionary.
	private void RemoveCell(Vector3Int pos)
	{
		Cell tile = tiles[(pos.x, pos.y)];
		tiles.Remove((pos.x, pos.y));
		map.SetTile(pos, null);
	}

	private void RemoveCell(int x, int y)
	{
		RemoveCell(new Vector3Int(x, y, 0));
	}

	// Puts the specified cell on the given position, replacing the previous
	// one.
	private Cell SwapCell(Cell cell, Vector3Int pos)
	{
		RemoveCell(pos);
		return AddCell(cell, pos);
	}

	// Puts a new instance of a cell with the given type on the specified
	// position, replacing the previous one.
	private Cell SwapCell<T>(Vector3Int pos) where T : Cell
	{
		RemoveCell(pos);
		return AddCell<T>(pos.x, pos.y);
	}

	// Returns the cell instance for the given position from the dictionary.
	private Cell GetCell(int x, int y)
	{
		return tiles[(x, y)];
	}

	private Cell GetCell(Vector3Int pos)
	{
		return GetCell(pos.x, pos.y);
	}

	public void grabCell(string cellName)
	{
		held = (Cell)ScriptableObject.CreateInstance(cellName);
	}

	public Cell getHeld()
	{
		return held;
	}

	public Vector3Int GetGridPosition()
	{
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		gridPosition = map.WorldToCell(mousePosition);
		return gridPosition;
	}

	// This method is responsible for placing the objects.
	private void ObjectPlacement()
	{
		// Check if the cell allows for placement at gridposition
		if (held.validPosition(map, gridPosition.x, gridPosition.y)) {
			if (TryPurchase(held, gridPosition)) {
				Sell(gridPosition);
                BuildingSound.Play();
                AddCell(held, gridPosition.x, gridPosition.y);
			}
		}
	}

	public ResourceManager GetManager()
	{
		return resourceManager;
	}

	public Dictionary<(int, int), Cell> GetTiles()
	{
		return tiles;
	}
}
