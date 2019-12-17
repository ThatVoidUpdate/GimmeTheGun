using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAnimation : MonoBehaviour
{
    public LevelManager manager;

    public void WaveTrigger(GameObject Enemy)
    {
        manager.SpawnEnemies(Enemy);
    }
}
