using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Tile : MonoBehaviour
{
	public int x;
	public int y;

	public abstract void HandleClick();
}
