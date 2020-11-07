using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetScript : MonoBehaviour
{

    public PlayerMovementScript pms;

    // Start is called before the first frame update
    void Start()
    {
        pms = GameObject.Find("Player").GetComponent<PlayerMovementScript>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            pms.Grounded();
        }
        else if (collision.gameObject.tag == "DeathPlane")
        {

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            pms.UnGrounded();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            pms.Grounded();
        }
    }
}
