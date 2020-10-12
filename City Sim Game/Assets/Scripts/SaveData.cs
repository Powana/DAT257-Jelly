using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Schema;
using System.Security;
using System.Runtime.ExceptionServices;

public class SaveData : MonoBehaviour
{
    public Map map;
    private static string DATA_PATH = "/MyGame.dat";
    private static PlayerProfile player;
    private List<(int, int)> positions;
    private List<Cell> _tilecells;
    private Dictionary<(int, int), Cell> _tiles;
    private Dictionary<string, Resource> _resources;
    private List<(int, int, int, string)> CellsList;
    private List<(int, int, string)> ResourcesList;
    private List<string> ResourceNameList;
    private List<Resource> CellResourcesList;
    private Cell temp;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
      
    }
    public void Save()
    {
        FileStream file = null;
       
        
        try
        {
            OnBeforeSerialize();
            PlayerProfile player = new PlayerProfile("name", map.GetManager(), positions, CellsList, ResourceNameList, CellResourcesList);
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + DATA_PATH);
            bf.Serialize(file, player);
           
        }
        catch (Exception e)
        {
            if(e != null)
            {
                //handle exception
                print(e.ToString());

            }
        }
        finally
        {
            if(file != null)
            {
                file.Close();
            }
        }
    }

    public static PlayerProfile Load()
    {
        FileStream file = null;

        try
        {
            
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + DATA_PATH, FileMode.Open);

            player = bf.Deserialize(file) as PlayerProfile;
        }
        catch(Exception e)
        {
            print(e.ToString());
        }
        finally
        {
            if(file != null)
            {
                file.Close();
            }
            
        }
        return player;
    }

    public void OnBeforeSerialize()
    {
        foreach(var kvp in map.GetTiles())
        {
            positions.Add(kvp.Key);
            _tilecells.Add(kvp.Value);
        }
        CellstoList(_tilecells);
        
        
    }

    public void OnAfterDeserialize()
    {
        
        ListToCell(CellsList, ResourceNameList, CellResourcesList);

        _tiles = new Dictionary<(int, int), Cell>();
        
        for(int i= 0; i != Math.Min(positions.Count, _tilecells.Count); i++)
        {
            _tiles.Add(positions[i], _tilecells[i]);
        }
    }

    public void CellstoList(List<Cell> cell)
    {

        CellsList = new List<(int, int, int, string)>();
        ResourceNameList = new List<string>();
        CellResourcesList = new List<Resource>();

        foreach (var i in cell)
        {
            int cost = i.cost;
            int avalibleJobs = i.availableJobs;
            int takenJobs = i.takenJobs;
            string spritePath = i.getSpritePath();
            CellsList.Add((cost, avalibleJobs, takenJobs, spritePath));

            foreach (var kvp in i.resources)
            {
                ResourceNameList.Add(kvp.Key);
                CellResourcesList.Add(kvp.Value);
            }
        }
         
    }
    /*
    public void ResourcesToList(List<Resource> resource)
    {
        ResourcesList.Clear();
        foreach (var res in resource)
        {
            int value = res.value;
            int delta = res.delta;
            //int upkeep = res.upkeep;
            string name = res.resource;
            ResourcesList.Add((value, delta, name));
        }
    }*/
    /*
    public void ListToResource(List<(int, int, string)> resource)
    {
        CellResourcesList.Clear();
        foreach(var i in resource)
        {
            Resource temp = new Resource(i.Item3, i.Item2, i.Item1);
            CellResourcesList.Add(temp);
        }

    }*/

    public void ListToCell(List<(int, int, int ,string)> Cell, List<string> ResourceName, List<Resource> CellResource)
    {
        foreach (var i in Cell)
        {
            temp.cost = i.Item1;
            temp.availableJobs = i.Item2;
            temp.takenJobs = i.Item3;
            temp.spritePath = i.Item4;

            for (int j = 0; j != Math.Min(ResourceName.Count, CellResource.Count); j++)
            {
                temp.resources.Add(ResourceName[j], CellResource[j]);
            }

            _tilecells.Add(temp);

        }
    }
}
