using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public enum Direction { Up, Down, Right, Left, None}
public enum PlayerID { Left, Right }


[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    #region Variables
    public PlayerID ID;

    [Header("Speeds")]
    public float HorizontalSpeed;
    public float VerticalSpeed; //The horizontal and vertical speed of the player
    
    //Control Values. Replacement for controller gameobjects
    [Header("Controls")]
    public string HorizontalLookControl; 
    public string VerticalLookControl;
    public string HorizontalMoveControl;
    public string VerticalMoveControl;
    public string GrabControl;
    public string ShootControl;
    public string SummonControl;
    public string PushControl;
    public string MenuControl;
    
    private float HorizontalLookControlValue;
    private float VerticalLookControlValue;
    private float VerticalMoveControlValue;
    private float HorizontalMoveControlValue;
    private float GrabControlValue;
    private float ShootControlValue;
    private bool SummonControlValue;
    private bool PushControlValue;
    private bool MenuControlValue;


    [Header("Held object options")]
    public Gun HeldObject = null; //The object that the player is currently holding
    public float ThrowSpeed;

    public List<Collider2D> closeGuns = new List<Collider2D>(); //A list of all the guns in the trigger
    private bool CanDrop = true;

    private Rigidbody2D Mass;

    [Header("Graphics")]
    public PlayerGraphics graphics;
    private Sprite UpSprite;
    private Sprite DownSprite;
    private Sprite LeftSprite;
    private Sprite RightSprite; //The sprites for each of their respective directions
    private SpriteRenderer rend; //The sprite renderer attached to this gameobject
    private Direction CurrentDirection = Direction.Down; //The direction that the player is currently facing

    [Header("Health")]
    public float MaxHealth = 100; //The maximum health of the player
    public float Health; //The current health of the player
    public bool dead = false; //Whether the player is dead or not
    private float DamageTime; //How long since the enemy started doing damage
    public float DamageSpeed; //How long it takes an enemy to do damage

    private bool InvertedControls = false; // Support for drunk powerup
    private bool CanMove = true; // Support for turret powerup

    private Rigidbody2D rb; //The rigidbody2d attached to this gameobject
    private float theta = 90; //The current angle of the rotation stick

    public float RespawnTime;
    private float DeathTime;
    public int DeathScore = -10;
    private ScorePlayers scorer;

    [Header("Events")]
    public UnityEvent OnFail;
    public SendBarkEvent BarkLineEvent;

    #endregion Variables


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();

        Health = MaxHealth;

        scorer = FindObjectOfType<ScorePlayers>();

        if(ID == PlayerID.Left)
        {
            graphics = SelectorScript.instance.leftGraphics;
        }
        else if(ID == PlayerID.Right)
        {
            graphics = SelectorScript.instance.rightGraphics;
        }

        rend.sprite = graphics.FrontSprite;
    }


    void Update()
    {
        UpdateControlState();
        rb.velocity = new Vector2(HorizontalMoveControlValue * (CanMove ? (InvertedControls ? -HorizontalSpeed : HorizontalSpeed) : 0), VerticalMoveControlValue * (CanMove ? (InvertedControls ? VerticalSpeed : -VerticalSpeed) : 0)); //Set the movement speed according to the controller input        

        if ((GrabControlValue == 1) && closeGuns.Count > 0 && HeldObject == null && !dead)
        {//There is a gun close to us, and we are attempting to grab it, and we arent currently holding a gun, and we arent currently dead
            HeldObject = closeGuns[0].GetComponent<Gun>();
            CanDrop = false;
            if (transform.position.x < 0)
            {
                closeGuns[0].GetComponent<Gun>().OnLeftSideOfScreen = true;
            }
            else
            {
                closeGuns[0].GetComponent<Gun>().OnLeftSideOfScreen = false;
            }

            //FindObjectOfType<BarkEvents>().TriggerBarkLine(BarkEventTypes.GunPickup, gameObject);
            BarkLineEvent.Invoke(BarkEventTypes.GunPickup, this.gameObject);
        }

        if (GrabControlValue == 0)
        {//Let the player drop the gun after they release the grab button
            CanDrop = true;
        }

        if (ShootControlValue == 1 && HeldObject != null)
        {//We are trying to shoot, and we are holding a gun
            HeldObject.GetComponent<Gun>().Shooting = true;
        }
        else if (ShootControlValue == 0 && HeldObject != null)
        {//We are trying to not shoot, and we are holding a gun
            HeldObject.GetComponent<Gun>().Shooting = false;
        }

        if (MenuControlValue)
        {
            SceneManager.LoadScene("Menu");
        }

        if (PushControlValue)
        {
            Mass = GetComponent<Rigidbody2D>();
            Mass.mass = 5000;
        }
        else if (PushControlValue)
        {
            Mass = GetComponent<Rigidbody2D>();
            Mass.mass = 4;
        }


        if (GrabControlValue == 1 && HeldObject != null && CanDrop)
        {//We are holding a gun, and trying to drop it
            //FindObjectOfType<BarkEvents>().TriggerBarkLine(BarkEventTypes.ThrowGun, gameObject);
            BarkLineEvent.Invoke(BarkEventTypes.ThrowGun, this.gameObject);

            HeldObject.GetComponent<Rigidbody2D>().velocity = new Vector2(ThrowSpeed * Mathf.Cos(theta * 2 * Mathf.PI / 360), ThrowSpeed * Mathf.Sin(theta * 2 * Mathf.PI / 360));
            HeldObject = null;
        }

        if (HeldObject != null)
        {//We are holding a gun, so move it around the player
            HeldObject.SetAngle(theta, transform.position);
        }

        //Maths to get the angle of the stick assigned to rotation
        if (VerticalLookControlValue != 0 || HorizontalLookControlValue != 0)
        {
            if (Mathf.Atan(VerticalLookControlValue / HorizontalLookControlValue) / Mathf.PI == 0.5)
            {
                theta = - Mathf.PI / 2;
            }
            else if (Mathf.Atan(VerticalLookControlValue / HorizontalLookControlValue) / Mathf.PI == -0.5)
            {
                theta = Mathf.PI / 2;
            }
            else
            {
                theta = Mathf.Atan(VerticalLookControlValue / HorizontalLookControlValue);
            }            

            if (HorizontalLookControlValue > 0)
            {
                 theta += Mathf.PI;
            }

            theta += Mathf.PI;
            theta = theta * -360 / (2 * Mathf.PI);            
        }

        //Set the current sprite to the correct one for the looking angle
        if (theta < -315 && theta > -405 && CurrentDirection != Direction.Left)
        {
            CurrentDirection = Direction.Left;
            rend.sprite = graphics.RightSprite;
        }
        else if (theta < -225 && theta > -315 && CurrentDirection != Direction.Up)
        {
            CurrentDirection = Direction.Up;
            rend.sprite = graphics.BackSprite;
        }
        else if (theta < -135 && theta > -225 && CurrentDirection != Direction.Right)
        {
            CurrentDirection = Direction.Right;
            rend.sprite = graphics.LeftSprite;
        }
        else if ((theta > -135 || theta < -405) && CurrentDirection != Direction.Down)
        {
            CurrentDirection = Direction.Down;
            rend.sprite = graphics.FrontSprite;
        }

        //Pull gun twards player
        if (SummonControlValue)
        {
            Gun gun = FindObjectOfType<Gun>();
            if (gun.transform.position.x < 0 && transform.position.x < 0) 
            {
                Vector3 direction = (transform.position - gun.transform.position).normalized*2;
                gun.GetComponent<Rigidbody2D>().velocity = direction;

            }
            if (gun.transform.position.x > 0 && transform.position.x > 0)
            {
                Vector3 direction = (transform.position - gun.transform.position).normalized * 2;
                gun.GetComponent<Rigidbody2D>().velocity = direction;

            }
        }

        if (dead)
        {
            DeathTime += Time.deltaTime;            
        }

        if (dead&& DeathTime>RespawnTime)
        {
            Respawn();
            DeathTime = 0;
        }
    }


    public void TakeDamage(float DamageAmount)
    {
        //If we are currently above half health, but taking damage would take us below half health
        if (Health > MaxHealth / 2 && Health - DamageAmount <= MaxHealth / 2)
        {//Say a half health line
            //FindObjectOfType<BarkEvents>().TriggerBarkLine(BarkEventTypes.HalfHealth, gameObject);
            BarkLineEvent.Invoke(BarkEventTypes.HalfHealth, this.gameObject);
        }

        //If we are currently above 10% health, but taking damage would take us below 10% health
        if (Health > MaxHealth / 10 && Health - DamageAmount <= MaxHealth / 10)
        {//Say a near death line   
            //FindObjectOfType<BarkEvents>().TriggerBarkLine(BarkEventTypes.NearDeath, gameObject);
            BarkLineEvent.Invoke(BarkEventTypes.NearDeath, this.gameObject);
        }

        Health -= DamageAmount;
        if (Health <= 0)
        {
            Die();
            scorer.PlayerKills(this);
        }
    }

    private void Die()
    {
        FindObjectOfType<BarkEvents>().TriggerBarkLine(BarkEventTypes.Death, gameObject);
        CanMove = false;
        rend.color = new Color(1, 0.5f, 0.5f, 0.5f);
        dead = true;
        if (HeldObject != null)
        {
            HeldObject.Shooting = false;
        }
        HeldObject = null;
        foreach (Player player in FindObjectsOfType<Player>())
        {
            if (player.dead == false)
            {
                return;
            }
        }
        //both players are dead, hopefully
        OnFail.Invoke();

    }

    public void Respawn()
    {
        //FindObjectOfType<BarkEvents>().TriggerBarkLine(BarkEventTypes.Respawn, gameObject);
        BarkLineEvent.Invoke(BarkEventTypes.Respawn, this.gameObject);

        rend.color = new Color(1, 1, 1, 1);
        dead = false;
        CanMove = true;
        Health = MaxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DamageTime = 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!dead)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                DamageTime += Time.deltaTime;
            }

            if (DamageTime > DamageSpeed)
            {
                DamageTime = 0;
                TakeDamage(collision.gameObject.GetComponent<Enemy>().Damage);
            }
        }
    }

    public void EndWave()
    {
        //FindObjectOfType<BarkEvents>().TriggerBarkLine(BarkEventTypes.WaveCleared, gameObject);
        BarkLineEvent.Invoke(BarkEventTypes.WaveCleared, this.gameObject);
    }

    public void StartWave()
    {
        //FindObjectOfType<BarkEvents>().TriggerBarkLine(BarkEventTypes.NewRound, gameObject);
        BarkLineEvent.Invoke(BarkEventTypes.NewRound, this.gameObject);

    }

    public void KillEnemy()
    {
        //FindObjectOfType<BarkEvents>().TriggerBarkLine(BarkEventTypes.KillEnemy, gameObject);
        BarkLineEvent.Invoke(BarkEventTypes.KillEnemy, this.gameObject);
    }


    public void SetControlInversion(bool Inverted)
    {
        InvertedControls = Inverted;
    }

    public void SetCanMove(bool _CanMove)
    {
        CanMove = _CanMove;
    }

    public void UpdateControlState()
    {
        HorizontalLookControlValue = Input.GetAxis(HorizontalLookControl);
        VerticalLookControlValue = Input.GetAxis(VerticalLookControl);
        VerticalMoveControlValue = Input.GetAxis(HorizontalMoveControl);
        HorizontalMoveControlValue = Input.GetAxis(VerticalMoveControl);
        GrabControlValue = Input.GetAxis(GrabControl);
        ShootControlValue = Input.GetAxis(ShootControl);
        SummonControlValue = Input.GetAxis(SummonControl) == 1;
        PushControlValue = Input.GetAxis(PushControl) == 1;
        MenuControlValue = Input.GetAxis(MenuControl) == 1;
    }
}

//#7582 ♥
