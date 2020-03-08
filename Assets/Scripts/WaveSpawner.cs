using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public GameObject StandardEnemy;
    public GameObject EnemySF;
    public GameObject EnemyMD;
    public GameObject EnemyRV;
    public GameObject NinjaEnemy;
    public GameObject FastEnemy;
    public GameObject PoisonEnemy;
    public GameObject HexagonBloated;
    public GameObject BeefyHexagon;

    public GameObject Boss;

    public TextMeshProUGUI WaveCounter;

    public List<(GameObject, int)[]> Waves = new List<(GameObject, int)[]>();

    private List<GameObject> CurrentEnemies = new List<GameObject>();
    private GameObject[] spawners;
    public int CurrentWave = -1;

    // Start is called before the first frame update
    void Start()
    {
   
        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 2), (EnemySF, 2) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 4), (EnemyMD, 2) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 4), (EnemyRV, 1), (EnemyMD, 1), (EnemySF, 1), (FastEnemy, 2) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 5), (EnemyMD, 3), (EnemySF, 2), (FastEnemy, 5) });

        Waves.Add(new (GameObject, int)[] { (FastEnemy, 10) });

        Waves.Add(new (GameObject, int)[] { (FastEnemy, 2), (PoisonEnemy, 2) });
        
        Waves.Add(new (GameObject, int)[] { (FastEnemy, 4), (PoisonEnemy, 5) });

        Waves.Add(new (GameObject, int)[] { (FastEnemy, 6), (PoisonEnemy, 6) });

        Waves.Add(new (GameObject, int)[] { (FastEnemy, 7), (PoisonEnemy, 7) });

        Waves.Add(new (GameObject, int)[] { (FastEnemy, 10), (PoisonEnemy, 10) }); 

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 4), (EnemySF, 4), (NinjaEnemy, 2), (PoisonEnemy, 2) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 1), (EnemyMD, 2), (EnemySF, 1), (EnemyRV, 2), (NinjaEnemy, 6) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 4), (EnemySF, 2), (NinjaEnemy, 6), (PoisonEnemy, 4) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 2), (EnemyMD, 1), (EnemySF, 1), (FastEnemy, 5), (NinjaEnemy, 10) });

        Waves.Add(new (GameObject, int)[] { (PoisonEnemy, 20) });

        Waves.Add(new (GameObject, int)[] { (BeefyHexagon, 1), (FastEnemy, 8), (NinjaEnemy, 3) });

        Waves.Add(new (GameObject, int)[] { (BeefyHexagon, 2), (PoisonEnemy, 8), (NinjaEnemy, 8) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 3), (EnemyRV, 2), (EnemyMD, 3), (FastEnemy, 10), (NinjaEnemy, 5) });

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 3), (EnemySF, 2), (FastEnemy, 16), (HexagonBloated, 3) }); 

        Waves.Add(new (GameObject, int)[] { (StandardEnemy, 20), (NinjaEnemy, 20), (HexagonBloated, 5) });

        spawners = GameObject.FindGameObjectsWithTag("Spawner");
    }

    // Update is called once per frame
    void Update()
    {


        if (CurrentEnemies.Count == 0)
        {//All enemies have been killed. MOAR ENEMIES
            CurrentWave++;
            WaveCounter.text = "Wave: " + CurrentWave.ToString();

            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                player.GetComponent<Player>().StartWave();
            }

            if (CurrentWave > Waves.Count && Boss != null && Boss.activeSelf == false)
            {//We defeated all waves. time for the boss battle
                Boss.SetActive(true);
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                {
                    FindObjectOfType<BarkEvents>().TriggerBarkLine(BarkEventTypes.Boss, player);
                }
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
