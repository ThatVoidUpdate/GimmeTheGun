using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool held;

    [Space]
    public float ShotsPerSecond;
    public bool Shooting;

    [Space]
    public GameObject bullet;
    public float BulletSpeed = 10;

    private float TimeSinceShot = 999;

    // Update is called once per frame
    void Update()
    {
        if (Shooting && held)
        {
            if (TimeSinceShot > (1/ShotsPerSecond))
            {
                Shoot();
                TimeSinceShot = 0;
            }
        }

       

        TimeSinceShot += Time.deltaTime;

        Debug.DrawRay(transform.position + transform.up * 0.3f, transform.up * 999, Color.blue);
    }

    public void Shoot()
    {
        GetComponent<AudioSource>().Play();

        GameObject currentBullet = Instantiate(bullet, transform.position + transform.up * 0.3f, transform.rotation);
        currentBullet.GetComponent<Rigidbody2D>().velocity = currentBullet.transform.up * BulletSpeed;

        


    }
}
