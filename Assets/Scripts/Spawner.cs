using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Tooltip("If spawning an enemy, set this to the enemy target gameobject")]
    public GameObject EnemyTarget;

    [Space]
    public List<Wave> waves;
    public int CurrentWave = 1;

    public void SpawnEnemy(GameObject Enemy)
    {
        if (Enemy != null)
        {
            Instantiate(Enemy, transform.position, transform.rotation).GetComponent<Enemy>().Target = EnemyTarget;
        }
    }
}
