using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenucharacterMusicScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private static MenucharacterMusicScript instance = null;
    public static MenucharacterMusicScript Instance
    {
        get { return instance; }
    }
    void Update()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
