using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.Text;
using UnityEngine;
using System.IO;
using SimpleJSON;
using System.Linq;
using UnityEngine.UI;
public class WriteJson : MonoBehaviour
{

    public Text best;
   // public ScoreManager scoreManager;
    public int id;
    // public long userID;
    public int score;
   // public PlayerControls player;
    public ShootingSceneManager shoot;

    //Scoreboard
    public List<Image> userIconList;
    public List<Text> userScoreList;

    public List<Image> IconList;

    public float tiLe;
    // Use this for initialization
    void Start()
    {

        LoadHighscoreToScoreBoard();
    }

	public void getMyImage()
	{
		GameData.Instance().myImage = LoadFileToSprite(GameData.Instance().myPathImage);
	}

    public void Save()
    {
        JSONObject playerJson = new JSONObject();
        Highscore highScore = LoadHighScore();
        PlayerScore currentUserScore = new PlayerScore(shoot.namePhoto, GameData.Instance().score);
        if (highScore == null || highScore.scores == null)
        {
            highScore = new Highscore();
            highScore.scores = new List<PlayerScore>();
        }
        highScore.scores.Add(currentUserScore);
        //save json in computer
        string jsonString = JsonUtility.ToJson(highScore);
        //File.WriteAllText(path, jsonString);
        PlayerPrefs.SetString("PlayerScore", jsonString);
       

    }
    // save đc rồi mới load highscore, gán hết giá trị trong highscore của đối tượng 
    public Highscore LoadHighScore()
    {
       // PlayerPrefs.DeleteAll();
        try
        {

            string jsonString = PlayerPrefs.GetString("PlayerScore");
			
            //Debug.Log(jsonString); 
            if (string.IsNullOrEmpty(jsonString))
                jsonString = "{}";
            return JsonUtility.FromJson<Highscore>(jsonString);
        }
        catch (FileNotFoundException e) 
        {
            return null;
        }

    }

     void LoadHighscoreToScoreBoard()
    {
        Highscore highscore = LoadHighScore();
        List<PlayerScore> sortedList = highscore.scores.OrderByDescending(o => o.score).ToList();
        for (int i = 0; i < userIconList.Count; i++)
        {
            if (sortedList.Count > i)
            {
                userIconList.ElementAt(i).sprite = LoadFileToSprite(sortedList.ElementAt(i).fileName);
				userIconList.ElementAt (i).enabled = true;
                userScoreList.ElementAt(i).text = sortedList.ElementAt(i).score.ToString();
            }
        }
		if (sortedList.Count > 0) {
			best.text = userScoreList.ElementAt(0).text;
			IconList.ElementAt(0).sprite = LoadFileToSprite(sortedList.ElementAt(0).fileName);
			userIconList.ElementAt (0).enabled = true;
		}

		if (sortedList.Count < userIconList.Count) {
			for (int i = sortedList.Count; i < userIconList.Count; i++)
			{
				userIconList.ElementAt (i).enabled = false;
			}
		}

        
    }
    //public Sprite BestAva()
    //{
    //    return userIconList.ElementAt(1).sprite;
    //}
    //public Text BestScore()
    //{
    //     userScoreList.ElementAt(1).text;
    //}

    Sprite LoadFileToSprite(string path)
    {
        Texture2D tex = null;
        byte[] fileData;
        string filePath = Application.dataPath + "/img/players/" + path;
        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.name = path;
            tex.LoadImage(fileData);
            Rect rec = new Rect(0, 0, tex.width, tex.height);
        return Sprite.Create(tex, rec, new Vector2(0, 0), 1); ;
        }
        return null;
        
        
    }


}