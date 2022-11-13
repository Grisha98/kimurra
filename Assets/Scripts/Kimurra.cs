using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kimurra : MonoBehaviour
{
    public float walkSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    public GameObject Spot;

    Vector2 movement;

    void Update()
    {
        //Start - walk
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        //End - walk
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * walkSpeed * Time.fixedDeltaTime);
    }

    public void GenerateSpot1()
    { 
        Instantiate(Spot, transform.position-new Vector3(0.3f,1f,0), Spot.transform.rotation);
    }

    public void GenerateSpot2()
    {
        Instantiate(Spot, transform.position - new Vector3(-0.3f, 1f, 0), Spot.transform.rotation);
    }
}
