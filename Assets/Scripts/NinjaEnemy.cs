using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaEnemy : Enemy
{
    public float TeleportTime;
    public float ThrowTime;
    private bool HasThrown = false;
    private float CurrentTime;

    [Header("Teleport Zone")]
    public Vector2 TopLeftTeleportZone;
    public Vector2 BottomRightTeleportZone;

    [Header("Shurikens")]
    public GameObject Shuriken;
    public float ShurikenSpeed;
    public float ShurikenSpawnDistance;

    [Space]
    public GameObject IndicatorPrefab;
    private GameObject AttackIndicator;

   public void FixedUpdate()
    {
        CurrentTime += Time.deltaTime;

        if (CurrentTime > TeleportTime)
        {
            Teleport();
            CurrentTime = 0;
            HasThrown = false;
        }

        if (CurrentTime > ThrowTime && !HasThrown)
        {
            ThrowShuriken();
            HasThrown = true;
        }
        else if (CurrentTime < ThrowTime)
        {
            //create a beam going towards the target, and fade it in slowly
            if (AttackIndicator == null)
            {
                AttackIndicator = Instantiate(IndicatorPrefab, transform.position, Quaternion.identity);
            }
            Vector3 difference = Target.transform.position - transform.position;
            AttackIndicator.SetActive(true);

            //set roation of indicator
            Vector3 angles = AttackIndicator.transform.eulerAngles;
            angles.z = Vector3.SignedAngle(difference, Vector3.right, Vector3.back);
            AttackIndicator.transform.eulerAngles = angles;

            //set position of indicator
            AttackIndicator.transform.position = transform.position + (difference / 2);

            //set length of indicator
            Vector3 scale = AttackIndicator.transform.localScale;
            scale.x = difference.magnitude / 10;
            AttackIndicator.transform.localScale = scale;

            //set transparency of indicator
            float ChargeTimeLeft = 1 - ((ThrowTime - CurrentTime) / ThrowTime);
            Color colour = AttackIndicator.GetComponent<SpriteRenderer>().color;
            colour.a = ChargeTimeLeft;
            AttackIndicator.GetComponent<SpriteRenderer>().color = colour;
        }

        if (IsSpinning)
        {
            GetComponentInChildren<SpriteRenderer>().transform.Rotate(new Vector3(0, 0, SpinSpeed));
        }
    }

    public void Teleport()
    {
        float TargetX = Random.Range(TopLeftTeleportZone.x, BottomRightTeleportZone.x);
        float TargetY = Random.Range(TopLeftTeleportZone.y, BottomRightTeleportZone.y);

        transform.position = new Vector3(TargetX, TargetY);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void ThrowShuriken()
    {
        Vector3 direction = new Vector2(Target.transform.position.x - transform.position.x, Target.transform.position.y - transform.position.y);
        Instantiate(Shuriken, transform.position + direction.normalized * ShurikenSpawnDistance, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = direction.normalized * ShurikenSpeed;
        AttackIndicator.SetActive(false);
    }

    public void OnDestroy()
    {
        Destroy(AttackIndicator);
    }
}
