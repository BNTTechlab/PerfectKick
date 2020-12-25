using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown1 : MonoBehaviour {
    public Text time;
    public bool timeOut=false;
    float timeRemaining = 61;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        timeRemaining -= Time.deltaTime;

	}
     public void OnGUI()
    {
        if (timeRemaining > 0)
        {
            time.text = "" + (int)timeRemaining;
        }
        else
            timeOut = true;

    }
}
