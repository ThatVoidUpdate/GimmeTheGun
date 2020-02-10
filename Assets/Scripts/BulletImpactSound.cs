using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BulletImpactSound : MonoBehaviour
{
    public AudioClip FlameThrowerSound;
    public AudioClip PistolSound;
    public AudioClip RocketLauncherSound;
    public AudioClip ShotgunSound;


    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnParticleCollision(GameObject ParticleSystem)
    {
        
        switch (ParticleSystem.GetComponentInParent<Gun>().type)
        {
            case GunType.Flamethrower:
                source.clip = FlameThrowerSound;
                break;
            case GunType.Pistol:
                source.clip = PistolSound;
                break;
            case GunType.RocketLauncher:
                source.clip = RocketLauncherSound;
                break;
            case GunType.Shotgun:
                source.clip = ShotgunSound;
                break;
            default:
                source.clip = null;
                break;
        }

        source.Play();
    }
}
