using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Space]
    public float ShotsPerSecond;
    public bool Shooting;

    [Space]
    public GameObject bullet;
    public float BulletSpeed = 10;

    [Space]
    public float HeldDistance;
    public GameObject BulletSpawn;

    private float TimeSinceShot = 999;
    private SpriteRenderer rend;

    private bool UsingParticleSystem = false;

    public void Start()
    {
        rend = GetComponent<SpriteRenderer>();

        if (GetComponentInChildren<ParticleSystem>())
        {//We are using a particle system, so switch over to particle systems
            UsingParticleSystem = true;
        }
    }

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

    public void SetAngle(float angle, Vector2 PlayerPosition)
    {
        if (angle > -270 )
        {
            //rend.sprite = Left;
            transform.position = PlayerPosition + new Vector2(-HeldDistance, 0);
            transform.eulerAngles = new Vector3(0, 0, angle);
            transform.localScale = new Vector2(1, -1);
        }
        else if (angle < -270)
        {
            //rend.sprite = Right;
            transform.position = PlayerPosition + new Vector2(HeldDistance, 0);
            transform.eulerAngles = new Vector3(0, 0, angle);
            transform.localScale = new Vector2(1, 1);
        }        
    }

    public void Shoot()
    {
        GetComponent<AudioSource>().Play();

        if (UsingParticleSystem)
        {
            GetComponentInChildren<ParticleSystem>().Play();
        }
        else
        {
            GameObject currentBullet = Instantiate(bullet, BulletSpawn.transform.position, transform.rotation);
            currentBullet.GetComponent<Rigidbody2D>().velocity = currentBullet.transform.right * BulletSpeed;
        }
    }
}
