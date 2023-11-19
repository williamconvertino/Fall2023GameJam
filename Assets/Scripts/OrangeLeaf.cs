using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class OrangeLeaf : MovingPlatform
{
    // Start is called before the first frame update


    // Update is called once per frame
    public float alpha;
    public float beta;
    public float gamma;
    public override void Awake()
    {
        base.startLoc = transform.position;
    }

    public override void Update()
    {
        currentTime += Time.deltaTime * gamma;
        base.Update();
    }
    public override Vector3 Position(Vector2 start, float time)
    {
        float sin = (float)System.Math.Sin(time);
        return start + new Vector2(alpha * sin, sin * sin - beta * (time + sin));
    }

    public void OnTransformChildrenChanged()
    {
        gamma = 0.5f * transform.childCount;
        base.startLoc = 2 * transform.position - Position(transform.position, base.currentTime);
        
    }
}

