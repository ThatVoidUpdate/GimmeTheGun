using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LSD : MonoBehaviour
{
    public float ColourSpeed;
    private PostProcessVolume volume;
    private ColorGrading colorGradingLayer;
    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out colorGradingLayer);
    }

    // Update is called once per frame
    void Update()
    {
        colorGradingLayer.hueShift.value = Mathf.Sin(Time.time * ColourSpeed) * 180;
    }
}
