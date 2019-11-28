using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public bool KeyboardPlayer;
    public int ControllerNumber;

    public float HorizontalSpeed;
    public float VerticalSpeed;

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
        }
    }
}
