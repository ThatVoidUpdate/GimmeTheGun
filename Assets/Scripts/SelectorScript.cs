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

    private bool LeftUpHeld = false;
    private bool LeftDownHeld = false;

    private bool RightUpHeld = false;
    private bool RightDownHeld = false;

    //stating the sprites
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } 
        else 
        {
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
        if(Input.GetAxis("Controller0MoveVertical") == 1 && !LeftUpHeld)
        {
            //next character left
            leftSelectionIndex = (leftSelectionIndex + 1) % AllPlayerGraphics.Length;
            leftGraphics = AllPlayerGraphics[leftSelectionIndex];
            LeftPlayer.sprite = leftGraphics.FrontSprite;
            LeftUpHeld = true;
        }
        else if (Input.GetAxis("Controller0MoveVertical") == -1 && !LeftDownHeld)
        {
            //previous character left
            leftSelectionIndex = leftSelectionIndex == 0 ? AllPlayerGraphics.Length - 1 : (leftSelectionIndex - 1);
            leftGraphics = AllPlayerGraphics[leftSelectionIndex];
            LeftPlayer.sprite = leftGraphics.FrontSprite;
            LeftDownHeld = true;
        }
        if (Input.GetAxis("Controller1MoveVertical") == 1 && !RightUpHeld)
        {
            //next character right
            rightSelectionIndex = (rightSelectionIndex + 1) % AllPlayerGraphics.Length;
            rightGraphics = AllPlayerGraphics[rightSelectionIndex];
            RightPlayer.sprite = rightGraphics.FrontSprite;
            RightUpHeld = true;
        }
        else if (Input.GetAxis("Controller1MoveVertical") == -1 && !RightDownHeld)
        {
            //previous character right
            rightSelectionIndex = rightSelectionIndex == 0 ? AllPlayerGraphics.Length - 1 : (rightSelectionIndex - 1);
            rightGraphics = AllPlayerGraphics[rightSelectionIndex];
            RightPlayer.sprite = rightGraphics.FrontSprite;
            RightDownHeld = true;
        }

        if (Input.GetAxis("Controller0MoveVertical") == 0)
        {
            LeftDownHeld = false;
            LeftUpHeld = false;
        }
        if (Input.GetAxis("Controller1MoveVertical") == 0)
        {
            RightDownHeld = false;
            RightUpHeld = false;
        }
    }

    public void PlayGame()  
    {
        SceneManager.LoadScene("Comic");
    }
    
    
    
}
