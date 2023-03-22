using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    private static SaveData _current;
    public static SaveData current
    {
        get
        {
            if(_current == null)
            {
                _current = new SaveData();
            }
            return _current;
        }
        set
        {
            _current = value;
        }
    }
    public PlayerData playerData;
    public List<PickupData> pickupData;
}

[System.Serializable]
public class PlayerData 
{
    public string id;
    public State playerState;
    public Vector2 position;
    public Vector2 velocity;
    public bool facingLeft;
    public int doubleJumps;
    public string animName;
    public float animTimer;
}

[System.Serializable]
public class PickupData
{
    bool pickedUp;
    Vector2 position;
}