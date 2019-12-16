using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject SpawnObject;

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

    }

    IEnumerator NextWave()
    {
        for (int i = 0; i < waves[CurrentWave-1].Enemies.Length; i++)
        {
            for (int j = 0; j < waves[CurrentWave-1].Amounts[i]; j++)
            {
                GameObject enemy = Instantiate(waves[CurrentWave-1].Enemies[i], transform.position, Quaternion.identity);
                enemy.GetComponent<Enemy>().Target = EnemyTarget;
                yield return new WaitForSeconds(1f);
            }

        }
    }
}
