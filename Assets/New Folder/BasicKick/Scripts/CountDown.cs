using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {

	// Use this for initialization
	int _startTime; //thoi gian de bat dau tinh --> khi goij ham Awake
	int _restSecond; //tong thoi gian da chay tu khi bat dau
	int _roundedRestSecond; // tong thoi gian da chay (duoc lam tron)
	int _didsplaySecond; //thoi gian giay hien thi tren man hinh
	int _displayMinute; //thoi gian phut hien thi tren man hinh
	int _countDownTime; //thoi gian se dem nguoc duoc truyen vao qua ham Awake
	int _guiTime; //thoi gian se hien thi tren GUI (neu dung GUI);
	bool _timerActive; // da bat dau tinh gio hay chua

	Text _timeTxt;

    public Text time;

	void Start () {
		_timerActive = false;
		_countDownTime = 0;

		_timeTxt = gameObject.GetComponent<Text> ();
      //  awake(45); 
	}

	public void awake (int time)
	{
		
		_countDownTime = time;
		_startTime = (int)Time.time;
		if (_countDownTime < 10) {
			_timeTxt.text = "0" + _countDownTime.ToString () + "s";
		} else {
			_timeTxt.text = _countDownTime.ToString () + "s";
		}
		_timerActive = true;

	}

	void sleep ()
	{
		_timerActive = false;
		_countDownTime = 0;
		_timeTxt.text = "00s";
	}

	// Update is called once per frame
	void Update () {
		
		if (_timerActive) {
			onCountDown ();
		} else {
			_startTime = (int)Time.time; //reset time
		}
	}

	public void addPlayTime(int time)
	{
		_countDownTime += time;
	}

	void onCountDown()
	{
		
		_guiTime = (int)Time.time - _startTime;

		_restSecond = _countDownTime - _guiTime;

		//hien thi timer
		_roundedRestSecond = Mathf.CeilToInt(_restSecond);
		_roundedRestSecond = Mathf.Clamp (_roundedRestSecond, 0, _roundedRestSecond);

		_didsplaySecond = _roundedRestSecond % 60;
		_displayMinute = _roundedRestSecond / 60;

        if (_didsplaySecond + _displayMinute * 60 - GameData.Instance().timePlay <= -1)
        {
            GameData.Instance().comboTime--;
        }

        GameData.Instance ().timePlay = _didsplaySecond + _displayMinute * 60;

		if (_didsplaySecond <= 0 && _displayMinute == 0) {
			_timeTxt.text = "00";
			sleep ();
		} else {
            if (GameData.Instance().comboTime <= 0)
            {
                if (_didsplaySecond < 10)
                {
                    _timeTxt.text = "0" + _didsplaySecond.ToString() + "s";
                }
                else
                {
                    _timeTxt.text = _didsplaySecond.ToString() + "s";
                }
            }
        }
	}

	void displayCoundDown()
	{
		string txt = string.Format ("{00:00}:{01:00}", _displayMinute, _didsplaySecond);
		GUI.Label (new Rect (400, 25, 50, 30), txt);
		if (GUI.Button (new Rect (10, 10, 100, 30), "Start Timer")) {
			_timerActive = true;
		} else {
			_timerActive = false;
		}
	}
}
