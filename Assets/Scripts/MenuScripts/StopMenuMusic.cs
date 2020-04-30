using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMenuMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MenucharacterMusicScript.Instance.gameObject.GetComponent<AudioSource>().Pause();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
