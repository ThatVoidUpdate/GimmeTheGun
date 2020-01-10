using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAnimation : MonoBehaviour
{

    public List<GameObject> Spawners;

    public void WaveTrigger(GameObject Enemy)
    {
        foreach (GameObject spawner in Spawners)
        {
            spawner.GetComponent<Spawner>()?.SpawnEnemy(Enemy);
        }
    }
}
