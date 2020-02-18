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
    }
}
