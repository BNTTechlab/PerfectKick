using UnityEngine;
using System.Collections;

public class GoalieAI : MonoBehaviour
{
    public BallScript ball;
    public GameObject target;
    float speed = 20;
    public GameObject temp;
    GameObject goalie;
    Vector3 goaliePos;
    Vector3 _backVector;
    bool _isFly;
    bool _isBack;
    float delta;
    float _angle;
    //lấy độ lệch của chân theo chiều ngang - trục y
    float yaxis;


    float _backTime; // thoi gian tiep theo se tao bomb
    float _lastBackTime;// lan cuoi cung tao bomb la luc nao
    bool _isTouchRight; //cham thanh phai
    bool _isTouchLeft;//cham thanh trai

    void Start()
    {

        BallScript ball = gameObject.GetComponent<BallScript>();
        goalie = GameObject.Find("GoalKeeperSideToSide");
        goaliePos = goalie.transform.position;
        speed = 0.07f;
        _isFly = false;
        //force = 0;
        // Target target = gameObject.GetComponent<Target>();
    }
    private void FixedUpdate()
    {
        //Debug.Log("co duoc bay: "+ transform.position.x);
        if (_isFly)
        {

            goalMove();
        }
        if (_isBack)
        {

            goalMove();
        }

        if (Time.time > _lastBackTime + _backTime)
        {
            _backTime = 3000000;
            backToCenter();

        }
        //  
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "connerRightCollider")
        {
            _isTouchRight = true;
        }
        else if (col.gameObject.name == "connerLeftCollider")
        {
            _isTouchLeft = true;
        }
    }

    public void updateBackTime()
    {

        _backTime = 1.4f;
        _lastBackTime = Time.time;
    }

    public void updateAngle(float ag, float deltaY)
    {
        _angle = ag;
        yaxis = deltaY;
    }

    // Update is called once per frame
    public void Jump()
    {//(GameObject ta) {
        if (!_isFly)
        {
            _isFly = true;
            _isBack = false;
            _backTime = 3000000;
            speed = 1.4f;
            delta = speed * Time.deltaTime;
            //target = ta;
            _backVector = new Vector3(target.transform.position.x, -0.04f, 16.5f);
            float radius = _backVector.x - this.transform.position.x;
           // Debug.Log(radius + "\t" + target.transform.position.x + "\t" + this.transform.position.x);
            transform.GetComponent<Animation>().Stop();

            if (_isTouchRight)
            {
                _backVector = new Vector3(-14.6f, -0.04f, 16.5f);
                if (_angle < 0.15f)
                {
                    transform.GetComponent<Animation>().Play("playerDiveRightLow");
                }
                else
                {
                 transform.GetComponent<Animation>().Play("playerDiveRight");
                }
                _isTouchRight = false;
                return;
            }
            if (_isTouchLeft)
            {
                _backVector = new Vector3(-14.6f, -0.04f, 16.5f);
                if (_angle < 0.15f)
                {
                     transform.GetComponent<Animation>().Play("playerDiveLeftLow");
                }
                else
                {
                    transform.GetComponent<Animation>().Play("playerDiveLeftHigh");
                }
                _isTouchLeft = false;
                return;
            }

            //if (Mathf.Abs(radius) <= 5.5f)
            //{

            //    //    //temp.transform.position = new Vector3(target.transform.position.x, 1.5f, 16f);
            //    if (radius > 0.3f)
            if (yaxis>0)

            {

                    if (_angle < 0.15f)

                    {
                         transform.GetComponent<Animation>().Play("playerDiveLeftLow");
                    }
                    else
                    {
                        transform.GetComponent<Animation>().Play("playerDiveLeftHigh");
                    }
            }
            else
            {
                    if (_angle < 0.15f)
                    {
                        transform.GetComponent<Animation>().Play("playerDiveRightLow");
                    }
                    else
                    {
                        transform.GetComponent<Animation>().Play("playerDiveRight");
                    }
                    //    int rd = Random.Range(0, 3);
            }
    //    else
    //    {
    //        //Debug.Log ("thu mon chi di chuyen");
    //        transform.GetComponent<Animation>().Play("Move_Sideways");

    //    }
        }
    }

    void backToCenter()
    {
        _isTouchLeft = false;
        _isTouchRight = false;
        _isBack = true;
        speed = 1.7f;
        delta = speed * Time.deltaTime;
        _backVector = new Vector3(-14.6f, -0.04f, 16.5f);
        transform.GetComponent<Animation>().Play("Move_Sideways");
    }

    void goalMove()
    {
        Vector3 vt;
        Vector3 direct;
        float radius;
        if (_isBack)
        {
            radius = _backVector.x - this.transform.position.x;
            direct = _backVector - this.transform.position;
            if (direct.magnitude > delta)
            {
                vt = new Vector3(transform.position.x + direct.x * delta, -0.04f, 16.5f);
                this.transform.position = vt;
            }
            else
            {
                vt = new Vector3(_backVector.x, -0.04f, 16.5f);
                this.transform.position = vt;
            }
        }
        else
        {

            radius = _backVector.x - this.transform.position.x;
            direct = _backVector - this.transform.position;
            if (radius > 0.3f)
            {

                if (direct.magnitude > delta)
                {
                    vt = new Vector3(transform.position.x + direct.x * delta, -0.04f, 16.5f);
                    this.transform.position = vt;
                }
                else
                {
                    vt = new Vector3(_backVector.x, -0.04f, 16.5f);
                    this.transform.position = vt;
                }

                //vt = new Vector3 (this.transform.position.x + speed, this.transform.position.y, this.transform.position.z);
                //this.transform.position = vt;
            }
            else
            {
                if (direct.magnitude > delta)
                {
                    vt = new Vector3(transform.position.x + direct.x * delta, -0.04f, 16.5f);
                    this.transform.position = vt;
                }
                else
                {
                    vt = new Vector3(target.transform.position.x, -0.04f, 16.5f);
                    this.transform.position = vt;
                }
                //vt = new Vector3 (this.transform.position.x - speed, this.transform.position.y, this.transform.position.z);
                //this.transform.position = vt;
            }
        }


        //Debug.Log ("toa do cua thu mon: " + transform.position + "---" + delta);
    }

    public void runComplete()
    {
        //temp.transform.position = new Vector3(this.transform.position.x, 1.5f, 16f);
        //target.transform.position = new Vector3(this.transform.position.x, 1.5f, 16f);

        /*if (target.transform.position.x == this.transform.position.x) {
			transform.GetComponent<Animation>().Play("playerIdle");
		} else {
			transform.GetComponent<Animation>().Play("Move_Sideways");
		}*/
        //
        //	GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        _isFly = false;
    }

    //void reset()
    //{
    //    goalie.transform.position = goaliePos;
    //}

}
