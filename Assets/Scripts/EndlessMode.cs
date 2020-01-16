using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EndlessMode : MonoBehaviour
{
    public int Level = 0;
    public Spawner[] SpawnPoints;

    [Header("Enemies")]
    public GameObject Enemy;

    public List<GameObject> CurrentEnemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentEnemies.Count == 0)
        {
            //calculate the amount of enemies we need to spawn, and spawn them
            int enemyCount = 1;

            for (int i = 0; i < enemyCount; i++)
            {
                foreach (Spawner spawner in SpawnPoints)
                {
                    CurrentEnemies.Add(spawner.Spawn(Enemy));
                }
            }
        }

        //remove any destroyed enemies
        CurrentEnemies = CurrentEnemies.Where(x => x != null).ToList();
    }
}
