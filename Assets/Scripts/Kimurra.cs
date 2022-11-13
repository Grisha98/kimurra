using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kimurra : MonoBehaviour
{
    public float walkSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    void Update()
    {
        //Start - walk
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //animator.SetFloat("Horizontal", movement.x);
        //animator.SetFloat("Vertical", movement.y);
        //animator.SetFloat("Speed", movement.sqrMagnitude);
        //End - walk
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * walkSpeed * Time.fixedDeltaTime);
    }

}
