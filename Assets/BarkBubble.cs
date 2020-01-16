using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarkBubble : MonoBehaviour
{
    public Camera camera;
    public GameObject TrackingObject;

    public Vector3 Offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = camera.WorldToScreenPoint(TrackingObject.transform.position) + Offset;
    }
}
