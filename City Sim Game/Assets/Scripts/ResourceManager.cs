using System;
using System.Collections;
using System.Collections.Generic;
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
				"cash", "population", "food", "energy", "pollution"
			}) {
			resources.Add(resource, new Resource(resource));
		}
	}

	// Should be called by game loop every period.
	public void Tick()
	{
		// Update resources depending on their upkeep/production.
		foreach (KeyValuePair<string, Resource> pair in resources) {
			pair.Value.value += pair.Value.delta;
		}
		Debug.Log(ToString());
	}

	// Purchases the given cell by subtracting cost from current cash and
	// updating resource deltas.
	public void Purchase(Cell cell)
	{
		// Subtract cost.
		resources["cash"].value -= cell.cost;

		// Update deltas for upkeep and production.
		foreach (KeyValuePair<string, Resource> pair in cell.resources) {
			resources[pair.Key].delta += pair.Value.delta;
		}
	}

	// Purchases the given cell by subtracting cost from current cash and
	// updating resource deltas.
	public void Sell(Cell cell)
	{
		// Sell for a smaller amount than it was bought for balance.
		resources["cash"].value += (cell.cost / 4);

		// Iterate resources to restore balance from before cell was bought.
		foreach (KeyValuePair<string, Resource> pair in cell.resources) {
			resources[pair.Key].delta -= pair.Value.delta;
		}
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
}
