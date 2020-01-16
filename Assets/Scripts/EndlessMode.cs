using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EndlessMode : MonoBehaviour
{
    public int Level = 1;
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
            int enemyCount = Mathf.CeilToInt(Level * Level / 20);
            foreach (Spawner spawner in SpawnPoints)
            {
                CurrentEnemies.AddRange(spawner.Spawn(Enemy, enemyCount));
            }

            Level++;
        }

        //remove any destroyed enemies
        CurrentEnemies = CurrentEnemies.Where(x => x != null).ToList();
    }
}
