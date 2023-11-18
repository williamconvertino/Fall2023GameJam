using System;
using Player.Input.ButtonPress;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public String playerName;
    public int playerID;
    public GameObject playerInputPrefab;
    public Color spriteColor;
    
    //Not implemented yet -> just add it to the GameManager part that instantiates the player prefab
    public Sprite headSprite;
    
}