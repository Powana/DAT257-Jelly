using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Cell : Tile
{
	public int cost = 0;
	public Dictionary<string, Resource> resources;
	public int availableJobs = 0;
	public int takenJobs = 0;
	public string spritePath;

	// Initialize resources dictionary.
	protected Cell()
	{
		resources = new Dictionary<string, Resource>();

		foreach (string resource in new string[] {
				"cash", "population", "food", "energy", "pollution", "workers"
			}) {
			resources.Add(resource, new Resource(resource));
		}
		spritePath=null;
	}

	// Check if the given position is valid for cell placement
	// TODO: Maybe this should return a string to be used in the warning message for invalid placement.
	public abstract bool validPosition(Tilemap tilemap, Vector3Int pos);

	public bool validPosition(Tilemap tilemap, int x, int y)
	{
		return validPosition(tilemap, new Vector3Int(x, y, 0));
	}

	// Hires the specified amount of workers and returns a calculated
	// difference in production of resources.
	public Dictionary<string, Resource> HireWorkers(int workers, int sign)
	{
		Dictionary<string, Resource> diffResources = new Dictionary<string, Resource>();

		// If sign is not of size 1, throw exception.
		if (Math.Abs(sign) != 1)
			throw new System.ArgumentException("Must be ±1", "sign");

		// If workers exceed available spots, throw exception.
		if (resources["workers"].value + workers > availableJobs)
			throw new System.ArgumentException("Workers can't exceed the available job limit on the cell.", "workers");

		// If workers fall short of zero, throw exception.
		if (resources["workers"].value + workers < 0)
			throw new System.ArgumentException("Workers on a cell can't fall short of zero.", "workers");

		// Iterate production resources.
		foreach (string resource in new string[] {
				"cash", "food", "energy"
			}) {

			// Multiply the total potential production of resources with the
			// fraction (addedworkers)/(availablejobs). Multiply also with sign
			// represent firing.
			diffResources.Add(resource, new Resource(resource, 0,
				(int)((float)resources[resource].delta *
				// If available jobs are zero, initiate with 100% capacity.
				(availableJobs > 0 ? ((float)workers / (float)availableJobs) : sign)))
			);
		}

		resources["workers"].value += workers;

		return diffResources;
	}

	public Dictionary<string, Resource> HireWorkers(int workers)
	{
		return HireWorkers(workers, 1);
	}

	public Dictionary<string, Resource> FireWorkers(int workers)
	{
		return HireWorkers(-workers, -1);
	}

	public int GetCost()
	{
		return cost;
	}

	public string getSpritePath(){
		//Returns a neutral grasssprite if path is not defined
		if(spritePath==null){
			return "Sprites/Grass4k";
		}
			return spritePath;
	}
}
