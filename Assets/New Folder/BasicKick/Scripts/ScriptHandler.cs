using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScriptHandler : MonoBehaviour
{
    public WriteJson writeJson;
    public GameObject Ball;
    
    GameObject Kick;
    GameObject timeRemain;
    GameObject playerScore;
	GameObject ballLauch;
	GameObject target;
	GameObject goalAi;
    GameObject combo;
    GameObject comboTime;

    float _bombTime; // thoi gian tiep theo se tao bong
    float _lastBombTime;// lan cuoi cung tao bong la luc nao

	float _waitTime; // thoi gian cho ket thuc game
	float _lastWaitTime;// lan cuoi cho ket thuc game

	SoundController soundController;

	public Image myAvatar;

    GameObject ball;
	GameObject imgStart;
	GameObject imgFinish;
    float _comboTime;
    float _createComboTime;

	bool _gameover;

    GUIStyle fontSize;
    public List<GameObject> list;
    public List<GameObject> list1;
    int t = 0;

    void Start()
    {

        WriteJson writeJson = gameObject.GetComponent<WriteJson>();

        soundController = GameObject.Find ("SoundController").GetComponent<SoundController>();

        fontSize = new GUIStyle();
        _comboTime = 10;
        _createComboTime = Time.time;
        timeRemain = GameObject.Find("timeRemain");
        playerScore = GameObject.Find("score");

        combo = GameObject.Find("combo");
        comboTime = GameObject.Find("comboTime");

        ballLauch = GameObject.Find("Launcher");
		target = GameObject.Find("Target");
		goalAi = GameObject.Find("GoalKeeperSideToSide");

        resetGame();
        fontSize.fontSize = 50;
        


        GameData.Instance().timePlay = 45; //45
        StartCoroutine(StartGame());

    }


    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2);
        updateBombTime();
        timeRemain.GetComponent<CountDown>().awake(GameData.Instance().timePlay);
        _gameover = false;
       soundController.playSoundBG(false);
        soundController.startGame();
        createBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _lastBombTime + _bombTime)
        {
            createBall();

        }

        if (!GameData.Instance().gameMode)
        {
            if (Time.time > _createComboTime + _comboTime)
            {
                GameData.Instance().gameMode = true;
            }
        }

        if (GameData.Instance ().timePlay == 0) {

            KickScript k = GameObject.Find("Kick").GetComponent<KickScript>();
            k.isAllow = false;
            _gameover = true;
            _bombTime = 3000000;
            Destroy(ball, 0.1f);
            Debug.Log ("het gio roi: ");
			GameData.Instance ().timePlay = -1;
			soundController.finishGame();
			list1 [0].SetActive( true);
			_lastWaitTime = Time.time;
			_waitTime = 3.0f;
            
        }

		if (Time.time > _lastWaitTime + _waitTime)
		{
			_lastWaitTime = Time.time;
			_waitTime = 30000000;
			overTime ();
		}

		if (GameData.Instance ().timePlay == 45) {
			writeJson.getMyImage();
			myAvatar.sprite = GameData.Instance ().myImage;
		}

    }

	void resetGame()
	{
		GameData.Instance().gameMode = true;
		GameData.Instance().score = 0;
		GameData.Instance().numShoot = 0;
        _bombTime = 3000000;
        _createComboTime = Time.time;
		_lastWaitTime = Time.time;
		_waitTime = 30000000;

        GameData.Instance().combo = 0;
	}

    void overTime ()
    {
        //Debug.Log ("ket thuc game chua diem: " + GameData.Instance ().allScore [GameData.Instance ().allScore.Count - 1]);
        //Hashtable obj = GameData.Instance ().allScore [GameData.Instance ().allScore.Count - 1];
        //obj ["score"] = GameData.Instance ().score;
        //Debug.Log ("ket thuc game co diem: " + GameData.Instance ().allScore [GameData.Instance ().allScore.Count - 1]);
      

        
        if (t == 0)
        {

            writeJson.Save();
           
        }
        t++;
        
        list[0].SetActive(true);
    }

	public void missShot()
	{
		GameData.Instance ().combo = 0;
		soundController.shotMiss ();
	}

	void createCombo()
	{
        combo.GetComponent<Combo>().createImage();

    }

    public void updateScore()
    {
        if (GameData.Instance().gameMode)
        {
            GameData.Instance().combo++;
            //Debug.Log("so luot da trung gon : " + GameData.Instance().combo);
            createCombo();
            if (GameData.Instance().combo == 5)
            {
                soundController.playSoundStartCombo();
                GameData.Instance().gameMode = false;
                GameData.Instance().combo = 0;
                GameData.Instance().timePlay += 10;
                GameData.Instance().comboTime = 10;
                timeRemain.GetComponent<CountDown>().addPlayTime(10);
                _createComboTime = Time.time;
                comboTime.GetComponent<ComboTime>().createImage();
            }

        }

        playerScore.GetComponent<PlScore>().updateScore();

    }

    void updateBombTime()
    {
        _lastBombTime = Time.time;
        _bombTime = 1.5f;

    }

    void createBall()
    {
		goalAi.GetComponent<GoalieAI>().updateBackTime ();
        _bombTime = 3000000;
		KickScript k = GameObject.Find ("Kick").GetComponent<KickScript> ();
		k.ResetKick ();

        ball = Instantiate(Ball, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;

        ball.GetComponent<BallScript>().getTarget (ballLauch, target);
    }

    public void ballMove(float[] pr, bool foot)
    {
		if (!_gameover) {
			soundController.shotGame ();
			updateBombTime();
            goalAi.GetComponent<GoalieAI>().updateAngle(pr[2],pr[1]);
			if (ball != null)
			{
				if(foot)
				{
					ball.GetComponent<BallScript>().KickForce(pr);
				}
				else
				{
					ball.GetComponent<BallScript>().KickForce2(pr);
				}
                goalAi.GetComponent<GoalieAI>().Jump();
            }
		}


    }

    //void OnGUI()
    //{
    //    GUI.Label(new Rect(Screen.width/2 - 50, 150, 100, 200), kick, fontSize);
    //}
}
