using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableBlueCircle : MonoBehaviour
{
	public SpriteRenderer spr;
	public bool visible = true;
	public int score = 0;
	public int highscore = 0;
	public bool setNewScore = false;
	public float t = 0;
	public bool canHide = false;

	//gui
	public Text scoreboard;
	public GameObject statescreen;
	public Text titleTxt;
	public Text highscoreTxt;
	public bool firstScreen = true;

    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        score = 0;

        Debug.Log(PlayerPrefs.GetInt("highscore"));
        statescreen.SetActive(true);

        //set high score
        if(PlayerPrefs.HasKey("highscore")){
            highscore = PlayerPrefs.GetInt("highscore");
            highscoreTxt.text = "High Score: " + highscore.ToString();
            Debug.Log("High score not found!");
        }else{
            highscore = 0;
            PlayerPrefs.SetInt("highscore", highscore);
        }

        //show title screen ui
        titleTxt.text = "Flew Thru Blue";
        statescreen.SetActive(true);
        firstScreen = true;
    }

    // Update is called once per frame
    void Update()
    {	
        if(Input.GetKey("space")){
        	//make circle transparent
        	spr.color = new Color(1f,1f,1f,0.3f);
        	visible = false;

        	//if(t != 0){t = 0;}		//reset timer

        }else{
        	//show circle
        	spr.color = new Color(1f,1f,1f,1f);
        	visible = true;

        	if(canHide){
        		//increase score
        		IncScore(0.75f);
        	}
        }

        //on gameover set new high score if applicable
        if(!firstScreen && !canHide && !setNewScore){
        	if(score > highscore){
        		highscore = score;
        		PlayerPrefs.SetInt ("highscore", highscore);
        	}
        	titleTxt.text = "GAME OVER";
        	highscoreTxt.text = "High Score: " + highscore.ToString();
        	statescreen.SetActive(true);

        	setNewScore = true;
        }
        //hide game screen
        if(canHide && statescreen.activeSelf){
        	setNewScore = false;
        	statescreen.SetActive(false);
        }
    }

    //updates the score every x seconds when the player is visible
    void IncScore(float freq){
    	t += Time.deltaTime;
	    if (t >= freq) {
	        t = t % freq;
	        score += 1;
	        scoreboard.text = score.ToString();			//update score
	    }
    }
}
