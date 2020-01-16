using System.Collections.Generic;
using UnityEngine;
public class Wave 1 : MonoBehaviour
{
public List<(GameObject, int)> Enemies;
public void Start()
{ Enemies = new List<(GameObject, int)> { (Resources.Load<GameObject>("Enemies/Enemy"), 5) };
GetComponent<Spawner>().Wave = Enemies;
}
}