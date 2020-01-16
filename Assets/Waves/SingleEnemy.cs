using System.Collections.Generic;
using UnityEngine;
public class SingleEnemy : MonoBehaviour
{
public List<(GameObject, int)> Enemies;
public void Start()
{ Enemies = new List<(GameObject, int)> { (Resources.Load<GameObject>("Enemies/Enemy"), 1) };
GetComponent<Spawner>().Wave = Enemies;
}
}