using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {
    Text text;
    public static int score;
    public static int highscore;
    public Text Hightext;
   // public PlayerControls player;
    // Use this for initialization
    void Start()
    {
        //Hightext.text = ("HighScore: " + PlayerPrefs.GetInt("highscore"));
        //highscore = PlayerPrefs.GetInt("highscore", 0);

        //if (PlayerPrefs.HasKey("score"))
        //{
        //    Scene ActiveScreen = SceneManager.GetActiveScene();
        //    score = PlayerPrefs.GetInt("score");
        //    PlayerPrefs.DeleteKey("score");
        //    score = 0;
        //}
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        text = GetComponent<Text>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "" + score;
    //    if(player.curHealth == 0 || player.end)
        {
            highscore = score;
            
        }
    }
}
