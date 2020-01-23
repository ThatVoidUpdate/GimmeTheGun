using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public float Health;
    public float MaxHealth;
    public Sprite NeutralSprite;

    [HideInInspector]
    public SpriteRenderer rend;

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DoBoss()
    {
        return null;
    }

    public GameObject GetClosestPlayer()
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in GameObject.FindGameObjectsWithTag("Player"))
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }
}
