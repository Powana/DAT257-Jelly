using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerProfile
{
    private string _name;
    private int _cash;
    private int _population;
    private int _food;
    private int _energy;
    private int _pollution;
    private int _workers;
    private int _lake;

    public PlayerProfile()
    {

    }

    public PlayerProfile(string name, ResourceManager PlayerResources)
    {
        _name = name;
        _cash = PlayerResources.resources["cash"].value;
        _population = PlayerResources.resources["population"].value;
        _food = PlayerResources.resources["food"].value;
        _energy = PlayerResources.resources["energy"].value;
        _pollution = PlayerResources.resources["pollution"].value;
        _workers = PlayerResources.resources["workers"].value;
        _lake = PlayerResources.resources["lake"].value;
    }

    public string Name
    {
        get { return _name; }

        set { _name = value; }
    }

    public int Cash
    {
        get { return _cash; }

        set { _cash = value; }
    }

    public int Population
    {
        get { return _population; }

        set { _population = value; }
    }
    public int Food
    {
        get { return _food; }

        set { _food = value; }
    }
    public int Energy
    {
        get { return _energy; }

        set { _energy = value; }
    }
    public int Pollution
    {
        get { return _pollution; }

        set { _pollution = value; }
    }
    public int Workers
    {
        get { return _workers; }

        set { _workers = value; }
    }
    public int Lake
    {
        get { return _lake; }

        set { _lake = value; }
    }
}
