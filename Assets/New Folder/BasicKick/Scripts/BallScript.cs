using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallScript : MonoBehaviour {

	bool isTarget;//da trung target nao chua

    public GameObject trailNormal;
    public GameObject trailSpe;

    //bounus
    public GameObject original;
    public GameObject original1;

    GameObject ballLauncher;
	GameObject Target;
    float time;
	public float distance;
	public float angle;
	float initialVelocity;
	float finalVelocity;
	float force;

	int _score = 25;//diem them duoc moi lan sut vao nhung ko trung muc tieu
	int _curScore;//so diem hien tai dang co

	ScriptHandler handler;
	Vector3 ballPosition;

	float _destroyTime; // thoi gian tiep theo se tao bomb
	float _createTime;// lan cuoi cung tao bomb la luc nao
	int kicked;
	bool isDestroy;

	float _timeGoalPos;//thoi gian check khi co dap coc, dap xa
	float _createTimeGoal;
	bool isGoalPos;

	TrailRenderer _trailNormal;
    TrailRenderer _trailSpe;

    public List<GameObject> listTrail;
    public static string goal = null;
    float height;
    public float xAxis, yAxis, dis;
    void Start()
	{

        _trailNormal = trailNormal.GetComponent<TrailRenderer> ();
        _trailNormal.enabled = true;
        _trailSpe = trailSpe.GetComponent<TrailRenderer>();
        _trailSpe.enabled = false;
        handler = GameObject.Find("ScriptHandler").GetComponent<ScriptHandler>();
		ballPosition = transform.position;
		_destroyTime = 500000;
		_createTime = Time.time;
		kicked = 0;
		isDestroy = false;
        
	}
   
	public void getTarget(GameObject bl, GameObject tg)

	{
		ballLauncher = bl;
		Target = tg;
	}

    //right foot
    public void KickForce(float[] param)
    {
		
		//Debug.Log("goi da chan phai : " + kicked);
        // deg, h 
        if (kicked > 0)
            return;
        updateDetroyTime();

        time = param[0];
        distance = param[1];
        angle = param[2];
        initialVelocity = param[3];
        finalVelocity = param[4];
        Debug.Log("Angle Right Foot: " + distance);
        //Debug.Log("Heigh Right Foot: " + angle);
        //int rnd = Random.Range(0, 2);
        //int rnd2 = Random.Range(0, 2);

        //giơ chân thấp và cao quyết định heigh của bóng
        //  if (angle < 0.15f)
        //  {
        //      if (rnd2 == 0)
        //      {
        //          height = -2.2f + Random.Range(0.1f, 0.2f);
        //      }
        //      else
        //          height = -2.2f - Random.Range(0.1f, 0.3f);
        //  }
        //  else
        //      if (angle < 0.27f)
        //  {
        //      if (rnd2 == 0)
        //      {
        //          height = -1.2f + Random.Range(0.1f, 0.2f);
        //      }
        //      else
        //          height = -1.2f - Random.Range(0.1f, 0.2f);
        //  }
        //  else
        //  {
        //      if (rnd2 == 0)
        //      {
        //          height = -0.3f + Random.Range(0.1f, 0.2f);
        //      }
        //      else
        //          height = -0.3f - Random.Range(0.1f, 0.3f);
        //  }

        ////  Debug.Log("height chan phai : " + height);

        //  //tùy độ cao của target mà chỉnh quỹ đạo đường bay
        //  if (height <= -2f)
        //  {
        //      ballLauncher.GetComponent<BallLauncher>().h = 0.7f;
        //  }
        //  else
        //  if (height <= -1f)
        //  {
        //      ballLauncher.GetComponent<BallLauncher>().h = 1.5f;
        //  }
        //  else if (height <= -0.1f)
        //  {
        //      ballLauncher.GetComponent<BallLauncher>().h = 2.1f;
        //  }

        //  //Debug.Log("h chan phai : " + ballLauncher.GetComponent<BallLauncher>().h);
        //  //góc 
        //  if (distance < -0.12f) //0.13f
        //  {
        //      if (rnd == 0)
        //      {
        //          Target.transform.position = new Vector3(-17.8f - Random.Range(0.2f, 0.6f), height, 21f);
        //      }
        //      else
        //          Target.transform.position = new Vector3(-17.8f + Random.Range(0.2f, 0.6f), height, 21f);
        //      Debug.Log("left < -0.13" + Target.transform.position + "heigh" + height);
        //  }
        //  else
        //  if (distance < -0.05f) //-0.09f
        //  {
        //      if (rnd == 0)
        //      {
        //          Target.transform.position = new Vector3(-16.8f - Random.Range(0.1f, 0.8f), height, 21f);
        //      }
        //      else
        //          Target.transform.position = new Vector3(-16.8f + Random.Range(0.1f, 0.7f), height, 21f);
        //   //   Debug.Log("left < -0.09" + Target.transform.position + "heigh" + height);

        //  }
        //  else
        //  if (distance < 0.1f) //0.11f
        //  {
        //      if (rnd == 0)
        //      {
        //          Target.transform.position = new Vector3(-14.8f - Random.Range(0.1f, 0.9f), height, 21f);
        //      }
        //      else
        //          Target.transform.position = new Vector3(-14.8f + Random.Range(0.1f, 0.9f), height, 21f);
        //   //   Debug.Log("center" + Target.transform.position + "heigh" + height);
        //  }
        //  else
        //      if (distance < 0.23f) //0.25f
        //  {
        //      if (rnd == 0)
        //      {
        //          Target.transform.position = new Vector3(-12.7f - Random.Range(0.1f, 0.8f), height, 21f);
        //      }
        //      else
        //          Target.transform.position = new Vector3(-12.7f + Random.Range(0.1f, 0.8f), height, 21f);
        //  //    Debug.Log("right < 0.2" + Target.transform.position + "heigh" + height);            
        //  }
        //  else            
        //  {
        //      if (rnd == 0)
        //      {
        //          Target.transform.position = new Vector3(-11.8f + Random.Range(0.1f, 0.9f), height, 21f);
        //      }
        //      else
        //          Target.transform.position = new Vector3(-11.8f - Random.Range(0.1f, 0.5f), height, 21f);
        //   //   Debug.Log("right >= 0.2" + Target.transform.position + "heigh" + height);
        //  }

        xAxis = (distance) * 1.3f / 0.4f;

        // kickParams[2] = 0.5f; // goc y chân giơ lên 0.1 -- 0.45
        dis = angle;//- 0.15f;
        if (dis < 0)
        {
            dis = 0;
        }
        yAxis = dis * 2.3f / 0.45f;
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(xAxis, yAxis, 1.0f) * 130);

        //ballLauncher.GetComponent<BallLauncher>().Launch(gameObject.GetComponent<Rigidbody>(), Target.transform);

		kicked++;

    }

    //left foot
    public void KickForce2(float[] param)
    {
        if (kicked > 0)
            return;
        time = param[0];
        distance = param[1];
        angle = param[2];
        initialVelocity = param[3];
        finalVelocity = param[4];
        //Debug.Log("Angle Left Foot : " + distance);
        //Debug.Log("Heigh Left Foot: " + angle);
        int rnd = Random.Range(0, 2);
        int rnd2 = Random.Range(0, 2);
        float xAdd = Random.Range(0.1f, 0.9f);

        updateDetroyTime();

        //giơ chân thấp và cao quyết định heigh của bóng
        //      if (angle < 0.15f)
        //      {
        //          if (rnd2 == 0)
        //          {
        //              height = -2.2f + Random.Range(0.1f, 0.2f);
        //          }
        //          else
        //              height = -2.2f - Random.Range(0.1f, 0.3f);

        //      }
        //      else
        //          if (angle < 0.27f)
        //      {
        //          if (rnd2 == 0)
        //          {
        //              height = -1.0f + Random.Range(0.1f, 0.2f);
        //          }
        //          else
        //              height = -1.0f - Random.Range(0.1f, 0.2f);

        //      }
        //      else
        //      {
        //          if (rnd2 == 0)
        //          {
        //              height = -0.3f + Random.Range(0.1f, 0.2f);
        //          }
        //          else
        //              height = -0.3f - Random.Range(0.1f, 0.3f);

        //      }

        //     // Debug.Log("height chan trái : " + height);

        //      //tùy độ cao của target mà chỉnh quỹ đạo đường bay
        //      if (height <= -2f)
        //      {
        //          ballLauncher.GetComponent<BallLauncher>().h = 0.7f;
        //      }
        //      else
        //      if (height <= -1f)
        //      {
        //          ballLauncher.GetComponent<BallLauncher>().h = 1.6f;
        //      }
        //      else if (height <= -0.1f)
        //      {
        //          ballLauncher.GetComponent<BallLauncher>().h = 2.2f;
        //      }

        //      //góc
        //      if (distance < -0.1f) //0.12f
        //      {
        //          //if (rnd == 0)
        //          //{
        //          //    Target.transform.position = new Vector3(-18f - Random.Range(0.2f, 0.8f), height, 21f);
        //          //}
        //          //else
        //              Target.transform.position = new Vector3(-17.7f - Random.Range(0.3f, 2f), height, 21f);
        //          Debug.Log("left < -0.13" + Target.transform.position + "heigh" + height);
        //      }
        //      else
        //      if (distance < -0.03f) //0.06f
        //      {
        //          if (rnd == 0)
        //          {
        //              Target.transform.position = new Vector3(-16.8f - Random.Range(0.1f, 0.8f), height, 21f);
        //          }
        //          else
        //              Target.transform.position = new Vector3(-16.8f + Random.Range(0.1f, 0.7f), height, 21f);
        //          Debug.Log("left < -0.09" + Target.transform.position + "heigh" + height);

        //      }
        //      else
        //      if (distance < 0.06f)  //0.09f
        //      {
        //          if (rnd == 0)
        //          {
        //              Target.transform.position = new Vector3(-14.8f - Random.Range(0.1f, 0.9f), height, 21f);
        //          }
        //          else
        //              Target.transform.position = new Vector3(-14.8f + Random.Range(0.1f, 0.9f), height, 21f);
        //          Debug.Log("center" + Target.transform.position + "heigh" + height);
        //      }
        //      else
        //          if (distance < 0.15f) //0.19f
        //      {
        //          if (rnd == 0)
        //          {
        //              Target.transform.position = new Vector3(-12.6f - Random.Range(0.1f, 0.8f), height, 21f);
        //          }
        //          else
        //              Target.transform.position = new Vector3(-12.6f + Random.Range(0.1f, 0.8f), height, 21f);
        //          Debug.Log("right < 0.2" + Target.transform.position + "heigh" + height);
        //      }
        //      else
        //      {
        //          //if (rnd == 0)
        //          //{
        //          //    Target.transform.position = new Vector3(-11f + Random.Range(0.1f, 0.9f), height, 21f);
        //          //}
        //          //else
        //              Target.transform.position = new Vector3(-11.7f + Random.Range(0.1f, 2.3f), height, 21f);
        //          Debug.Log("right >= 0.2" + Target.transform.position + "heigh" + height);
        //      }
        //    //  Debug.Log("target position: " + Target.transform.position);
        //ballLauncher.GetComponent<BallLauncher>().Launch(gameObject.GetComponent<Rigidbody>(), Target.transform);
        float xAxist = (distance - 0.1f) * 1.3f / 0.4f;
        // kickParams[2] = 0.5f; // goc y chân giơ lên 0.1 -- 0.45
        float dis = angle;// - 0.15f;

        if (dis < 0)
        {
            dis = 0;
        }
        float yAxist = dis * 2.3f / 0.45f;
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(xAxist + 0.2f, yAxist, 1.0f) * 130);


        kicked++;

       // handler.SendMessage("NextKick", SendMessageOptions.DontRequireReceiver);
    }
    void updateDetroyTime()
	{
		_destroyTime = 5.0f;
		_createTime = Time.time;
	}

	public void resetPos(int pos)
	{
		
		Vector3 pB = ballPosition;


		if (pos == -1)
		{
			pB.x = pB.x - 5;

		}
		else if (pos == 1)
		{
			pB.x = pB.x + 5;

		}
		//Debug.Log("reset vitri moi cua bong : " + pB);

		//bo khoa het cac truc
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

		transform.position = pB;
	}

	void Update()
	{
        //
        ballLauncher.GetComponent<BallLauncher>().DrawPath(gameObject.GetComponent<Rigidbody>(), Target.transform);

        //Debug.Log("time bong : " + Time.time + "--" + _createTime + _destroyTime);
        if (Time.time > _createTime + _destroyTime && !isDestroy) {
			isDestroy = true;
            _destroyTime = 3000000;
            
			Destroy (gameObject, 1);
		}

		if (GameData.Instance ().gameMode) {
			_trailNormal.enabled = true;
            _trailSpe.enabled = false;
            listTrail[0].SetActive(false);
        } else {
			_trailSpe.enabled = true;
            _trailNormal.enabled = false;
            //
            listTrail[0].SetActive(true);
        }

		if (isGoalPos) {
			if (Time.time > _timeGoalPos + _createTimeGoal) {
				if (!isTarget) {
					missShot ();
					Destroy(gameObject, 0.2f);
					isGoalPos = false;
				}
			}
		}
        

	}

    void OnTriggerEnter(Collider other)
    {
		
        if (other.GetComponent<Collider>().tag == "Goal")
        {
            goal = "Goal";
        }
        else if (other.GetComponent<Collider>().name == "connerCollider")
        {
			if (!isTarget) {
				isTarget = true;
				addScore();

			}
            
        }
        else if (other.GetComponent<Collider>().name == "connerCollider1")
        {
			if (!isTarget) {
				isTarget = true;
				addScore();
			}
        }
        else if (other.GetComponent<Collider>().name == "horCollider")
        {
			if (!isTarget) {
				isTarget = true;
				addScore();
			}
        }
        else if (other.GetComponent<Collider>().name == "rightCollider")
        {
			if (!isTarget) {
				isTarget = true;
				addScore();
			}
        }
        else if (other.GetComponent<Collider>().name == "leftCollider")
        {
			if (!isTarget) {
				isTarget = true;
				addScore();
			}
        }
        else if (other.GetComponent<Collider>().name == "centerCollider")
        {
			if (!isTarget) {
				isTarget = true;
				addScore();
			}
        }
		else if (other.GetComponent<Collider>().tag == "Terrain")
		{
			if (!isTarget) {
				isTarget = true;

			}
		}
		else if (other.GetComponent<Collider>().name == "MissOverTheBar")
		{
			missShot ();
		}
		else if (other.GetComponent<Collider>().name == "MissRight")
		{
			missShot ();
		}
		else if (other.GetComponent<Collider>().name == "MissLeft")
		{
			missShot ();
		}
		else if (other.GetComponent<Collider>().name == "CrossBar")
		{
			isGoalPos = true;
			_timeGoalPos = 0.5f;//thoi gian check khi co dap coc, dap xa
			_createTimeGoal = Time.time;
		}
		else if (other.GetComponent<Collider>().name == "GoalpostLeft")
		{
			isGoalPos = true;
			_timeGoalPos = 0.5f;//thoi gian check khi co dap coc, dap xa
			_createTimeGoal = Time.time;

		}
		else if (other.GetComponent<Collider>().name == "GoalpostRight")
		{
			isGoalPos = true;
			_timeGoalPos = 0.5f;//thoi gian check khi co dap coc, dap xa
			_createTimeGoal = Time.time;

		}
		else if (other.GetComponent<Collider>().name == "GoalKeeperSideToSide")
		{
			isGoalPos = true;
			_timeGoalPos = 0.5f;//thoi gian check khi co dap coc, dap xa
			_createTimeGoal = Time.time;

		}

    }

	void OnCollisionEnter(Collision other)
	{
		//Debug.Log ("cos collison: " + other.gameObject.name);
		if (other.gameObject.name == "CrossBar")
		{
			isGoalPos = true;
			_timeGoalPos = 0.5f;//thoi gian check khi co dap coc, dap xa
			_createTimeGoal = Time.time;
		}
		else if (other.gameObject.name == "GoalpostLeft")
		{
			isGoalPos = true;
			_timeGoalPos = 0.5f;//thoi gian check khi co dap coc, dap xa
			_createTimeGoal = Time.time;

		}
		else if (other.gameObject.name == "GoalpostRight")
		{
			isGoalPos = true;
			_timeGoalPos = 0.5f;//thoi gian check khi co dap coc, dap xa
			_createTimeGoal = Time.time;

		}
		else if (other.gameObject.name == "GoalKeeperSideToSide")
		{
			isGoalPos = true;
			_timeGoalPos = 0.5f;//thoi gian check khi co dap coc, dap xa
			_createTimeGoal = Time.time;

		}
	}

	void missShot()
	{
		//Debug.Log("sut truot : ");
		handler.missShot();
	}

    void addScore()
    {

      
        // Debug.Log("loai cong diem : " + GameData.Instance().gameMode);
        //Debug.Log("diem hien co : " + GameData.Instance().score);
        if (GameData.Instance().gameMode)
        {
             GameObject clone = (GameObject)Instantiate(original, new Vector3(-18f, 3.6f, 17f), Quaternion.identity);
            GameData.Instance().score = GameData.Instance().score + _score;
        }
        else
        {
            GameObject clone1 = (GameObject)Instantiate(original1, new Vector3(-18f, 3.6f, 17f), Quaternion.identity);
            GameData.Instance().score = GameData.Instance().score + _score * 2;
        }
		//Debug.Log("diem sau cong : " + GameData.Instance().score);
		SoundController sC = GameObject.Find ("SoundController").GetComponent<SoundController> ();
		sC.resultShot (1);
        Destroy(gameObject, 0.5f);
        handler.updateScore();
        //handler.SendMessage("updateScore", SendMessageOptions.DontRequireReceiver);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(50, 50, 200, 100), goal);
    }
}
