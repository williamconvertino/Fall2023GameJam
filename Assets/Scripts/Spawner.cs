using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    public List<GameObject> patterns;
    public List<GameObject> leaves;
    public List<GameObject> hazzards;
    public float leafRate;
    public float hazzardRate;
    public float patternRate;
    public float patternCooldown;

    private List<GameObject> onCooldown;
    private List<float> cooldowns;

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
        cooldowns[0] = cooldowns[0] - Time.deltaTime;
        while (cooldowns.Count > 0 && cooldowns[0] <= 0)
        {
            patterns.Add(onCooldown[0]);
            onCooldown.RemoveAt(0);
            cooldowns.RemoveAt(0);
        }
        if (rand.NextDouble() <= leafRate * Time.deltaTime) SpawnLeaf();
        if (patterns.Count > 0 && rand.NextDouble() <= patternRate * Time.deltaTime) SpawnPattern();
    }

    void SpawnPattern()
    {
        int index = rand.Next(patterns.Count);
        GameObject pattern = Instantiate(patterns[index], transform);
        onCooldown.Add(patterns[index]);
        // incomplete
    }

    void SpawnLeaf() {
        int index = rand.Next(leaves.Count);
        MovingPlatform leaf = Instantiate(leaves[index], transform).GetComponent<MovingPlatform>();
        leaf.startLoc = new Vector2(leftbound + (float)rand.NextDouble() * (rightbound - leftbound), transform.position.y);
    }

    void SpawnHazard() { }
}
