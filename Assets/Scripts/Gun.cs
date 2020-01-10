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

    //[Header("Graphics")]
    //public Sprite Graphic;

    private float TimeSinceShot = 999;
    private SpriteRenderer rend;

    public void Start()
    {
        rend = GetComponent<SpriteRenderer>();
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
            transform.position = PlayerPosition + new Vector2(-1.2f, 0);
            transform.eulerAngles = new Vector3(0, 0, angle);
            transform.localScale = new Vector2(1, -1);
        }
        else if (angle < -270)
        {
            //rend.sprite = Right;
            transform.position = PlayerPosition + new Vector2(1.2f, 0);
            transform.eulerAngles = new Vector3(0, 0, angle);
            transform.localScale = new Vector2(1, 1);

        }


        
    }

    public void Shoot()
    {
        GetComponent<AudioSource>().Play();

        GameObject currentBullet = Instantiate(bullet, transform.position, transform.rotation);
        currentBullet.GetComponent<Rigidbody2D>().velocity = currentBullet.transform.right * BulletSpeed;
    }
}
