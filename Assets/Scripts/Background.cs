using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Backgrounds {Rocky, Lava}
public class Background : MonoBehaviour
{
    public Backgrounds background;
    // Start is called before the first frame update
    void Start()
    {
        switch (background)
        {
            case Backgrounds.Rocky:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Backgrounds/Rocky");
                break;
            case Backgrounds.Lava:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Backgrounds/Lava");
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
