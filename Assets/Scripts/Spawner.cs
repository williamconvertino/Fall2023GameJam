using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> patterns;
    public List<GameObject> leaves;
    public List<GameObject> hazzards;
    public float leafRate;
    public float hazzardRate;
    public float patternRate;
    public float patternCooldown;
    private float currentCooldown;
    private System.Random rand;

    public float leftbound;
    public float rightbound;

    private void Start()
    {
        rand = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        if (rand.NextDouble() <= leafRate * Time.deltaTime) SpawnLeaf();
    }

    void SpawnPattern()
    {

    }

    void SpawnLeaf() {
        int index = rand.Next(leaves.Count);
        MovingPlatform leaf = Instantiate(leaves[index], transform).GetComponent<MovingPlatform>();
        leaf.startLoc = new Vector2(leftbound + (float)rand.NextDouble() * (rightbound - leftbound), transform.position.y);
    }

    void SpawnHazard() { }
}
