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

	public Dictionary<string, Resource> HireWorkers(int workers)
	{
		Dictionary<string, Resource> diffResources = new Dictionary<string, Resource>();

		if (resources["workers"].value + workers > availableJobs)
			throw new System.ArgumentException("Workers can't exceed the available job limit on the cell.", "workers");
		if (resources["workers"].value + workers < 0)
			throw new System.ArgumentException("Workers on a cell can't fall short of zero.", "workers");

		foreach (string resource in new string[] {
				"cash", "food", "energy"
			}) {
			diffResources.Add(resource, new Resource(resource, 0, (int)((float)resources[resource].delta * (availableJobs > 0 ? ((float)workers / (float)availableJobs) : 1))));
		}

		resources["workers"].value += workers;

		return diffResources;
	}

	public Dictionary<string, Resource> FireWorkers(int workers)
	{
		return HireWorkers(-workers);
	}
}
