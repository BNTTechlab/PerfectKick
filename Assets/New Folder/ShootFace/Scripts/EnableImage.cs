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
using UnityEngine.SceneManagement;

public class EnableImage : MonoBehaviour {
    public List<GameObject> list;
    public ScoreManager scoreManager;
  //  public PlayerControls player;
    //public ShootingSceneManager shoot;
    public WriteJson writeJson;
    public GameObject soundManhMeLen;
    public GameObject soundNgon;
    public GameObject soundUayUay;
    public GameObject soundTruat;
    public GameObject quitGame;
    public GameObject soundRun;
    public GameObject soundFinish;
	// Use this for initialization
	void Start () {
      //  player.x = 0;

        soundRun.SetActive(false);
        soundFinish.SetActive(false);
        StartCoroutine(Wait());
        
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("Bu");
    }
    public float ShowEndGame()
    {
        float tiLe = 0;
        Highscore highscore = GameObject.FindObjectOfType<WriteJson>().LoadHighScore();
        List<PlayerScore> sortedList = highscore.scores.OrderByDescending(o => o.score).ToList();
        for (int i = 0; i < sortedList.Count; i++)
        {
            if (GameObject.FindObjectOfType<WriteJson>().shoot.namePhoto == sortedList.ElementAt(i).fileName)
            {
                tiLe = (sortedList.Count - i) / (float)sortedList.Count;
            }
        }
        return tiLe;
    }
    // Update is called once per frame
    void Update () {
        quitGame.SetActive(false);
        if (ScoreManager.highscore<=150)
        {
            soundManhMeLen.SetActive(true);
            list[0].SetActive(true);
            
        }
        else
        if (ScoreManager.highscore >150 && ScoreManager.highscore <=300)
        {
            soundNgon.SetActive(true);
            list[1].SetActive(true);
        }
        else
        if (ScoreManager.highscore > 300 && ScoreManager.highscore <= 400)
        {
            soundUayUay.SetActive(true);
            list[2].SetActive(true);
        }
        else
        if (ScoreManager.highscore > 400 )
        {
            soundTruat.SetActive(true);
            list[3].SetActive(true);
        }

    }
}
