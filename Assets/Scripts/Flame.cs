using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : Bullet
{
    private void OnDestroy()
    {
        GetComponentInChildren<TrailRenderer>().autodestruct = true;
        GetComponentInChildren<TrailRenderer>().transform.parent = null;
    }
}
