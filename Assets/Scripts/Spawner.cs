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

    [Space]
    public List<Wave> waves;
    public int CurrentWave = 1;

    public void Start()
    {
        StartCoroutine(NextWave());
    }

    // Update is called once per frame
    void Update()
    {
        //if (TimeSinceLastSpawn >= SpawnDelay)
        //{
        //    GameObject Spawned = Instantiate(SpawnObject, transform.position, transform.rotation);
        //    if (EnemyTarget != null)
        //    {
        //        Spawned.GetComponent<Enemy>().Target = EnemyTarget;
        //    }
        //    TimeSinceLastSpawn = 0;
        //}

        //TimeSinceLastSpawn += Time.deltaTime;
    }

    IEnumerator NextWave()
    {
        for (int i = 0; i < waves[CurrentWave].Enemies.Length; i++)
        {
            for (int j = 0; j < waves[CurrentWave].Amounts[i]; j++)
            {
                Instantiate(waves[CurrentWave].Enemies[i], transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1f);
            }

        }
    }
}
