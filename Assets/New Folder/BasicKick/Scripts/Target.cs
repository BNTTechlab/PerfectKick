using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	//public variable
	public int score = 0;//diem so cua cai target nay

	//private variable
	float _speed = 1;//toc do di chuyen
	bool _isMove;//cho phep di chuyen
	GameObject _parent;
	bool _isLeft;//di chuyen sang trai hay phai

    public GameObject original;
    public GameObject original2;
    public GameObject original3;
    public GameObject original4;

    /** 0: be nhat -->scale 0.4;
	 * 1: --> 0.5
	 * 2: -->0.6
	 * **/
    int _typeScale = 0;

	ScriptHandler handler;

	// Use this for initialization
	void Start () {
		_isMove = false;
		handler = GameObject.Find("ScriptHandler").GetComponent<ScriptHandler>();
	}

	/** 0: be nhat -->scale 0.4;
	 * 1: --> 0.5
	 * 2: -->0.6
	 * **/
	public void updateInfo(GameObject pa, int val)
	{
		_parent = pa;
		int type = Random.Range (0, 3);
		_typeScale = type;
		_speed = 0.3f;

		if (val == 0) {
			_isLeft = false;
		} else if (val == 1) {
			if (type == 0) {
				_isLeft = false;
			} else {
				_isLeft = true;
			}

		} else if (val == 2) {
			_isLeft = true;
		}

		if (type == 0) {
			score = 50;
			gameObject.transform.localScale = new Vector3 (0.4f, 0.4f, 0.4f);
		} else if (type == 1) {
			score = 50;
			gameObject.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		} else if (type == 2) {
			score = 50;
			gameObject.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);
		}
	}

	void targetMove()
	{
		_isMove = true;
	}

	void shoot()
	{
		_isMove = false;
		Destroy (gameObject, 0.1f);
		_parent.GetComponent<GoalController> ().destroyTarget (this.name);
	}
	
	// Update is called once per frame
	void Update () {
		if (_isMove) {
			
			if (_isLeft) {
				gameObject.transform.position = new Vector3 (gameObject.transform.position.x - _speed * Time.deltaTime, 
															gameObject.transform.position.y, gameObject.transform.position.z);
			} else {
				gameObject.transform.position = new Vector3 (gameObject.transform.position.x + _speed * Time.deltaTime, 
															gameObject.transform.position.y, gameObject.transform.position.z);
			}

		}

		if (!_isMove) {
			if (GameData.Instance ().timePlay <= 20) {
				targetMove ();
			}
		}

		if (GameData.Instance ().timePlay <= 0) {
			Destroy (gameObject);
		}
	}


	void OnTriggerEnter(Collider other)
	{
		
		if (other.GetComponent<Collider>().tag == "Ball")
		{
            if (GameData.Instance().gameMode)
            {
                GameObject clone3 = (GameObject)Instantiate(original3, new Vector3(-18f, 3.6f, 17f), Quaternion.identity);
            }
            else
            {
                GameObject clone4 = (GameObject)Instantiate(original4, new Vector3(-18f, 3.6f, 17f), Quaternion.identity);
            }
           

            //hieu ung trung Target x Combo tq
            GameObject clone = (GameObject)Instantiate(original, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
            Destroy(clone, 0.5f);

            if (!GameData.Instance().gameMode)
            {
                GameObject clone2 = (GameObject)Instantiate(original2, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
            }
            //tq
            addScore();
			shoot ();
			SoundController sC = GameObject.Find ("SoundController").GetComponent<SoundController> ();
			sC.resultShot (2);
			Destroy (other, 0.5f);
			//Debug.Log("diem nguoi choi khi da trung target : " + GameData.Instance().score);
		} else if (other.GetComponent<Collider>().name == "connerRightCollider")
		{
			_isLeft = true;
		} else if (other.GetComponent<Collider>().name == "connerLeftCollider")
		{
			_isLeft = false;
		}


	}


	void addScore ()
	{
        if (GameData.Instance().gameMode)
        {
            GameData.Instance().score = GameData.Instance().score + score;
        }
        else
        {
            GameData.Instance().score = GameData.Instance().score + score * 2;
        }

        ////tq
        //GameData.Instance().score = GameData.Instance().score + score * 2;

        ////tq

        handler.updateScore();
		//handler.SendMessage("updateScore", SendMessageOptions.DontRequireReceiver);
	}


}
