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

	private Dictionary<string, Cell> availableCells;

	// Currently held cell.
	private Cell held;

	// Tilemap
	private Tilemap map;

	// Manager of resources.
	private ResourceManager resourceManager;

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

	// Position of mouse in world and on grid.
	Vector2 mousePosition;
	Vector3Int gridPosition;

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

		//Initialize and populate dictionary with available buildings
		availableCells = new Dictionary<string, Cell>();

		// TODO: Are there any side-effects of only using one instance for all tiles?
		availableCells.Add("Park", ScriptableObject.CreateInstance<Park>());
		availableCells.Add("Farm", ScriptableObject.CreateInstance<Farm>());
		availableCells.Add("Office", ScriptableObject.CreateInstance<Office>());
		availableCells.Add("Industry", ScriptableObject.CreateInstance<Industry>());
		availableCells.Add("Residential", ScriptableObject.CreateInstance<Residential>());
		availableCells.Add("Casino", ScriptableObject.CreateInstance<Casino>());
		availableCells.Add("Nuclear", ScriptableObject.CreateInstance<Nuclear>());
		availableCells.Add("Road", ScriptableObject.CreateInstance<Road>());

		//generate grass
		generateMap();
		//generate water
		generateLake();


	}

	void generateMap()
	{
		// Iterate columns.
		for (int x = 0; x < width; x++)
		{
			//Iterate rows
			for (int y = 0; y < height; y++)
			{
				Grass tile = AddCell<Grass>(x, y) as Grass;
			}
		}
	}

	void generateLake()
	{
		//spawns i number of cells on random places
		for(int i = 0; i < lake; i++)
		{
			SwapCell<Water>(new Vector3Int(Random.Range(0, width), Random.Range(0, height), 0));
		}
		int lakeTiles = 0;
		while ( lakeTiles < lakeSize)
		{
			int xCord = Random.Range(0, width);
			int yCord = Random.Range(0, height);

			int neighbour = GetSurroundingWallCount<Water>(xCord, yCord);
			//spawns Lakesize number of cells on the map if there are any adjacent cells with water
			if(neighbour > 0)
			{
				SwapCell<Water>(new Vector3Int(xCord, yCord, 0));
				lakeTiles++;
			}
		}
	
	}

	//Checks how many surrounding blocks are T
	int GetSurroundingWallCount<T>(int x, int y) where T: Cell
	{
		int wallcount = 0;

		// if we are not at the top of the map
		// check the northern tile
		if (y != 0) {
			if (map.GetTile(new Vector3Int(x, y, 0) + new Vector3Int(1, 0, 0)) is T)
				wallcount++; }

		// if we are not at the bottom of the map
		// check the southern tile
		if (y != height) {
			if(map.GetTile(new Vector3Int(x, y, 0) + new Vector3Int(-1, 0, 0)) is T)
			wallcount++; }

		// if we are not at the left of the map
		// check the western tile
		if (x != 0) {
			if (map.GetTile(new Vector3Int(x, y, 0) + new Vector3Int(0, 1, 0)) is T)
				wallcount++;
		}

		// if we are not at the right of the map
		// check the eastern tile
		if (x != height) {
			if (map.GetTile(new Vector3Int(x, y, 0) + new Vector3Int(0, -1, 0)) is T)
				wallcount++;
		}
		return wallcount;
	}



	void Update()
	{
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		gridPosition = map.WorldToCell(mousePosition);

		Cell hoveredTile = map.GetTile<Cell>(gridPosition);

		// Handle mouse clicks on the map.
		if (Input.GetMouseButtonDown(0)) {
			// Fetch clicked tile, if any.
			Cell clickedTile = map.GetTile<Cell>(gridPosition);

			// If no tile is present, return.
			if (clickedTile == null) {
				//Debug.Log("Finns inte");
				return;
			}
			//// FOR DEMONSTRATION PURPOSES
			// If you are holding a cell and click grass, you will sell the grass
			// and buy the held cell
			if (held != null) {
				if (itLand(clickedTile)) {
					landObjectsPlacement();
				} else if (itWater(clickedTile)) {
					waterObjectsPlacement();
				}
			}
		}

		// Call tick every period.
		if (Time.time > tickTime) {
			tickTime = Time.time + period;
			Tick();
		}
	}

	// Is called every period.
	public void Tick()
	{
		resourceManager.Tick();
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

	// Purchases and adds cell of specified type at given position.
	private void Purchase<T>(Vector3Int pos) where T : Cell
	{
		resourceManager.Purchase(AddCell<T>(pos));
	}

	private void Purchase<T>(int x, int y) where T : Cell
	{
		Purchase<T>(new Vector3Int(x, y, 0));
	}

	// Purchases specified cell at given position.
	private void Purchase(Cell cell, int x, int y)
	{
		resourceManager.Purchase(AddCell(cell,x,y));
	}

	// Instantiates the given cell on the given position.
	private Cell AddCell(Cell cell, Vector3Int pos)
	{
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
		held = availableCells[cellName];
	}

	// This method check if the picked cell is land and return true if it is .
	private bool itLand(Cell  cell)
	{
		return cell is Grass;
	}

	// This method check if the picked cell is water and return true if it is.
	private bool itWater(Cell cell)
	{
		return cell is Water;
	}
	// This method is responsible for placing the objects that belong to water.
	private void waterObjectsPlacement()
	{
		//Sell(gridPosition);

		//here its gonna place anything in water because we still don't have any water objects
		//Purchase(held, gridPosition.x, gridPosition.y);
		held = null;
	}
	// This method is responsible for placing the objects that belong to land.
	private void landObjectsPlacement()
	{
		Sell(gridPosition);

		if (held is Road) {
			Debug.Log("PURCASHING ROAD");
			Purchase<Road>(gridPosition.x, gridPosition.y);
		} else {
			Purchase(held, gridPosition.x, gridPosition.y);
		}
		held = null;
	}

	public void Warn(string message)
	{
		messages.GetComponent<Message>().Warn(message);
	}

	public void Info(string message)
	{
		messages.GetComponent<Message>().Info(message);
	}

}
