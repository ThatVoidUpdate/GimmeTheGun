using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_Manager : MonoBehaviour
{
    public GameObject StandardEnemy;
    public GameObject NinjaEnemy;
    public GameObject FastEnemy;

    public GameObject Boss;

    public List<(GameObject, int)[]> Waves = new List<(GameObject, int)[]>();

    public List<GameObject> CurrentEnemies;
    private GameObject[] spawners;
    private int CurrentWave = -1;

    // Start is called before the first frame update
    void Start()
    {
        Waves.Add(new (GameObject, int)[]
        {
            (StandardEnemy, 3)
        });

        Waves.Add(new (GameObject, int)[]
         {
            (StandardEnemy, 5)
         });

        Waves.Add(new (GameObject, int)[]
         {
            (StandardEnemy, 7)
         });

        Waves.Add(new (GameObject, int)[]
         {
            (StandardEnemy, 9)
         });

        Waves.Add(new (GameObject, int)[]
         {           
            (StandardEnemy, 10),
            (FastEnemy, 1)
         });

        Waves.Add(new (GameObject, int)[]
         {
            (StandardEnemy, 10),
            (FastEnemy, 2)
         });

        Waves.Add(new (GameObject, int)[]
         {
            (StandardEnemy, 12),
            (FastEnemy, 2)
         });

        Waves.Add(new (GameObject, int)[]
         {
            (StandardEnemy, 14),
            (FastEnemy, 2)
         });

        Waves.Add(new (GameObject, int)[]
         {
            (StandardEnemy, 14),
            (FastEnemy, 2),
            ( NinjaEnemy, 1)
         });

        Waves.Add(new (GameObject, int)[]
         {
            (StandardEnemy, 14),
            (FastEnemy, 2),
            ( NinjaEnemy, 2)
         });


        spawners = GameObject.FindGameObjectsWithTag("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentEnemies.Count == 0)
        {//All enemies have been killed. MOAR ENEMIES
            CurrentWave++;

            if (CurrentWave > Waves.Count)
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
