using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    public Rigidbody2D target;
    public float slower;
    public bool followY;

    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.Find("Main Camera").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (followY)
        {
            //transform.Translate(new Vector2(0, target.velocity.y / 5 * Time.deltaTime));
        }
        else
        {
            //transform.Translate(new Vector2(target.velocity.x / slower * Time.deltaTime, 0));
        }
    }
}
