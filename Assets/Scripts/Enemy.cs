using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float Speed;
    public GameObject Target;

    private float Health;
    public float MaxHealth;

    public float Damage;// The damage to do to a player

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(Target.transform);
        //transform.Translate((Target.transform.position - transform.position).normalized * Speed * Time.deltaTime);]
        rb.MovePosition(transform.position + (Target.transform.position - transform.position).normalized * Speed * Time.deltaTime);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }

    }

    public void OnParticleCollision(GameObject other)
    {
        List<Vector4> data = new List<Vector4>();
        other.GetComponent<ParticleSystem>().GetCustomParticleData(data, ParticleSystemCustomData.Custom1);
        TakeDamage(data[0].x);        
    }

    public void TakeDamage(float DamageAmount)
    {
        Health -= DamageAmount;
        if (Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (GetComponent<Smash>())
        {
            GetComponent<Smash>().DoSmash();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
