using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreFinal : MonoBehaviour {
    public Text text;
    public Text Hightext;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        text.text = "" + GameData.Instance().score.ToString();
   //     Hightext.text = Mathf.FloorToInt(GameObject.FindObjectOfType<EndGame>().ShowEndGame() * 100).ToString() + "%";
    }

}
