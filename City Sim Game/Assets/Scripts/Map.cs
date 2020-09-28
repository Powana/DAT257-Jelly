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
	private Dictionary<(int, int), Cell> tiles;

	private Dictionary<string, Cell> availableBuildings;
	
	// Currently held cell.
	private Cell held;

	// Tilemap
	private Tilemap map;

	// Manager of resources.
	private ResourceManager resourceManager;

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
		availableBuildings = new Dictionary<string, Cell>();
		Cell park = ScriptableObject.CreateInstance<Park>();
		Cell farm = ScriptableObject.CreateInstance<Farm>();
		Cell office = ScriptableObject.CreateInstance<Office>();
		Cell industry = ScriptableObject.CreateInstance<Industry>();
		Cell residential = ScriptableObject.CreateInstance<Residential>();
		availableBuildings.Add("Park", park);
		availableBuildings.Add("Farm", farm);
		availableBuildings.Add("Office", office);
		availableBuildings.Add("Industry", industry);
		availableBuildings.Add("Residential", residential);
		
		// Iterate columns.
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				Grass tile = AddCell<Grass>(x, y) as Grass;
				// tile.gameObject.GetComponent(typeof(Cell)) as Cell;
			}
		}

		// Iterate a quarter of columns.
		for (int x = 0; x < width / 3.5f; x++) {
			for (int y = height / 2; y < height; y++) {
				SwapCell<Water>(new Vector3Int(x, y, 0));
			}
		}
	}

	void Update()
	{
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		gridPosition = map.WorldToCell(mousePosition);
		
		Cell hoveredTile = map.GetTile<Cell>(gridPosition);

			// Handle mouse clicks on the map.
			if (Input.GetMouseButtonDown(0))
				{
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
					if (clickedTile is Grass && held !=null)
					{
						Sell(gridPosition);
						Purchase(held,gridPosition.x,gridPosition.y);
						held = null;
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
		public void grabBuilding(string building)
		{
			held = availableBuildings[building];
		}
}
