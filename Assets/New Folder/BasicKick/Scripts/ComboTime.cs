using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboTime : MonoBehaviour {
	//hinh anh cac object can co
	public GameObject comboTitle;
	public GameObject x1;
	public GameObject x2;
	public GameObject x3;
	public GameObject x4;
	public GameObject x5;
	public GameObject x10;
	public GameObject x6;
	public GameObject x7;
	public GameObject x8;
	public GameObject x9;

	private float _createTime; //thoi gian ton tai
	private float _startTime;//thoi gian bat dau add
	private int _time;
	private GameObject _title;//anh combo
	private GameObject _numX;//anh cac lan dc combo: 1, 2, 3...
	private bool _isCreate;
	// Use this for initialization
	void Start () {
		_isCreate = false;
		_createTime = 0.5f;
		_time = 0;
	}

	void Update()
	{
		if (_isCreate) {
			if (_time != GameData.Instance ().comboTime) {
				createImage ();
			}
			if (GameData.Instance ().comboTime == 0) {
				destroyAll ();
			}
		}
        if (GameData.Instance().timePlay <= 0)
        {
            if (_title != null)
            {
                Destroy(_title, 0.1f);
                Destroy(_numX, 0.1f);
            }

        }
    }

	void destroyAll()
	{
		_isCreate = false;
		Destroy (_title, 0.1f);
		Destroy (_numX, 0.1f);
	}

	public void createImage()
	{
		_startTime = Time.time;
		_isCreate = true;
		_time = GameData.Instance ().comboTime;
		if (_title == null) {
			_title = Instantiate (comboTitle, new Vector3 (transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

		}
		if (_numX != null) {
			Destroy (_numX);
		}

		if (GameData.Instance ().comboTime == 1) {
			_numX = Instantiate (x1, new Vector3 (transform.position.x - 0.1f, transform.position.y - 0.41f, transform.position.z), Quaternion.identity);
		} else if (GameData.Instance ().comboTime == 2) {
			_numX = Instantiate (x2, new Vector3 (transform.position.x - 0.1f, transform.position.y - 0.41f, transform.position.z), Quaternion.identity);
		} else if (GameData.Instance ().comboTime == 3) {
			_numX = Instantiate (x3, new Vector3 (transform.position.x - 0.1f, transform.position.y - 0.41f, transform.position.z), Quaternion.identity);
		} else if (GameData.Instance ().comboTime == 4) {
			_numX = Instantiate (x4, new Vector3 (transform.position.x - 0.1f, transform.position.y - 0.41f, transform.position.z), Quaternion.identity);
		} else if (GameData.Instance ().comboTime == 5) {
			_numX = Instantiate (x5, new Vector3 (transform.position.x - 0.1f, transform.position.y - 0.41f, transform.position.z), Quaternion.identity);
		} else if (GameData.Instance ().comboTime == 6) {
			_numX = Instantiate (x6, new Vector3 (transform.position.x - 0.1f, transform.position.y - 0.41f, transform.position.z), Quaternion.identity);
		} else if (GameData.Instance ().comboTime == 7) {
			_numX = Instantiate (x7, new Vector3 (transform.position.x - 0.1f, transform.position.y - 0.41f, transform.position.z), Quaternion.identity);
		} else if (GameData.Instance ().comboTime == 8) {
			_numX = Instantiate (x8, new Vector3 (transform.position.x - 0.1f, transform.position.y - 0.41f, transform.position.z), Quaternion.identity);
		} else if (GameData.Instance ().comboTime == 9) {
			_numX = Instantiate (x9, new Vector3 (transform.position.x - 0.1f, transform.position.y - 0.41f, transform.position.z), Quaternion.identity);
		} else if (GameData.Instance ().comboTime == 10) {
			_numX = Instantiate (x10, new Vector3 (transform.position.x - 0.1f, transform.position.y - 0.41f, transform.position.z), Quaternion.identity);
		}

	}
}
