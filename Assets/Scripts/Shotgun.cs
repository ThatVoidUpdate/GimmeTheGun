using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Shotgun : Gun
{
    [Header("Shotgun Pellet Settings")]
    public float SpreadAngle = 15;
    public float PelletAmount = 10;

    public new void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Shooting && (BulletSpawnPosition.transform.position.x < 0) == OnLeftSideOfScreen)
        {
            if (TimeSinceShot > (1 / ShotsPerSecond))
            {
                Shoot();
                TimeSinceShot = 0;
            }
        }

        TimeSinceShot += Time.deltaTime;
    }

    public new void SetAngle(float angle, Vector2 PlayerPosition)
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
        else if (GunSide == Direction.Right)
        {//gun is on the right side of hte player
            transform.position = PlayerPosition + new Vector2(HeldDistance, 0);
            transform.eulerAngles = new Vector3(0, 0, angle);
            transform.localScale = new Vector2(1, 1);
        }
    }

    public new void Shoot()
    {
        GetComponent<AudioSource>().Play();

        //spawn the bullet
        for (int i = 0; i < PelletAmount; i++)
        {
            GameObject bullet = Instantiate(BulletPrefab, BulletSpawnPosition.transform.position, Quaternion.identity);
            bullet.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0,0,Random.Range(-SpreadAngle / 2, SpreadAngle / 2)));
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed * bullet.transform.right;

            RaycastHit2D[] closeObjects = Physics2D.CircleCastAll(transform.position, HeldDistance * 5, Vector3.forward);

            foreach (RaycastHit2D hit in closeObjects)
            {
                if (hit.collider.gameObject.GetComponent<Player>() != null)
                {
                    bullet.GetComponent<Bullet>().SetSourcePlayer(hit.collider.gameObject.GetComponent<Player>());
                }
            }
        }
    }
}
