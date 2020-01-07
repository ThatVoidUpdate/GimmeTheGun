using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerGraphics {Blue, Green, Orange, Pink, Purple, Yellow}

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Control Options")]
    public Controller controller; //The controller that his player is listening to

    [Header("Speeds")]
    public float HorizontalSpeed;
    public float VerticalSpeed; //The ohirzontal and vertical speed of the player

    [Header("Held object options")]
    public GameObject HeldObject; //The object that the player is currently holding
    //public Vector2 HeldOffset;
    public float HeldDistance; //The distance to hold the object at
    private List<Collider2D> closeGuns = new List<Collider2D>(); //A list of all the guns in the trigger
    private bool Holding; //Whether the player is holding on to something

    [Header("Controls")]
    public Control MoveHorizontal;
    public Control MoveVertical;
    public Control LookHorizontal;
    public Control LookVertical;
    public Control Grab;
    public Control Shoot; //The controls for each of the respective actions

    [Header("Graphics")]
    public PlayerGraphics Graphics; //The sprite to render the player with

    private Rigidbody2D rb;
    private float theta = 90; //The current angle of the rotation stick

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //Select the correct image from the resources folder, depeding on the player graphic selected
        switch (Graphics)
        {//Select the correct iamge freom the resources folder, and load it
            case PlayerGraphics.Blue:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Squares/Blue");
                break;
            case PlayerGraphics.Green:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Squares/Green");
                break;
            case PlayerGraphics.Orange:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Squares/Orange");
                break;
            case PlayerGraphics.Pink:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Squares/Pink");
                break;
            case PlayerGraphics.Purple:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Squares/Purple");
                break;
            case PlayerGraphics.Yellow:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Squares/Yellow");
                break;
            default:
                break;
        }
    }


    void Update()
    {       

        rb.velocity = new Vector2(controller.ControlState[MoveHorizontal] * HorizontalSpeed, controller.ControlState[MoveVertical] * -VerticalSpeed); //Set the movement speed according to the controller input
        if (closeGuns.Count > 0)
        {
            if (controller.ControlState[Grab] == 1)
            {
                //colliders[0].transform.position = transform.TransformPoint(new Vector3(HeldOffset.x, HeldOffset.y));

                //Set the position and rotation of the gun, using the angle of the stick assigned to the gun movement
                closeGuns[0].transform.position = new Vector2(transform.position.x + HeldDistance * Mathf.Cos(theta * 2 * Mathf.PI / 360), transform.position.y + HeldDistance * Mathf.Sin(theta * 2 * Mathf.PI / 360));
                closeGuns[0].transform.eulerAngles = new Vector3(0, 0, theta - 90);


                if (closeGuns[0].GetComponent<Gun>()?.held == false)
                {
                    closeGuns[0].GetComponent<Rigidbody2D>().freezeRotation = true;
                    closeGuns[0].GetComponent<Gun>().held = true;
                }

                closeGuns[0].GetComponent<Gun>().Shooting = controller.ControlState[Shoot] == 1;
            }
            else
            {
                foreach (Collider2D item in closeGuns)
                {
                    if (closeGuns[0].GetComponent<Gun>()?.held == true)
                    {
                        closeGuns[0].GetComponent<Rigidbody2D>().freezeRotation = false;
                        closeGuns[0].GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
                        closeGuns[0].GetComponent<Gun>().held = false;
                        closeGuns[0].GetComponent<Gun>().Shooting = false;
                    }
                }
            }
        }

        //Maths to get the angle of the stick assigned to rotation
        if (controller.ControlState[LookVertical] != 0 || controller.ControlState[LookHorizontal] != 0)
        {
            if (Mathf.Atan(controller.ControlState[LookVertical] / controller.ControlState[LookHorizontal]) / Mathf.PI == 0.5)
            {
                theta = - Mathf.PI / 2;
            }
            else if (Mathf.Atan(controller.ControlState[LookVertical] / controller.ControlState[LookHorizontal]) / Mathf.PI == -0.5)
            {
                theta = Mathf.PI / 2;
            }
            else
            {
                theta = Mathf.Atan(controller.ControlState[LookVertical] / controller.ControlState[LookHorizontal]);
            }
            

            if (controller.ControlState[LookHorizontal] > 0)
            {
                 theta += Mathf.PI;
            }

            theta += Mathf.PI;
            theta = theta * -360 / (2 * Mathf.PI);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Whenever an object enters the trigger around the player, and it has the tag Gun and isnt in the colliders list, add it to the colliders list
        if (!closeGuns.Contains(other) && other.gameObject.CompareTag("Gun")) 
        { 
            closeGuns.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //If a gun move out of the trigger, stop it from shooting and being held, and remove it from the colliders list
        if (other.gameObject.CompareTag("Gun"))
        {
            closeGuns.Find(x => x == other).GetComponent<Gun>().held = false;
            closeGuns.Find(x => x == other).GetComponent<Gun>().Shooting = false;

            closeGuns.Remove(other);
        }        
    }
}
