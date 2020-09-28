using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource
{
	public int value = 0;
	public int delta = 0;
	public string resource;

	public Resource(string resource, int value, int delta)
	{
		this.resource = resource;
		this.value = value;
		this.delta = delta;
	}

	public Resource(string resource)
	{
		this.resource = resource;
	}

	public override string ToString()
	{
		return resource + "{value:" + value + "}, {delta:" + delta + "}";
	}
}
