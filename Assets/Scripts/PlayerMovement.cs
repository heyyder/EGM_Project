using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float pspeed;
    public LayerMask groundLayer;

    private Rigidbody2D pbody;
    private Animator animator;
    private BoxCollider2D pBoxColl;
    
    

    private void Awake()
    {
        //Get Rigidbody2D component and Animator component
        pbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        pBoxColl = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //Move player based on horizontal input
        float horInput = Input.GetAxis("Horizontal"); 
        pbody.velocity = new Vector2(horInput * pspeed, pbody.velocity.y);

        //Flip player sprite based on direction
        if (horInput > 0.01f)
        {
            transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        }
        else if (horInput < -0.01f)
        {
            transform.localScale = new Vector3(-0.75f, 0.75f, 0.75f);
        }

        //Jump
        if (Input.GetKey(KeyCode.Space) && !isJumping())
        {
           Jump();
        }

        animator.SetBool("isMoving", horInput != 0);
        animator.SetBool("isJumping", isJumping());

    }

    private void Jump()
    {
        pbody.velocity = new Vector2(pbody.velocity.x, pspeed);
        animator.SetTrigger("jump");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      
    }

    //Check if player is jumping
    private bool isJumping()
    {
        RaycastHit2D raycast = Physics2D.BoxCast(   pBoxColl.bounds.center,
                                                    pBoxColl.bounds.size, 
                                                    0, 
                                                    Vector2.down, 
                                                    0.1f, 
                                                    groundLayer);
        return raycast.collider == null;
    }     
}
