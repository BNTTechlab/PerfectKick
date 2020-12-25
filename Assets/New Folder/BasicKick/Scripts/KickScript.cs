using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class KickScript : MonoBehaviour
{

    //static variable

    public List<GameObject> list1;
    //public variable
    public KinectManager2 k2;
	public float h;
	public int posBall; //-1 là bóng ở bên trái, 0 là chính giữa, 1 là bên phải
	public bool isAllow; //cho phep choi
	//private variable

	GameObject rightFoot;//mũi chân phải
	GameObject leftFoot;//mũi chân trái
	GameObject rightAnkle;//gót chân phải
	GameObject leftAnkle;//gót chân trái
	GameObject head;
	GameObject hip;//hông phải
	GameObject hip2;//hông trái
	GameObject ball;
   
	bool check = false;
	bool check1 = false;
	float posOfFoot; //khoảng cách chân phải tới khung thành
	//
	float posOfFoot2;//khoảng cách chân trái tới khung thành
	Vector3 differenceOfFoot;
	Vector3 angleOfKick; //góc di chuyển chân so với hip để cho thủ môn di chuyển theo

	float _posFootX; //khoảng cách x khi bắt đầu vung chân cho đến chạm bóng-->tính góc đặt lòng, úp mu

	bool _isFoot;//đang đá chân nào --> true là chân phải
	bool _isLegUp; //check đang trong quá trình giờ chân

	float time;
	float timeTaken;
	float maxDistanceofKick;

	//số liệu khi sút chân phải
	List<float> timeList;
	List<float> footPos;
	List<float> angleDetect;

	//số liệu khi sút chân trái
	List<float> timeList2;
	List<float> footPos2;
	List<float> angleDetect2;

	GameObject sH;

	void Start()
	{
        list1[0].SetActive(true);
        KinectManager2 k2 = gameObject.GetComponent<KinectManager2>();
       
		sH = GameObject.Find ("ScriptHandler");
		//lấy các phần cơ thể
		leftFoot = GameObject.Find("15_Foot_Left");//mũi chân phải (trong kinect là chân trái)
		rightFoot = GameObject.Find("19_Foot_Right");
		leftAnkle = GameObject.Find("14_Ankle_Left");//gót chân phải (trong kinect là chân trái)
		rightAnkle = GameObject.Find("18_Ankle_Right");

		hip = GameObject.Find("12_Hip_Left");
		hip2 = GameObject.Find("16_Hip_Right");
		head = GameObject.Find("03_Head");
		time = 0;
		timeTaken = 0;
		maxDistanceofKick = 0;

		footPos = new List<float>();
		timeList = new List<float>();
		angleDetect = new List<float>();
		//
		footPos2 = new List<float>();
		timeList2 = new List<float>();
		angleDetect2 = new List<float>();

		posBall = 0;

		_posFootX = 0;

		_isLegUp = false;

		isAllow = true;
	}

	public void ResetKick()
	{
		//posBall++;
		check = false;
		check1 = false;
		if(posBall == 2)
		{
			posBall = -1;
		}
		//Debug.Log("reset trong kick: " + posBall);
		time = 0;
		timeTaken = 0;
		maxDistanceofKick = 0;

		footPos = new List<float>();
		timeList = new List<float>();
		angleDetect = new List<float>();
		//
		footPos2 = new List<float>();
		timeList2 = new List<float>();
		angleDetect2 = new List<float>();
		isAllow = true;
		_posFootX = 0;

		_isLegUp = false;

	}

	void FixedUpdate()
	{
        //tính tọa độ chân, hông so với khung thành
        if (leftFoot == null || rightFoot == null)
        {
            SceneManager.LoadScene("MiniGame");
        }
        posOfFoot2 = Mathf.Floor((rightFoot.transform.position.z - hip2.transform.position.z) * 100) / 100;
        posOfFoot = Mathf.Floor((leftFoot.transform.position.z - hip.transform.position.z) * 100) / 100;
        float Rdis = leftFoot.transform.position.y - rightFoot.transform.position.y;
        float Ldis = rightFoot.transform.position.y - leftFoot.transform.position.y;
        bool rightKick = false;
        bool leftKick = false;
        if (Rdis>=0.2)
        {
            rightKick = true;
        }
        if (Ldis>0.2f)
        {
            leftKick = true;
        }

        //nhận ra chân phải sút
        if ( rightKick  ) {
            if (isAllow)
            {
                //add tọa độ chân trái (vì chân ngược khi qua kinect)
                angleDetect.Add(leftFoot.transform.position.y);
                //Debug.Log ("check độ lớn o fixupdate: " + angleDetect.Count);
                DetectKick();
                time = 0;
            }
		}
        if (leftKick)
        {
			
			if ( isAllow) {

				//Debug.Log ("chan len hay xuong left " + _isLegUp);
				angleDetect2.Add (rightFoot.transform.position.y);
				DetectKick2 ();
				time = 0;
			}
		}

	}

	void DetectKick()
	{
		_isFoot = true;
		time = time + Time.deltaTime;
		footPos.Add(posOfFoot);
		timeList.Add(time);
		//nếu khoảng cách chân phải so với hông
		Debug.Log ("check khoang cach: " + posOfFoot);
		if (posOfFoot < -0.27f ) //-0.3f
		{
			// Debug.Log("check pos : " + leftFoot.transform.position + "--" + hip.transform.position);
			//check gio chan len chua neu chua thi chuyen trang thai dang gio chan
			//check da gio chan len chua, neu gio roi thi goi ham tinh toan viec sut bong

			_isLegUp = true;
			if (_isLegUp)
			{
				angleDetect.Add(leftFoot.transform.position.y);
				angleOfKick = (leftFoot.transform.position - hip.transform.position);
				//foreach (Vector3 a in angleDetect)
				//    Debug.Log(a);
				angleOfKick.Normalize();
                h = leftFoot.transform.position.y - rightFoot.transform.position.y;
                angleDetect.Sort();

				_isLegUp = false;
				KickParameters();
			}

		}
	}
	//
	void DetectKick2()
	{
		_isFoot = false;
		time = time + Time.deltaTime;
		footPos.Add(posOfFoot2);
		timeList.Add(time);

		if (posOfFoot2 < -0.27f ) //-0.3f
		{
			//check gio chan len chua neu chua thi chuyen trang thai dang gio chan
			//check da gio chan len chua, neu gio roi thi goi ham tinh toan viec sut bong
			//Debug.Log ("da gio chan trai hay chua: " + _isLegUp);

			_isLegUp = true;
			if (_isLegUp)
			{
				angleDetect2.Add(rightFoot.transform.position.y);
				angleOfKick = (rightFoot.transform.position - hip2.transform.position);
				//Debug.Log(angle);
				//foreach (Vector3 a in angleDetect)
				//    Debug.Log(a);
				angleOfKick.Normalize();
				angleDetect2.Sort ();

				_isLegUp = false;
				KickParameters();
			}

		}
	}


	void KickParameters()
	{
		float maxTime = 0;
		float minTime = 0;
		float maxDistance = 0;
		float minDistance = 0;

		float initialVelocity = 0;
		float finalVelocity = 0;

		foreach (float t in timeList)
		{
			maxTime = Mathf.Max(maxTime, t);
			minTime = Mathf.Min(minTime, t);
		}
		foreach (float d in footPos)
		{
			maxDistance = Mathf.Max(maxDistance, d);
			minDistance = Mathf.Min(minDistance, d);
		}

		//thời gian diễn ra hành động sút
		//nếu bé là sút mạnh v ngược lại 
		timeTaken = maxTime - minTime;

		//quãng đường di chuyển của chân từ vị trí co lên cao nhất đến khi chạm bóng
		maxDistanceofKick = maxDistance - minDistance;
		initialVelocity = (maxDistance / 2) / (maxTime / 2);
		//Debug.Log("time gio chan: " + timeTaken);
		if(!isAllow)
		{
			return;
		}
		//maxDis càng lớn khả năng sút càng mạnh
		//time di chuyển càng bé thì khả năng sút càng mạnh
		//_posFootX : đặt lòng: nhẹ hoặc mạnh tùy thuộc vào maxDis, time, góc chính xác theo hướng _posX, _posY
		//				úp mu giữa: Mạnh, góc ngược với hướng _posX, _posY

		// góc bóng sẽ bay đến
		//truc x
		float angleX;

		//truc y
		float angleY;
		float deg;
        //check sut chan nào
        if (_isFoot)
        {
            //deg = Vector2.Angle(leftFoot.transform.position, hip.transform.position);
            angleX = leftFoot.transform.position.x - hip.transform.position.x;
            angleY = leftFoot.transform.position.y - rightFoot.transform.position.y;
            //         //check khoảng cách cổ chân và gót chân để biết bóng bay theo hướng nào
            //         if (leftFoot.transform.position.x - hip.transform.position.x < -0.1f)
            //{
            //	_posFootX = Random.Range(0.1f, 0.5f);
            //}
            //else
            //{
            //	_posFootX = 0;
            //}
            ////Debug.Log("goc cong them ben phai : " + _posFootX);
            ////góc bay của bóng = vị trí bóng * 0.5 +  hướng * góc giữa mũi và gót
            ////0.5 có thể thay đổi tùy thuộc vào vị trí 2 bên 
            //angleX = posBall * -.5f + deg - _posFootX; 
            //float maxY = angleDetect [angleDetect.Count - 1];
            //float minY = angleDetect [0];
            ////Debug.Log("khoang cach chan theo y : " + maxY + "--" + minY);

            ////neu khi nao check y la 0.5 len cham xa thi bo di -0.3f
            //_posFootX = 0.3f + Random.Range(0.1f, 0.3f);
            //if (_posFootX < 0.1f) {
            //	_posFootX = 0.1f;
            //}
            ////Debug.Log("ti le gio chan  : " + _posFootX);

            ////góc y: bóng bổng hay sệt sẽ theo random
            //angleY = _posFootX;

            //van tốc
            finalVelocity = maxDistanceofKick / timeTaken;

            float[] kickParams = new float[5];
            kickParams[0] = timeTaken;
            kickParams[1] = angleX;//goc x sut sang 2 ben -0.5 -- 0.5
            kickParams[2] = angleY; // goc y sut bong hoac set 0.1 -- 0.2
            kickParams[3] = 1; //goc z sut phia truoc hoac sau
            kickParams[4] = finalVelocity;

   
            if (!check)
            {
				check = true;
				isAllow = false;
         		sH.GetComponent<ScriptHandler> ().ballMove (kickParams, true);
               // GameObject.Find("Ball").SendMessage("KickForce", kickParams, SendMessageOptions.DontRequireReceiver);
               
            }
                

        }
		else
		{

            //deg = Vector2.Angle (rightFoot.transform.position, hip2.transform.position);
            angleX = rightFoot.transform.position.x - hip2.transform.position.x;
            angleY = rightFoot.transform.position.y - leftFoot.transform.position.y;
            //  Debug.Log("goc lech chan trai : " + angleX);
            //	if (rightFoot.transform.position.x - hip2.transform.position.x < -0.1f)
            //	{
            //		_posFootX = Random.Range(0.1f, 0.5f);
            //	}
            //	else
            //	{
            //		_posFootX = 0;
            //	}

            ////	Debug.Log("kc cong them trasi : " + _posFootX);
            //	angleX = posBall * -.5f + deg + _posFootX;

            //	float maxY = angleDetect2 [angleDetect2.Count - 1];
            //	float minY = angleDetect2 [0];
            ////	Debug.Log("khoang cach chan theo y : " + maxY + "--" + minY);
            //	_posFootX = 0.3f + Random.Range(0.1f, 0.3f);
            //	//Debug.Log("ti le gio chan  : " + _posFootX);
            //	if (_posFootX < 0.1f) {
            //		_posFootX = 0.1f;
            //	}

            //	//góc y: bóng bổng hay sệt sẽ theo random
            //	angleY = _posFootX;

            //van tốc
            finalVelocity = maxDistanceofKick / timeTaken;

            float[] kickParams = new float[5];
            kickParams[0] = timeTaken;
            kickParams[1] = angleX;//goc x sut sang 2 ben -0.5 -- 0.5
            kickParams[2] = angleY; // goc y sut bong hoac set 0.1 -- 0.2
            kickParams[3] = 1; //goc z sut phia truoc hoac sau
            kickParams[4] = finalVelocity;

            

            if (!check1)
            {
				check1 = true;
				isAllow = false;
				Debug.Log ("go bong bay chan trai");
               	sH.GetComponent<ScriptHandler> ().ballMove (kickParams, false);
                // GameObject.Find("Ball").SendMessage("KickForce2", kickParams, SendMessageOptions.DontRequireReceiver);
                
            }
            

        }

		//can giai quyet: sut vao phía nào quả bóng: bên trái, phải, chính giữa

		//chỉ cần truyền 2 biến: vận tốc tạo cho bóng

	}
}