using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour {

	//hinh anh cac object can co
	public GameObject comboTitle;
	public GameObject x1;
	public GameObject x2;
	public GameObject x3;
	public GameObject x4;
	public GameObject x5;

	private float _createTime; //thoi gian ton tai
	private float _startTime;//thoi gian bat dau add

	private GameObject _title;//anh combo
	private GameObject _numX;//anh cac lan dc combo: 1, 2, 3...
	private bool _isCreate;
	// Use this for initialization
	void Start () {
		_isCreate = false;
		_createTime = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		if (_isCreate) {
			if (Time.time > _createTime + _startTime) {
				Destroy (_title, 0.1f);
				Destroy (_numX, 0.1f);
				_isCreate = false;
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

	public void createImage()
	{
		_startTime = Time.time;
		_isCreate = true;
		if (_title != null) {
			Destroy (_title);
		}
		if (_numX != null) {
			Destroy (_numX);
		}
		_title = Instantiate (comboTitle, new Vector3 (transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
		if (GameData.Instance ().combo == 1) {
			_numX = Instantiate (x1, new Vector3 (transform.position.x, transform.position.y - 0.57f, transform.position.z), Quaternion.identity);
		} else if (GameData.Instance ().combo == 2) {
			_numX = Instantiate (x2, new Vector3 (transform.position.x, transform.position.y - 0.57f, transform.position.z), Quaternion.identity);
		} else if (GameData.Instance ().combo == 3) {
			_numX = Instantiate (x3, new Vector3 (transform.position.x, transform.position.y - 0.57f, transform.position.z), Quaternion.identity);
		} else if (GameData.Instance ().combo == 4) {
			_numX = Instantiate (x4, new Vector3 (transform.position.x, transform.position.y - 0.57f, transform.position.z), Quaternion.identity);
		} else if (GameData.Instance ().combo == 5) {
			_numX = Instantiate (x5, new Vector3 (transform.position.x, transform.position.y - 0.57f, transform.position.z), Quaternion.identity);
		}
	}
}
