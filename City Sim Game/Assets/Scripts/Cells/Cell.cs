﻿using System;
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

	// Initialize resources dictionary.
	protected Cell()
	{
		resources = new Dictionary<string, Resource>();

		foreach (string resource in new string[] {
				"cash", "population", "food", "energy", "pollution", "workers"
			}) {
			resources.Add(resource, new Resource(resource));
		}
	}

    // Check if the given position is valid for cell placement
    // TODO: Maybe this should return a string to be used in the warning message for invalid placement.
    public abstract bool validPosition(Tilemap tilemap, Vector3Int pos);

    public bool validPosition(Tilemap tilemap, int x, int y)
    {
        return validPosition(tilemap, new Vector3Int(x, y, 0));
    }

	public Dictionary<string, Resource> HireWorkers(int workers, int sign)
	{
		Dictionary<string, Resource> diffResources = new Dictionary<string, Resource>();

		if (Math.Abs(sign) != 1)
			throw new System.ArgumentException("Must be ±1", "sign");

		if (resources["workers"].value + workers > availableJobs)
			throw new System.ArgumentException("Workers can't exceed the available job limit on the cell.", "workers");
		if (resources["workers"].value + workers < 0)
			throw new System.ArgumentException("Workers on a cell can't fall short of zero.", "workers");

		foreach (string resource in new string[] {
				"cash", "food", "energy"
			}) {
			diffResources.Add(resource, new Resource(resource, 0, (int)((float)resources[resource].delta * (availableJobs > 0 ? ((float)workers / (float)availableJobs) : sign))));
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
}
