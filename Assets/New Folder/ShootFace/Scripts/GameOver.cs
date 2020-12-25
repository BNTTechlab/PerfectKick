using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOver : MonoBehaviour {
    public PlayerControls player;
    public static int score;
    Text text;
    public List<GameObject> list;
    // Use this for initialization
    void Start () {
       // list[0].SetActive(false);
       // text = GetComponent<Text>();
       // score = 0;
       //// player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
       // StartCoroutine(WaitTimeReloadGame());
       // // Application.LoadLevel(1);
       // text.text = "Score: " + ScoreManager.score;
    }

    IEnumerator WaitTimeReloadGame()
    {
        yield return new WaitForSeconds(5f);

    }
    void Update()
    {
      
       // text.text = "Rank: ";
    }
}
