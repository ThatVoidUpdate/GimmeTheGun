using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MasterMixer : MonoBehaviour
{
    public AudioMixer audiomixer;

    public AudioMixer musicmixer;
    // Start is called before the first frame update
   

        public void SetVolume (float volume)
        {
            audiomixer.SetFloat ("auidomixer", volume);
            //volume = pubaud;
            PlayerPrefs.SetFloat ("GlobalVol", volume);
            PlayerPrefs.Save();
        }

        public void SetVolumeMusic (float volume)
        {
            musicmixer.SetFloat ("musicmixer", volume);
        }

    // Update is called once per frame
}
