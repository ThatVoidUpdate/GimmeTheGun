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
    public GameObject bullet;
    public float BulletSpeed = 10;

    [Space]
    public float HeldDistance;
    public GameObject BulletSpawn;

    [Space]
    public GunType type;

    private float TimeSinceShot = 999;
    private SpriteRenderer rend;

    private bool UsingParticleSystem = false;
    private bool GunSide; //Left is true, right is false

    private ParticleSystem system;

    [HideInInspector]
    public bool OnLeftSideOfScreen;

    public void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        system = GetComponentInChildren<ParticleSystem>();
        if (system != null)
        {//We are using a particle system, so switch over to particle systems
            UsingParticleSystem = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        print("X position: " + transform.position.x + ", picked up on left side: " + OnLeftSideOfScreen + ", Current particle system position: " + system.transform.position.x);
        //Debug.DrawLine(Vector3.zero, system.transform.localPosition);
        Debug.Log(system + " " + system.transform.localPosition);
        
        if (Shooting && (system.transform.position.x < 0) == OnLeftSideOfScreen)
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

        if (angle < -270 - 45 && GunSide)
        {//gun is pointing more clockwise than up-right. if its on the left side, it needs to be on the right
            GunSide = false;
        }

        if (angle > -270 + 45 && !GunSide)
        {//gun is pointing more anticlockwise than up-left. if its on the right side, it needs to be on the left
            GunSide = true;
        }

        if (GunSide)
        {//gun is on the left side of the player
            transform.position = PlayerPosition + new Vector2(-HeldDistance, 0);
            transform.eulerAngles = new Vector3(0, 0, angle);
            transform.localScale = new Vector2(1, -1);
        }
        else
        {//gun is on the right side of hte player
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
