using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public float Damage;
    public float LifeTime;
    private float AliveTime;

    // Update is called once per frame
    void Update()
    {
        AliveTime += Time.deltaTime;
        if (AliveTime > LifeTime)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {//we hit a player, do some damage to them
            collision.gameObject.GetComponent<Player>().TakeDamage(Damage);
            Destroy(gameObject);  
        }
    }
}
