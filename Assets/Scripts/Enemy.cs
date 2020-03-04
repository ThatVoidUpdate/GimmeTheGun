using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int ScoreValue = 10;

    public float Speed;
    public GameObject Target;

    private float Health;
    public float MaxHealth;

    public float Damage;// The damage to do to a player

    public float DamageTakenModifier = 1;

    private Rigidbody2D rb;

    private Player LastDamagedBy;
    private bool IsSpinning = false;

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.LookAt(Target.transform);
        //transform.Translate((Target.transform.position - transform.position).normalized * Speed * Time.deltaTime);]
        rb.MovePosition(transform.position + (Target.transform.position - transform.position).normalized * Speed);

        if (IsSpinning)
        {
            GetComponentInChildren<SpriteRenderer>().transform.Rotate(new Vector3(0, 0, 1));
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

    }

    public void OnParticleCollision(GameObject ParticleSystem)
    {
        List<Vector4> data = new List<Vector4>();
        ParticleSystem.GetComponent<ParticleSystem>().GetCustomParticleData(data, ParticleSystemCustomData.Custom1);
        TakeDamage(data[0].x);
        //LastDamagedBy = Physics2D.OverlapCircleAll(GameObject.FindGameObjectWithTag("Gun").transform.position, 2f, LayerMask.GetMask("Player"))[0].GetComponent<Player>();
    }

    public void TakeDamage(float DamageAmount)
    {
        Health -= DamageAmount * DamageTakenModifier;
        if (Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //LastDamagedBy.KillEnemy();

        if (GetComponent<Smash>())
        {
            GetComponent<Smash>().DoSmash();
        }
        else
        {
            Destroy(this.gameObject);
            ScorePlayers.score += ScoreValue;
        }
    }

    public void Spin(bool DoSpin)
    {
        if (DoSpin)
        {//start spinning
            IsSpinning = true;
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().gameObject.transform.rotation = Quaternion.identity;
            IsSpinning = false;
        }
    }
}
