using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject EnemyTarget;

    public List<(GameObject, int)> Wave;

    private float time = 0;

    public void Update()
    {
        if (Wave != null)
        {
            time += Time.deltaTime;
            if (time < 1)
            {
                SpawnWave();
                time += 1;
            }
        }
    }

    public void SpawnWave()
    {
        foreach ((GameObject, int) set in Wave)
        {
            for (int i = 0; i < set.Item2; i++)
            {
                Instantiate(set.Item1, transform.position, Quaternion.identity).GetComponent<Enemy>().Target = EnemyTarget;
            }
        }
    }

    public GameObject Spawn(GameObject enemy)
    {
        GameObject SpawnedEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
        SpawnedEnemy.GetComponent<Enemy>().Target = EnemyTarget;
        return SpawnedEnemy;
    }
}
