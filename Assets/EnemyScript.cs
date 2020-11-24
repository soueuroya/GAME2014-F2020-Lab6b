using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int direction = 1;
    public Rigidbody2D rb;
    public bool hasTarget = false;
    public bool isShooter;
    public int speed;
    public AudioSource audio;
    void Start()
    {
        
    }

    void Update()
    {
        if (!isShooter)
        {
            rb.velocity = new Vector2(speed * direction, 0);
        }
        else
        {
            if (!hasTarget)
            {
                rb.velocity = new Vector2(speed * direction, 0);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hasTarget = true;
        }

        if (collision.tag == "Sword")
        {
            audio.Play();
            Destroy(gameObject, 0.2f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            SwapDirection();
        }
        else if (collision.tag == "Player")
        {
            hasTarget = false;
        }
    }

    public void SwapDirection()
    {
        if (direction == 1)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
            direction = -1;
        }
        else
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            direction = 1;
        }
    }
}
