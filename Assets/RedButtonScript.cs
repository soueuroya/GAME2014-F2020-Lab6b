using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButtonScript : MonoBehaviour
{

    public PlayerMovementScript pms;

    public void Attack()
    {
        if (pms.isGrounded && !pms.isJumping && !pms.isAttacking)
        {
            pms.Attack();
        }
    }

}