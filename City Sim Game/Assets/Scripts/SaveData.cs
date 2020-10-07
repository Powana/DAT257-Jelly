using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    private static SaveData _current;
    private static SaveData current
    {
        get
        {
            if(_current == null)
            {
                _current = new SaveData();
            }

            return _current;
        }
    }

    public PlayerProfile profile;
}
