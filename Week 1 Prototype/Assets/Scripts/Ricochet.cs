using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Ricochet : MonoBehaviour
{

    public Rigidbody2D rb;
    //public float speedModifier = 1.0f;
    //public int hitCt = 0;
    public float maxSpeed = 35;
    public bool game_over = true;
    Vector2 initialMovement;
    public Transform randPos;

    public GameObject player;
    public DisableBlueCircle dbc;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dbc = player.GetComponent<DisableBlueCircle>();
    }

    //start the circle in a random position
    void ToRandPos(){
        Transform[] pos = randPos.GetComponentsInChildren<Transform>();
        int p = Random.Range(0, pos.Length);
        this.transform.position = pos[p].position;
    }

    void Update()
    {
        if (game_over && Input.GetKeyDown("r"))
        {
            //goto random point
            ToRandPos();

            initialMovement = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            initialMovement.Normalize();
            rb.AddForce(80 * initialMovement);

            game_over = false;
            Debug.Log("Game Start!");

            //allow player to hide and reset score
            dbc.score = 0;
            dbc.canHide = true;
            dbc.firstScreen = false;

        }

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

    }

    //activate when the ball hits a wall
    void OnCollisionEnter2D(Collision2D c)
    {
        //increase the speed of the ball after every x hits
        if (c.gameObject.name == "VWall" || c.gameObject.name == "HWall")
        {
            /*
            if (hitCt == 5)
            {
                rb.velocity *= new Vector2(speedModifier, speedModifier);
                speedModifier *= 1.05f;
                hitCt = 0;
                Debug.Log("speed up!");
            }
            else
            {
                hitCt += 1;
            }
            */

            //make sure velocity on either axis is not too small to prevent repeated bouncing in a straight line 
            if (Mathf.Abs(rb.velocity.x) <= 0.1f)
            {
                initialMovement = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                rb.velocity = initialMovement.normalized * rb.velocity.magnitude;

            }

            if (Mathf.Abs(rb.velocity.y) <= 0.1f)
            {
                initialMovement = new Vector2(Random.Range(1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                rb.velocity = initialMovement.normalized * rb.velocity.magnitude;
            }
        }
    }

    //game over if the ball hits the blue circle while visible
    void OnTriggerEnter2D(Collider2D c){
        
        if (c.gameObject == player && dbc.visible)
        {
            rb.velocity = Vector2.zero;
            Debug.Log("Game over");
            game_over = true;
            dbc.canHide = false;
        }
    }

    //same as above but if player reactivates while red circle is in middle 
    void OnTriggerStay2D(Collider2D c){
        if (c.gameObject == player && dbc.visible)
        {
            rb.velocity = Vector2.zero;
            Debug.Log("Game over");
            game_over = true;
            dbc.canHide = false;
        }
    }
}