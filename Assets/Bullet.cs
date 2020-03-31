using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    [HideInInspector]
    public Player SourcePlayer;

    public void SetSourcePlayer(Player _SourcePlayer)
    {
        SourcePlayer = _SourcePlayer;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //we hit an enemy
            collision.collider.gameObject.GetComponent<Enemy>().TakeDamage(Damage, SourcePlayer);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            //we hit a wall
            Destroy(gameObject);
        }
    }
}
