using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class EndlessMode : MonoBehaviour
{
    public int Level = 1;
    public Spawner[] SpawnPoints;

    [Header("Enemies")]
    public GameObject Enemy;
    public GameObject FastEnemy;

    [Header("Wave Settings")]
    public float WaveIntervalTime;//Time in seconds between waves

    public List<GameObject> CurrentEnemies = new List<GameObject>();

    public void Start()
    {//Spawn an initial wave
        //calculate the amount of enemies we need to spawn, and spawn them
        int EnemyCount = Mathf.CeilToInt(Level * Level / 2);
        int FastEnemyCount = Mathf.CeilToInt((Level * Level / 30) - 1) > 0 ? Mathf.CeilToInt((Level * Level / 30) - 1) : 0;

        foreach (Spawner spawner in SpawnPoints)
        {
            CurrentEnemies.AddRange(spawner.Spawn(Enemy, EnemyCount));
            CurrentEnemies.AddRange(spawner.Spawn(FastEnemy, FastEnemyCount));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentEnemies.Count == 0)
        {//We killed all the enemies
            StartCoroutine(NewWave());
            
        }

        //remove any destroyed enemies
        CurrentEnemies = CurrentEnemies.Where(x => x != null).ToList();
    }

    IEnumerator NewWave()
    {
        //End the wave

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetComponent<Player>().EndWave();
        }

        //Wait for a bit
        yield return new WaitForSeconds(WaveIntervalTime);

        //Start the next wave
        Level++;

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetComponent<Player>().StartWave();
        }

        //calculate the amount of enemies we need to spawn, and spawn them
        int EnemyCount = Mathf.CeilToInt(Level * Level / 2);
        int FastEnemyCount = Mathf.CeilToInt((Level * Level / 30) - 1) > 0 ? Mathf.CeilToInt((Level * Level / 30) - 1) : 0;

        foreach (Spawner spawner in SpawnPoints)
        {
            CurrentEnemies.AddRange(spawner.Spawn(Enemy, EnemyCount));
            CurrentEnemies.AddRange(spawner.Spawn(FastEnemy, FastEnemyCount));
        }     
    }
}
