using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {

	//public variable
	public GameObject target;

	//private variable
	private float _timeCreate;
	private float _time;
	private int _numTarget;//so luong target moi lan tao
	private bool _isCreate;
    ArrayList _arrTarget;
	// Use this for initialization
	void Start () {
        _arrTarget = new ArrayList();
        Debug.Log("khoi tao: ");
        createTarget ();
	}

	void createTarget()
	{
		_isCreate = false;
		if (_numTarget > 0) {
		} else {
			_numTarget = Random.Range (2, 4);
			ArrayList arrPos = new ArrayList();
			arrPos.Add (0);
			arrPos.Add (1);
			arrPos.Add (2);

		//	Debug.Log ("tao ra may thang: " + _numTarget);
			for (int i = 0; i < _numTarget; i++) {
				float posY = Random.Range (0.1f, 1.2f);
				int posX = Random.Range (0, arrPos.Count);
				int valueX = (int)arrPos [posX];
				arrPos.RemoveAt (posX);
				//Debug.Log ("tao ra hu nao: " + posY + "--" + valueX);
				GameObject tg = Instantiate(target, new Vector3(transform.position.x + 2.2f * valueX, transform.position.y - posY, transform.position.z - 4.5f), Quaternion.identity) as GameObject;
				tg.name = "target" + i.ToString();
				tg.GetComponent<Target> ().updateInfo (gameObject, valueX);
                _arrTarget.Add(tg);
			}
		}

	}

	public void destroyTarget (string na)
	{
        _numTarget--;
        for (int i = 0; i < _arrTarget.Count; i++)
        {
            GameObject go = (GameObject)_arrTarget[i];
            if (na == go.name)
            {
                _arrTarget.RemoveAt(i);
                break;
            }
        }

        if (_numTarget <= 0)
        {
            _timeCreate = Time.time;
            _time = 1.0f;
            _isCreate = true;

        }
    }

	// Update is called once per frame
	void Update () {
		if (_isCreate) {
			if (Time.time > _time + _timeCreate) {
				
				createTarget ();
			}
		}
        if (GameData.Instance().timePlay == 0)
        {
            for (int i = 0; i < _arrTarget.Count; i++)
            {
                GameObject go = (GameObject)_arrTarget[i];
                _arrTarget.RemoveAt(i);
                Destroy(go, 0.1f);
                i--;
            }
        }
    }
}
