using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType {Pistol, RocketLauncher, Flamethrower, Shotgun}


[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Gun : MonoBehaviour
{
    [Space]
    public float ShotsPerSecond;
    public bool Shooting;
    

    [Space]
    public float HeldDistance;

    [Space]
    public GunType type;

    private float TimeSinceShot = 999;
    private SpriteRenderer rend;

    private Direction GunSide; //Left is true, right is false

    public float bulletSpeed;
    public GameObject BulletPrefab;
    public GameObject BulletSpawnPosition;

    [HideInInspector]
    public bool OnLeftSideOfScreen;

    public void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {        
        if (Shooting && (BulletSpawnPosition.transform.position.x < 0) == OnLeftSideOfScreen)
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

        if (angle < -270 - 45 && GunSide != Direction.Right)
        {//gun is pointing more clockwise than up-right. if its on the left side, it needs to be on the right
            GunSide = Direction.Right;
        }

        if (angle > -270 + 45 && GunSide != Direction.Left)
        {//gun is pointing more anticlockwise than up-left. if its on the right side, it needs to be on the left
            GunSide = Direction.Left;
        }

        if (GunSide == Direction.Left)
        {//gun is on the left side of the player
            transform.position = PlayerPosition + new Vector2(-HeldDistance, 0);
            transform.eulerAngles = new Vector3(0, 0, angle);
            transform.localScale = new Vector2(1, -1);
        }
        else if(GunSide == Direction.Right)
        {//gun is on the right side of hte player
            transform.position = PlayerPosition + new Vector2(HeldDistance, 0);
            transform.eulerAngles = new Vector3(0, 0, angle);
            transform.localScale = new Vector2(1, 1);
        }
    }

    public void Shoot()
    {
        GetComponent<AudioSource>().Play();

        //spawn the bullet
        GameObject bullet = Instantiate(BulletPrefab, BulletSpawnPosition.transform.position, Quaternion.identity);
        bullet.transform.rotation = transform.rotation;
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed * transform.right;

    }
}
