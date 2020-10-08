using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveData : MonoBehaviour
{
    public Map map;
    private static string DATA_PATH = "/MyGame.dat";
    private static PlayerProfile player;
    
    
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        
    }
    public void Save(PlayerProfile player)
    {
        FileStream file = null;

        try
        {
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
}
