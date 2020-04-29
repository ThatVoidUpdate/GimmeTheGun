using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectorScript : MonoBehaviour {

    public static SelectorScript instance = null;

    public Image LeftPlayer;
    public Image RightPlayer;

    [HideInInspector]
    public PlayerGraphics leftGraphics;
    [HideInInspector]
    public PlayerGraphics rightGraphics;

    public PlayerGraphics[] AllPlayerGraphics;

    private int leftSelectionIndex = 0;
    private int rightSelectionIndex = 0;

    //stating the sprites
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }

    }

    private void Start()
    {
        leftGraphics = AllPlayerGraphics[0];
        rightGraphics = AllPlayerGraphics[0];
        LeftPlayer.sprite = leftGraphics.FrontSprite;
        RightPlayer.sprite = rightGraphics.FrontSprite;
    }

    private void Update()
    {
        if(Input.GetAxis("Controller0MoveVertical") == 1)
        {
            //next character left
            leftSelectionIndex = (leftSelectionIndex + 1) % AllPlayerGraphics.Length;
            leftGraphics = AllPlayerGraphics[leftSelectionIndex];
            LeftPlayer.sprite = leftGraphics.FrontSprite;
        }
        else if (Input.GetAxis("Controller0MoveVertical") == -1)
        {
            //previous character left
            leftSelectionIndex = (leftSelectionIndex - 1) % AllPlayerGraphics.Length;
            leftGraphics = AllPlayerGraphics[leftSelectionIndex];
            LeftPlayer.sprite = leftGraphics.FrontSprite;
        }
        if (Input.GetAxis("Controller1MoveVertical") == 1)
        {
            //next character right
            rightSelectionIndex = (rightSelectionIndex + 1) % AllPlayerGraphics.Length;
            rightGraphics = AllPlayerGraphics[rightSelectionIndex];
            RightPlayer.sprite = rightGraphics.FrontSprite;
        }
        else if (Input.GetAxis("Controller1MoveVertical") == -1)
        {
            //previous character right
            rightSelectionIndex = (rightSelectionIndex - 1) % AllPlayerGraphics.Length;
            rightGraphics = AllPlayerGraphics[rightSelectionIndex];
            RightPlayer.sprite = rightGraphics.FrontSprite;
        }
    }

    public void PlayGame()  

    {
        SceneManager.LoadScene("Comic");
    }
    
    
    
}
