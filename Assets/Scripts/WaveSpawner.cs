using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject StandardEnemy;
    public GameObject NinjaEnemy;
    public GameObject FastEnemy;

    public GameObject Boss;

    public List<(GameObject, int)[]> Waves = new List<(GameObject, int)[]>();

    private List<GameObject> CurrentEnemies = new List<GameObject>();
    private GameObject[] spawners;
    private int CurrentWave = -1;

    // Start is called before the first frame update
    void Start()
    {
    
        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 4) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 6) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 8),  (FastEnemy, 2) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 10), (FastEnemy, 3) });

        Waves.Add(new (GameObject, int)[] { (FastEnemy, 10) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 11), (FastEnemy, 3) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 12), (FastEnemy, 3), (NinjaEnemy, 2) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 14), (FastEnemy, 4) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 16), (FastEnemy, 3), (NinjaEnemy, 3) });

        Waves.Add(new (GameObject, int)[] { (FastEnemy, 12), (NinjaEnemy, 4) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 8), (FastEnemy, 5), (NinjaEnemy, 2) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 6), (FastEnemy, 6), (NinjaEnemy, 6) });


        spawners = GameObject.FindGameObjectsWithTag("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentEnemies.Count == 0)
        {//All enemies have been killed. MOAR ENEMIES
            CurrentWave++;

            if (CurrentWave > Waves.Count && Boss != null)
            {//We defeated all waves. time for the boss battle
                Boss.SetActive(true);
            }

            foreach (GameObject spawner in spawners)
            {
                foreach ((GameObject, int) set in Waves[CurrentWave])
                {
                    CurrentEnemies.AddRange(spawner.GetComponent<Spawner>().Spawn(set.Item1, set.Item2));
                }
            }
        }
        else
        {
            CurrentEnemies.RemoveAll(item => item == null);
        }
    }
}
