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

    // Used to make sure warnings arent spammed each frame
    private Vector3Int prevWarnedCellPos;

	public int previouspop = 0;

	// Initializes resource dictionary.
	public ResourceManager()
	{
		resources = new Dictionary<string, Resource>();

		// Populate dictionary with resource entries.
		foreach (string resource in new string[] {
				"cash", "population", "food", "energy", "pollution", "workers", "lake", "residences", "residents"
			}) {
			resources.Add(resource, new Resource(resource));
		}

		// Start with some cash and lake health.
		resources["cash"].value = 100000;
		resources["lake"].value = 100000;
		resources["population"].value = 10;
		resources["food"].value = 250;
		resources["residences"].value = 0;
		resources["pollution"].value = 0;

		//intialize variables for diff in populationtick
		int previouspop = resources["population"].value;
		
	}

	

	// Should be called by game loop every period.
	public void Tick()
	{
		

		//set food delta for first population
		if (resources["population"].value == previouspop && resources["food"].value > 0)
		{
			resources["food"].delta += -1;
		}

		//updates food delta with population diff
		 if (resources["population"].value != previouspop)
		{
			resources["food"].delta += -(resources["population"].value - (previouspop));
			previouspop = resources["population"].value;
		}
		

		// Update resources depending on their upkeep/production.
		foreach (KeyValuePair<string, Resource> pair in resources) {
			pair.Value.value += pair.Value.delta;
		}
		
		PopulationGrowth(); 
		

		// Convenience
		Resource population = resources["population"];
		Resource residents  = resources["residents"];
		Resource residences = resources["residences"];

		// Fill available resident spots with population.
		residents.value = population.value <= residences.value
			// If population is less than or equal to amount of residences,
			// all people have a residence.
			? population.value
			// If not, all spots are filled but some are homeless.
			: residences.value;

		// Deplete lake health depending on the current pollution.
		resources["lake"].delta = -resources["pollution"].value / 100;

		// If lake had no health left, exit the game.
		if (resources["lake"].value <= 0) {
			MessageManager.Warn("You fool! The lake is dead and you have lost the game.");
			Application.Quit();

		}
	}
	// This method check the conditions for population growth
	public void PopulationGrowth()
	{ 

		if (resources["population"].value > 0 && resources["food"].value <= 0)
		{
			resources["population"].delta = -1;
		}

		else if (resources["population"].value >= 2 && resources["food"].value > 0)
		{
			resources["population"].delta = 1;
		}

	}

	// Purchases the given cell by subtracting cost from current cash and
	// updating resource deltas.
	public bool TryPurchase(Cell cell, bool warnEnable)
	{
		if (resources["cash"].value - cell.cost < 0) {
            if (warnEnable) { 
			    MessageManager.Warn("Not enough cash!");
            }
            return false;
		}

		// Subtract cost.
		resources["cash"].value -= cell.cost;

		// Add available residences.
		resources["residences"].value += cell.resources["residences"].value;

		// Update deltas for upkeep.
		foreach (KeyValuePair<string, Resource> pair in cell.resources) {
			resources[pair.Key].delta -= pair.Value.upkeep;
		}

		// If no available jobs are available, produce at full capacity by
		// default.
		if (cell.availableJobs == 0)
			Diff(cell.HireWorkers(0));

		return true;
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
		if (resources["workers"].value + workers > resources["population"].value)
			throw new System.ArgumentException("No more workers are available!", "workers");

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
