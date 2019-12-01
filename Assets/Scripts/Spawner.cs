using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float SpawnDelay;
    public GameObject SpawnObject;

    private float TimeSinceLastSpawn = 999;

    [Tooltip("If spawning an enemy, set this to the enemy target gameobject")]
    public GameObject EnemyTarget;

    // Update is called once per frame
    void Update()
    {
        if (TimeSinceLastSpawn >= SpawnDelay)
        {
            GameObject Spawned = Instantiate(SpawnObject, transform.position, transform.rotation);
            if (EnemyTarget != null)
            {
                Spawned.GetComponent<Enemy>().Target = EnemyTarget;
            }
            TimeSinceLastSpawn = 0;
        }

        TimeSinceLastSpawn += Time.deltaTime;
    }
}
