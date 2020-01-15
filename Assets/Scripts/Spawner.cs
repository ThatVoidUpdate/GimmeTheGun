using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Tooltip("If spawning an enemy, set this to the enemy target gameobject")]
    public GameObject EnemyTarget;

    public List<(GameObject, int)> Wave;

    private float time = 0;

    public void Update()
    {
        time += Time.deltaTime;
        if (time < 1)
        {
            Spawn();
            time += 1;
        }
    }

    public void Spawn()
    {
        foreach ((GameObject, int) set in Wave)
        {
            for (int i = 0; i < set.Item2; i++)
            {
                Instantiate(set.Item1, transform.position, Quaternion.identity).GetComponent<Enemy>().Target = EnemyTarget;
            }
        }
    }
}
