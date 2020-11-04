using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Joystick joystick;
    public float baseSpeed = 1500;
    public float calculatedSpeed = 1500;
    public float jumpSpeed = 700;
    public float calculatedJumpSpeed = 700;
    public bool isLookingRight = true;
    public bool isGrounded = true;
    public bool isCrouching = false;
    public bool isGoingDown = false;
    public bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        joystick = GameObject.Find("Joystick").GetComponent<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();
        UpdateAnimations();
    }

    public void ReadInput()
    {
        if (Input.GetAxisRaw("Horizontal") > 0 || joystick.Horizontal > 0.15f)
        {
            rigidbody.velocity = new Vector2(calculatedSpeed * Time.deltaTime, rigidbody.velocity.y);
            if (!isLookingRight)
            {
                spriteRenderer.flipX = false;
                isLookingRight = true;
            }
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 || joystick.Horizontal < -0.15f)
        {
            rigidbody.velocity = new Vector2(-calculatedSpeed * Time.deltaTime, rigidbody.velocity.y);
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

        if (isJumping)
        {
            rigidbody.AddForce(Vector2.up * calculatedJumpSpeed / 7, ForceMode2D.Force);
            if (!Input.GetKey(KeyCode.Space))
            {
                isJumping = false;
            }
        }

        if (rigidbody.velocity.y < 0)
        {
            isJumping = false;
            if (!Input.GetKey(KeyCode.Space) && joystick.Vertical < 0.15f)
            {
                rigidbody.AddForce(Vector2.down * calculatedJumpSpeed / 5, ForceMode2D.Force);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || joystick.Vertical > 0.15f)
        {
            if (isGrounded && !isJumping)
            {
                isJumping = true;
                isGrounded = false;
                if (rigidbody.velocity.y < 0.1f)
                {
                    rigidbody.AddForce(Vector2.up * calculatedJumpSpeed, ForceMode2D.Impulse);
                    animator.SetTrigger("Jump");
                }
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
}
