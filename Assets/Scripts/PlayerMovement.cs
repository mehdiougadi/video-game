using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

public class PlayerMovement : MonoBehaviour
{

    //UNITY COMPONENTS
    private Rigidbody2D character;
    private Animator animator; 

    //MOVING HORIZONTALLY VARIABLES
    private float horizontal;
    public float moveSpeed = 20;
    private bool isRight = true;
    private Vector3 objectScale;
    private Vector3 pastPos;

    //JUMPING VARIABLES
    public float jump;
    private bool isJumping = false;

    private void Start()
    {

        //CAPTURE GAMEOBJECT AT THE START
        animator = GetComponent<Animator>();
        character = GetComponent<Rigidbody2D>();
        pastPos = character.position;
    }

    private void Update()

    {

        //UPDATED GLOBAL VARIABLES
        horizontal = Input.GetAxis("Horizontal");
        objectScale = gameObject.transform.localScale;

        //FUNCTIONS USED
        Moving(horizontal);
        Jumping();

        //MOVEMENT HORIZONTALLY
        character.velocity = new Vector2(horizontal * moveSpeed, character.velocity.y);

        //UPDATED GLOBAL VARIABLES
        pastPos = character.position;

        //Verify if character fell
        if (fell())
        {
            Vector2 initialPos = new Vector2(0, 10);
            character.transform.position = initialPos;
            Camera.main.transform.position = initialPos;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetBool("IsGrounded", true);
        }
    }    
    
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
            animator.SetBool("IsGrounded", false);
        }
    }    


    //FUNCTION FOR JUMPING
    void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isJumping != true)
        {
            isJumping = true;
            character.AddForce(new Vector2(0, jump));
            animator.SetBool("IsGrounded", false);
        }
    }

    //FUNCTION FOR MOVING
    void Moving(float x)
    {
        if (x == 0) 
        { 
            animator.SetBool("IsMoving", false);
        }
        else
        {
            animator.SetBool("IsMoving", true);
            //Condition used to flip the character in movement
            if ((x < 0 && isRight == true) || (x > 0 && isRight == false))
            {
                isRight = !isRight;
                objectScale.x = -(objectScale.x);
                gameObject.transform.localScale = objectScale;
            }
        }
    }

    bool fell()
    {
        return (Convert.ToInt32(character.transform.position.y) <= -1);
    }
}

