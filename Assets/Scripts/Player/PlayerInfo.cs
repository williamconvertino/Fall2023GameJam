using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerInfo : MonoBehaviour
{
    public String playerName = "Player";
    public int playerID = 999;

    private Vector3 nameTagWorldPosition;
    private Vector3 nameTagOffset;

    private void Start()
    {
        nameTagOffset = new Vector3(-5, 50,0);
    }

    private void Update()
    {
        nameTagWorldPosition = Camera.main.WorldToScreenPoint(transform.position) + nameTagOffset;
    }

    private void OnGUI()
    {
        //Draws a nametag
        GUI.Label(new Rect(nameTagWorldPosition.x, Screen.height-nameTagWorldPosition.y, 100, 50), playerName );
    }
}