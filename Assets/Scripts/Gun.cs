using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool held;

    [Space]
    public float ShotsPerSecond;
    public bool Shooting;

    private float TimeSinceShot = 999;

    // Update is called once per frame
    void Update()
    {
        if (Shooting)
        {
            if (TimeSinceShot > (1/ShotsPerSecond))
            {
                Shoot();
                TimeSinceShot = 0;
            }
        }

        TimeSinceShot += Time.deltaTime;
    }

    public void Shoot()
    {
        GetComponent<AudioSource>().Play();
    }
}
