using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_MS : Boss
{
    public Sprite AttackSprite;
    public Sprite SleepSprite;

    [HideInInspector]
    public int state = 0;//0 is neutral, 1 is attacking, 2 is asleep

    public float NeutralTime;
    public float AttackTime;
    public float SleepTime;

    [Header("Attacks")]
    public Canvas canvas;
    public GameObject CodeProjectile;
    public float CodeDamage;
    public float AttackSpeed;
    private float CurrentShootTime;

    [Header("Movement")]
    public float Y;
    public float MinX;
    public float MaxX;
    private float CurrentMoveTime;

    [HideInInspector]
    public float DamageTakenModifier;

    // Start is called before the first frame update
    void Start()
    {
        DamageTakenModifier = 1;
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = NeutralSprite;
        Health = MaxHealth;
        StartCoroutine(DoBoss());
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0 || state == 1)
        {
            CurrentMoveTime += Time.deltaTime;
            transform.position = new Vector3(Mathf.Sin(CurrentMoveTime) * (MaxX - MinX) / 2, Y);
        }

        if (state == 1)
        {
            CurrentShootTime += Time.deltaTime;
            if (CurrentShootTime >= 1/AttackSpeed)
            {
                GameObject target = GetClosestPlayer();
                GameObject Projectile = Instantiate(CodeProjectile, transform.position, Quaternion.identity);
                Projectile.transform.right = target.transform.position - transform.position;
                Projectile.GetComponent<CodeProjectile>().Damage = CodeDamage;
                Projectile.GetComponent<CodeProjectile>().Movement = (target.transform.position - transform.position).normalized;
                Projectile.transform.SetParent(canvas.transform);
                CurrentShootTime = 0;
            }
        }
    }

    public new IEnumerator DoBoss()
    {
        while (Health > 0)
        {
            //Enter neutral phase
            rend.sprite = NeutralSprite;
            state = 0;
            yield return new WaitForSeconds(NeutralTime);

            //enter Attack Phase
            CurrentShootTime = 0;
            rend.sprite = AttackSprite;
            state = 1;
            yield return new WaitForSeconds(AttackTime);

            //enter sleep phase            
            rend.sprite = SleepSprite;
            state = 2;
            yield return new WaitForSeconds(SleepTime);

        }
    }

    public void OnParticleCollision(GameObject ParticleSystem)
    {
        List<Vector4> data = new List<Vector4>();
        ParticleSystem.GetComponent<ParticleSystem>().GetCustomParticleData(data, ParticleSystemCustomData.Custom1);
        if (state == 2)
        {
            TakeDamage(data[0].x);
        }
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
