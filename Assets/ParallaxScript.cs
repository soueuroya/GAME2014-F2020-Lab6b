﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    public Rigidbody2D target;
    public float slower;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(target.velocity.x / slower * Time.deltaTime, target.velocity.y / 5 * Time.deltaTime));
    }
}