using Player.Input.ButtonPress;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class YellowLeaf : MovingPlatform
{

    public float alpha;
    public float beta;
    private void Awake()
    {
        System.Random rand = new System.Random();
        base.currentTime = (float) (6.28 * rand.NextDouble());
        base.startLoc = transform.position - Position(startLoc, currentTime);
    }
    public override Vector3 Position(Vector2 start, float time){
        float sin = (float)Math.Sin(time);
        return start + new Vector2(alpha * sin, sin * sin - beta*(time + sin));
    }
}
