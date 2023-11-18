using Player.Input.ButtonPress;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class YellowLeaf : MovingPlatform
{
    private void Awake()
    {
        base.startLoc = transform.position;
        System.Random rand = new System.Random();
        base.currentTime = (float) (6.28 * rand.NextDouble());

    }
    public override Vector2 Position(Vector2 start, float time){
        float sin = (float)Math.Sin(time);
        return start + new Vector2(3 * sin, sin * sin - (time + sin));
    }
}
