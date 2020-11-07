using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Joystick joystick;
    public float joystickHorizontalSensitivity;
    public float joystickVerticalSensitivity;
    public float horizontalForce;
    public float verticalForce;
    public bool isGrounded;
    public bool isJumping;
    public Transform spawnPoint;

    private Rigidbody2D m_rigidBody2D;
    private SpriteRenderer m_spriteRenderer;
    private Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody2D = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
    }

    void _Move()
    {
        if (isGrounded)
        {
            if (joystick.Horizontal > joystickHorizontalSensitivity || Input.GetAxisRaw("Horizontal") > joystickHorizontalSensitivity)
            {
                m_rigidBody2D.velocity = (Vector2.right * horizontalForce * Time.deltaTime);
                m_spriteRenderer.flipX = false;
                m_animator.SetInteger("AnimState", 1);
            }
            else if (joystick.Horizontal < -joystickHorizontalSensitivity || Input.GetAxisRaw("Horizontal") < -joystickHorizontalSensitivity)
            {
                m_rigidBody2D.velocity = (Vector2.left * horizontalForce * Time.deltaTime);
                m_spriteRenderer.flipX = true;
                m_animator.SetInteger("AnimState", 1);
            }
            else if(!isJumping)
            {
                m_animator.SetInteger("AnimState", 0);
            }


            if ((joystick.Vertical > joystickVerticalSensitivity) && (!isJumping))
            {
                m_rigidBody2D.AddForce(Vector2.up * verticalForce * Time.deltaTime, ForceMode2D.Impulse);
                m_animator.SetInteger("AnimState", 2);
                isJumping = true;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        //isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // respawn
        if (other.gameObject.CompareTag("DeathPlane"))
        {
            transform.position = spawnPoint.position;
        }else if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
