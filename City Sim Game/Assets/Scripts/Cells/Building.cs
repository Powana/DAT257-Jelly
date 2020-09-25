using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Abstract class to be used when implenting buildings.
abstract public class Building : Cell
{
    // Name of the building, as used in shop interface etc.
    protected string stringName;

    // Building-specific stats are set in the constructor.
    protected Building()
    {
        // Use placeholder constructer to make sure constructer is defined in inhereted building classes.
        stringName = "UNDEFINED";
    }

    // Kinda seems a bit silly to have a building class when there aren't really any variables differing a building to a disctrict / any other cell. Maybe more will be needed later.
    // It does however lead to well structured code which is nice.

	
}
