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
    

    //Dictionaries to be saved
    private Dictionary<(int, int), Cell> _tiles;
    private Dictionary<string, Resource> _resources;
    private ResourceManager _resourceManager;
    private List<(int, int, int, string)> CellsList;
    
   
    //Breakdown for tiles dictionary in map
    private List<(int, int)> PositionsList;
    private List<Cell> TileCellsList;

    //Breakdown for a cells
    private Cell temp;
    private List<Resource> CellResourcesList;
    private List<(int, int, int, string)> CellData;
    private List<string> res;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void AssignData()
    {
        _tiles = new Dictionary<(int, int), Cell>(map.GetTiles());
        _resourceManager = map.GetManager();
    }


    private void Update()
    {
      
    }
    public void Save()
    {
        FileStream file = null;
        AssignData();
        OnBeforeSerialize();

        try
        {
            
            PlayerProfile player = new PlayerProfile("name", _resourceManager, PositionsList);
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
        PositionsList = new List<(int, int)>();
        TileCellsList = new List<Cell>();
        foreach(var kvp in map.GetTiles())
        {
            PositionsList.Add(kvp.Key);
            TileCellsList.Add(kvp.Value);
        }
     
    }

    public void CellToList(List<Cell> cells)
    {
        CellData = new List<(int, int, int, string)>();
        CellResourcesList = new List<Resource>();
        res = new List<string>();
        foreach(var c in cells)
        {
            CellData.Add((c.cost, c.availableJobs, c.takenJobs, c.spritePath));

            foreach(var kvp in c.resources)
            {
                res.Add(kvp.Key);
                CellResourcesList.Add(kvp.Value);
            }
        }
    }

    public void OnAfterDeserialize()
    {
        
        
    }
}
