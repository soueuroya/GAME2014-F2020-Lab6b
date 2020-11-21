using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButtonScript : MonoBehaviour
{

    public PlayerMovementScript pms;

    public void Attack()
    {
        pms.Attack();
    }

}