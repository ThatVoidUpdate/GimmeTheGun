using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Control Options")]
    public bool KeyboardPlayer;
    public int ControllerNumber;

    [Header("Speeds")]
    public float HorizontalSpeed;
    public float VerticalSpeed;

    [Header("Held object options")]
    public GameObject HeldObject;
    public Vector2 HeldOffset;
    private List<Collider2D> colliders = new List<Collider2D>();
    private bool Holding;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (KeyboardPlayer)
        {
            rb.velocity = new Vector2(Input.GetAxis("KeyboardHorizontal") * HorizontalSpeed, Input.GetAxis("KeyboardVertical") * VerticalSpeed);
            if (colliders.Count > 0)
            {
                if (Input.GetButton("KeyboardGrab"))
                {
                    colliders[0].transform.position = gameObject.transform.position + new Vector3(HeldOffset.x, HeldOffset.y);
                    colliders[0].transform.rotation = transform.rotation;
                    if (colliders[0].GetComponent<Gun>()?.held == false)
                    {
                        colliders[0].GetComponent<Rigidbody2D>().isKinematic = true;
                        colliders[0].GetComponent<Rigidbody2D>().freezeRotation = true;
                        colliders[0].GetComponent<Gun>().held = true;
                    }
                }
                else
                {
                    foreach (Collider2D item in colliders)
                    {
                        if (colliders[0].GetComponent<Gun>()?.held == true)
                        {
                            colliders[0].GetComponent<Rigidbody2D>().isKinematic = false;
                            colliders[0].GetComponent<Rigidbody2D>().freezeRotation = false;
                            colliders[0].GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
                            colliders[0].GetComponent<Gun>().held = false;
                        }
                    }
                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!colliders.Contains(other)) { colliders.Add(other); }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        colliders.Remove(other);
    }
}
