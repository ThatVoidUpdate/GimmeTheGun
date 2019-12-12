using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject[] Enemies;
    public int[] Amounts;
    public Wave(GameObject[] _Enemies, int[] _Amounts)
    {
        Enemies = _Enemies;
        Amounts = _Amounts;
    }
}
