using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Responsible for containing and managing global resources.
public class ResourceManager
{
	// Dictionary representing all resources and their deltas.
	public Dictionary<string, Resource> resources;

	// Initializes resource dictionary.
	public ResourceManager()
	{
		resources = new Dictionary<string, Resource>();

		// Populate dictionary with resource entries.
		foreach (string resource in new string[] {
				"cash", "population", "food", "energy", "pollution", "workers", "lake","settlement"
			}) {
			resources.Add(resource, new Resource(resource));
		}

		// Start with some cash and lake health.
		resources["cash"].value = 10000;
		resources["lake"].value = 100000;
		resources["population"].value = 10;
		resources["food"].value = 100;
		resources["settlement"].value = 0;
		

	}

	// Should be called by game loop every period.
	public void Tick()
	{
		resources["food"].delta -= resources["population"].value * 5;
		// Update resources depending on their upkeep/production.
		foreach (KeyValuePair<string, Resource> pair in resources) {
			pair.Value.value += pair.Value.delta;
		}

		PopulationGrowth();

		// Deplete lake health depending on the current pollution.
		resources["lake"].delta = -resources["pollution"].value / 100;
       
	}

	public void PopulationGrowth()
    {
		int foodLeft = resources["food"].value - resources["population"].value * 5;
		if (foodLeft >= 5)
        {
				resources["population"].delta += 1;
            
		}

    }

	public void AddSettlement()
	{
		resources["settlement"].delta += 1;
	}



	// Purchases the given cell by subtracting cost from current cash and
	// updating resource deltas.
	public void Purchase(Cell cell)
	{
		// Subtract cost.
		resources["cash"].value -= cell.cost;

		// Update deltas for upkeep.
		foreach (KeyValuePair<string, Resource> pair in cell.resources) {
			resources[pair.Key].delta -= pair.Value.upkeep;
		}

		// If no available jobs are available, produce at full capacity by
		// default.
		if (cell.availableJobs == 0)
			Diff(cell.HireWorkers(0));
	}

	public void Diff(Dictionary<string, Resource> diffs)
	{
		// Update deltas for and production.
		foreach (KeyValuePair<string, Resource> pair in diffs) {
			resources[pair.Key].delta += (int)pair.Value.delta;
		}
	}

	// Purchases the given cell by subtracting cost from current cash and
	// updating resource deltas.
	public void Sell(Cell cell)
	{
		// Sell for a smaller amount than it was bought for balance.
		resources["cash"].value += (cell.cost / 4);

		// Update deltas for upkeep and production.
		foreach (KeyValuePair<string, Resource> pair in cell.resources) {
			resources[pair.Key].delta += (int)pair.Value.upkeep;
		}

		// Take into account the lost production by firing all workers.
		FireWorkers(cell, cell.resources["workers"].value);
	}

	public override string ToString()
	{
		string data = "[";

		foreach (KeyValuePair<string, Resource> pair in resources) {
			data += String.Format("{0}: {1} ({2}),", pair.Key, pair.Value.value, pair.Value.delta);
		}

		data += "]";

		return data;
	}

	public void HireWorkers(Cell cell, int workers)
	{
		HireWorkers(cell, workers, cell.HireWorkers(workers));
	}

	public void FireWorkers(Cell cell, int workers)
	{
		HireWorkers(cell, -workers, cell.FireWorkers(workers));
	}

	public void HireWorkers(Cell cell, int workers, Dictionary<string, Resource> diffs)
	{
		resources["workers"].value += workers;
		foreach (KeyValuePair<string, Resource> pair in diffs) {
			resources[pair.Key].delta += pair.Value.delta;
		}
	}
}
