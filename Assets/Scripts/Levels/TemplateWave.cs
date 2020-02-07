using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateWave : MonoBehaviour
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
        /*
         * This is how to make a level of waves:
         * - Copy this script
         * - Rename the file. The filename must not have any spaces in it
         * - Change "public class TemplateWave : MonoBehaviour" to "public class FILENAME : MonoBehaviour"
         * THE NAME AT THE TOP OF THE FILE MUST BE THE EXACT SAME AS THE FILENAME. IF YOU COMPLAIN THAT UNITY WONT FIND THE SCRIPT, THIS WILL ALMOST CERTAINLY BE THE PROBLEM
         * - every Waves.Add is a new wave of enemies in the level. Edit each one, including the currect gameobject name and the amount you want to spawn. The examples are for a standard progression of enemies
         * - Save the file, and add it to an empty gameobject in the scene.
         */
        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 3) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 5) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 7) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 9) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 10), (FastEnemy, 1) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 10), (FastEnemy, 2) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 12), (FastEnemy, 2) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 14), (FastEnemy, 2) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 14), (FastEnemy, 2), (NinjaEnemy, 1) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 14), (FastEnemy, 2), (NinjaEnemy, 2) });


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
