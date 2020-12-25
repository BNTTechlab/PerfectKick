using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlScore : MonoBehaviour {

    
    // Use this for initialization
	Text _timeTxt;
    public Text currentScore;
    void Start () {
        
		_timeTxt = gameObject.GetComponent<Text> ();

		_timeTxt.text = GameData.Instance ().score.ToString ();
      //  currentScore.text = GameData.Instance().score.ToString();

        updateScore ();

       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void updateScore()
	{
		_timeTxt.text = GameData.Instance ().score.ToString ();
     
    }
}
