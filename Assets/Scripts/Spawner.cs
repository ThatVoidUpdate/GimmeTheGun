using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject EnemyTarget;

    private BoxCollider2D SpawnArea;
    private float MinX;
    private float MaxX;
    private float MinY;
    private float MaxY;

    public float SpawnDelay;

    public void Start()
    {
        SpawnArea = GetComponent<BoxCollider2D>();
        MinX = transform.position.x + SpawnArea.offset.x - SpawnArea.size.x / 2;
        MinY = transform.position.y + SpawnArea.offset.y - SpawnArea.size.y / 2;
        MaxX = transform.position.x + SpawnArea.offset.x + SpawnArea.size.x / 2;
        MaxY = transform.position.y + SpawnArea.offset.y + SpawnArea.size.y / 2;
    }

    public GameObject[] Spawn(GameObject enemy, int amount)
    {
        GameObject[] ret = new GameObject[amount];
        StartCoroutine(SpawnEnemiesWithDelay(enemy, amount, SpawnDelay, ret));
        return ret;
    }

    public IEnumerator SpawnEnemiesWithDelay(GameObject enemy, int amount, float SpawnDelay, GameObject[] returnArray)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject SpawnedEnemy = Instantiate(enemy, new Vector2(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY)), Quaternion.identity);
            SpawnedEnemy.GetComponent<Enemy>().Target = EnemyTarget;
            returnArray[i] = SpawnedEnemy;
            yield return new WaitForSeconds(SpawnDelay);
        }
    }
}
