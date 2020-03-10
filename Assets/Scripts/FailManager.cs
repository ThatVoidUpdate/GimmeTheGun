using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailManager : MonoBehaviour
{
    public GameObject FailOverlay;
    private bool HasFailed = false;
    
    public void OnFail()
    {
        if (!HasFailed)
        {
            FailOverlay.SetActive(true);
            foreach (Enemy enemy in GameObject.FindObjectsOfType<Enemy>())
            {
                enemy.Speed = 0;
            }

            HasFailed = true;
        }
    }
}
