using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Backgrounds {Rocky, Lava, Arctic, Forest}
public class Background : MonoBehaviour
{
    public AudioClip[] RockyMusic;
    public AudioClip[] LavaMusic;
    public AudioClip[] ArcticMusic;
    public AudioClip[] ForestMusic;
    public AudioSource source;
    public Backgrounds background;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        switch (background)
        {
            case Backgrounds.Rocky:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Backgrounds/Rocky");
                StartCoroutine(PlayMusic(RockyMusic));
                break;
            case Backgrounds.Lava:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Backgrounds/Lava");
                StartCoroutine(PlayMusic(LavaMusic));
                break;
            case Backgrounds.Arctic:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Backgrounds/Arctic");
                StartCoroutine(PlayMusic(ArcticMusic));
                break;
            case Backgrounds.Forest:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Backgrounds/Forest");
                StartCoroutine(PlayMusic(ForestMusic));
                break;
            default:
                break;
        }
 
    }
    public IEnumerator PlayMusic(AudioClip[] clips)
    {
        while (true)
        {
            foreach (AudioClip clip in clips)
            {
                source.clip = clip;
                source.Play();
                yield return new WaitForSeconds(source.clip.length);
                
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
