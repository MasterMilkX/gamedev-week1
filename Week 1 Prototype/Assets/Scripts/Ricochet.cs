using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ricochet : MonoBehaviour
{

	public Rigidbody2D rb;
	public float speedModifier = 1.0f;
	public int hitCt = 0;
	public bool game_over = true;

    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update(){
        if(game_over && Input.GetKeyDown("r")){
        	rb.AddForce(new Vector2(10, 10));
        	game_over = false;
        	Debug.Log("here we go!");
        }
    }

    //activate when the ball hits a wall
    void OnCollisionEnter2D(Collision2D c){
    	//increase the speed of the ball after every x hits
    	if(c.gameObject.name == "VWall" || c.gameObject.name == "HWall"){
    		if(hitCt == 5){
    			rb.velocity *= new Vector2(speedModifier,speedModifier);
    			speedModifier *= 1.05f;
    			hitCt = 0;
    		}else{
    			hitCt += 1;
    		}
    		
    	}
    	//game over if the ball hits the blue circle while visible
    	else if(c.gameObject.name == "blue_circle"){
    		rb.velocity = Vector2.zero;
    		Debug.Log("Game over");
    		game_over = true;
    	}
    }
}
