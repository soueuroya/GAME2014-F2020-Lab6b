﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Joystick joystick;
    public Vector2 startPosition;
    public float baseSpeed = 1500;
    public float calculatedSpeed = 1500;
    public float jumpSpeed = 700;
    public float calculatedJumpSpeed = 700;
    public bool isLookingRight = true;
    public bool isGrounded = true;
    public bool isCrouching = false;
    public bool isGoingDown = false;
    public bool isJumping = false;
    public bool isAttacking = false;
    public bool isFalling;
    public int maxLifes = 3;
    public int lifes = 3;
    public List<GameObject> lifesIcon;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        joystick = GameObject.Find("Joystick").GetComponent<Joystick>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();
        UpdateAnimations();
    }

    public void Reset()
    {
        transform.position = startPosition;
        rigidbody.velocity = Vector3.zero;
    }

    public void ReadInput()
    {
        if (!isAttacking)
        {
            if (Input.GetAxisRaw("Horizontal") > 0 || joystick.Horizontal > 0.15f)
            {
                rigidbody.velocity = new Vector2(calculatedSpeed * Time.deltaTime * (joystick.Horizontal + Input.GetAxisRaw("Horizontal")), rigidbody.velocity.y);
                if (!isLookingRight)
                {
                    spriteRenderer.flipX = false;
                    isLookingRight = true;
                }
            }
            else if (Input.GetAxisRaw("Horizontal") < 0 || joystick.Horizontal < -0.15f)
            {
                rigidbody.velocity = new Vector2(calculatedSpeed * Time.deltaTime * (joystick.Horizontal + Input.GetAxisRaw("Horizontal")), rigidbody.velocity.y);
                if (isLookingRight)
                {
                    isLookingRight = false;
                    spriteRenderer.flipX = true;
                }
            }
            else
            {
                rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            }
        }

        if (isJumping)
        {
            rigidbody.AddForce(Vector2.up * calculatedJumpSpeed / 3, ForceMode2D.Force);
            if (!Input.GetKey(KeyCode.Space) && joystick.Vertical < 0.15f)
            {
                isJumping = false;
            }
        }

        if (rigidbody.velocity.y < 0)
        {
            isJumping = false;
            if (!Input.GetKey(KeyCode.Space) && joystick.Vertical < 0.15f)
            {
                rigidbody.AddForce(Vector2.down * calculatedJumpSpeed, ForceMode2D.Force);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || joystick.Vertical > 0.15f)
        {
            if (isGrounded && !isJumping && !isAttacking)
            {
                isJumping = true;
                isGrounded = false;
                if (rigidbody.velocity.y < 0.1f && !isFalling)
                {
                    rigidbody.AddForce(Vector2.up * calculatedJumpSpeed, ForceMode2D.Impulse);
                    animator.SetTrigger("Jump");
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0) && false)
        {
            if (isGrounded && !isJumping && !isAttacking)
            {
                Attack();
            }
        }
        else if (isGrounded && rigidbody.velocity.x < 0.1f && rigidbody.velocity.x > -0.1f && (Input.GetKey(KeyCode.S) || joystick.Vertical < -0.15f))
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }
        
    }

    public void Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
    }

    public void FinishAttack()
    {
        isAttacking = false;
    }

    public void FinishFall()
    {
        isFalling = false;
    }


    public void GoingDown()
    {
        isGoingDown = true;
    }

    public void UpdateAnimations()
    {
        animator.SetFloat("SpeedY", rigidbody.velocity.y);
        animator.SetFloat("SpeedX", Mathf.Abs(rigidbody.velocity.x));
        animator.SetBool("Grounded", isGrounded);
        animator.SetBool("Crouching", isCrouching);
    }

    public void Grounded()
    {
        if (!isGrounded)
        {
            isGrounded = true;
            if (isGoingDown)
            {
                animator.SetTrigger("Fall");
                isFalling = true;
                isGoingDown = false;
            }
        }
    }

    public void UnGrounded()
    {
        if (isGrounded)
        {
            isGrounded = false;
            //animator.SetTrigger("Fall");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Grounded();
        }
        else
        if (collision.CompareTag("DeathPlane"))
        {
            Reset();
        }
        else
        if (collision.CompareTag("Enemy"))
        {
            LoseLife();
        }
    }

    public void LoseLife()
    { 
        if (lifes > 0)
        {
            lifes--;
            UpdateLifes();
        }
        else
        {
            GameOver();
        }
    }

    public void GameOver()
    { }

    public void UpdateLifes()
    {
        for (int i = 0; i < maxLifes; i++)
        {
            lifesIcon[i].SetActive(false);
        }

        for (int i = 0; i < lifes; i++)
        {
            lifesIcon[i].SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Grounded();
            isFalling = false;
            isJumping = false;
        }
        else
        if (collision.CompareTag("DeathPlane"))
        {
            Reset();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            
        }
        else
        if (collision.CompareTag("DeathPlane"))
        {
            
        }
    }
}
