using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorScript : MonoBehaviour {

    public static SelectorScript instance = null;
    public Sprite spriteToUse = null;

    public GameObject Football;
    public GameObject Scientist;
    public GameObject Soldier;

    private Vector3 CharacterPosition;
    private Vector3 OffScreen;

    private int CharacterInt = 1;

    private SpriteRenderer ScientistRender, FootballRender, SoldierRender;

    //stating the sprites
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
        CharacterPosition = Soldier.transform.position;
        OffScreen = Scientist.transform.position;
        SoldierRender = Soldier.GetComponent<SpriteRenderer>();
        ScientistRender = Scientist.GetComponent<SpriteRenderer>();
        FootballRender = Football.GetComponent<SpriteRenderer>();

    }

    private void Start()
    {
        spriteToUse = SoldierRender.sprite;
    }

    //This is used to render in the next character and move the previous character off screen
    public void NextCharacter()
    {
        switch (CharacterInt)
        {
            case 1:
                SoldierRender.enabled = false;
                Soldier.transform.position = OffScreen;
                Scientist.transform.position = CharacterPosition;
                ScientistRender.enabled = true;
                spriteToUse = ScientistRender.sprite;
                CharacterInt++;
                break;
            case 2:
                ScientistRender.enabled = false;
                Scientist.transform.position = OffScreen;
                Football.transform.position = CharacterPosition;
                FootballRender.enabled = true;
                spriteToUse = FootballRender.sprite;

                CharacterInt++;
                break;
            case 3:
                FootballRender.enabled = false;
                Football.transform.position = OffScreen;
                Soldier.transform.position = CharacterPosition;
                SoldierRender.enabled = true;
                spriteToUse = SoldierRender.sprite;

                CharacterInt++;
                ResetInt();
                break;
            default:
                ResetInt();
                break;
        }
    }


    //made for the restart so once its cycled through all the characters it resets.
    private void ResetInt()
    {
        if (CharacterInt >= 3)
        {
            CharacterInt = 1;
        }
        else
        {
            CharacterInt = 3;
        }

    }
    public void PlayGame()  

    {
        SceneManager.LoadScene("Complete2Player");
    }
    
    
    
}
