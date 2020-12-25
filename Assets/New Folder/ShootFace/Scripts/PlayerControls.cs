using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    //Dectect_Kinect
    public bool slideChangeWithGestures = true;
    public bool slideChangeWithKeys = true;
    public List<Texture> slideTextures;
    public List<GameObject> horizontalSides;
    public bool autoChangeAlfterDelay = false;
    private GestureListener gestureListener;
    private WriteJson writeJson;
    //end_Detect

    //health
    public int curHealth;
    public int maxHealth =2;

    public KinectManager kinectManager;
    public Rigidbody2D rb;
    public Animator animation;

    public float horizontal;
    public float speed = 5.5f;
    public float jump;
    public float jumpForce;

    public bool grounded;
    public bool facingRight = true;
    public bool isDead;
    public bool isJump;
    public bool inPipe = false;
    public bool isRun;

    public bool isIdle;

    public bool JumpUp;
    public float x = 270;

    public Camera camPlayer;
    public GameObject gameOver;

    public LayerMask whatIsGround;
    public float groundRadius;
    public Transform groundPoints;
    public float crouchTimeout = 1.0f;
    public List<GameObject> list;

    public HUD hub;
  //  public ScrollBackground scroll;
    public int k = 5;
    public bool checkisKnock=false;

    //sound
    public GameObject soundJump;
    public GameObject soundCrouch;
    public GameObject SoudHienDiem;
    public GameObject soundDoanCuoi;
    public GameObject soundDoanDau;
    public GameObject soundToSchool;
    public GameObject soundGif;

    public GameObject phaoHoa;
    public GameObject original;
    public bool end;

    // Use this for initialization
    void Start()
    {
        x = 270;
        k = 10;
            curHealth = 1;
        list[0].SetActive(false);
        gameOver.SetActive(false);
        StartCoroutine(Example());
        rb = gameObject.GetComponent<Rigidbody2D>();
        animation = gameObject.GetComponent<Animator>();
        StartCoroutine("CrouchChekingCoroutine");
        //Detect
        gestureListener = Camera.main.GetComponent<GestureListener>();
        writeJson = Camera.main.GetComponent<WriteJson>();
    }
    IEnumerator Example()
    {
        yield return new WaitForSeconds(3.5f);
        list[1].SetActive(false);
        animation.SetBool("IsRun", true);
      
        isIdle = true;
    }
    IEnumerator Example1()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Chờ 2 s");
    }
    // Update is called once per frame
    void Update()
    {
        // horizontal = Input.GetAxisRaw("Horizontal");
        //  jump = Input.GetAxisRaw("Jump");
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }


    }

    //vật là isTrigger khi va chạm với collider (Boxcollider ở chân player chạm đất)
    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("MocFinish"))
        {
            soundDoanCuoi.SetActive(true);
            soundDoanDau.SetActive(false);
        }

        
        if (col.CompareTag("BuLy"))
        {
            if (checkisKnock == true) {
                x = 0;
                //     OnDeadEnd();
                StartCoroutine(Wait2());
            }

        }

        // Gap Em gai Bu
        //if (col.CompareTag("BuGai"))
        //{
        //    curHealth -= 1;
        //    if (curHealth > 0)
        //    {
        //        StartCoroutine(Enabled());
        //    }
        //    else
        //    {
        //        animation.SetBool("FallInLove", true);
        //        x = 0;
        //        StartCoroutine(Wait3());
        //    }
        //}

        //Finish
        if (col.CompareTag("Finish"))
        {
            end = true;
            OnDeadEnd();
              x = 0;
            soundToSchool.SetActive(true);
            phaoHoa.SetActive(true);
            
           // gameOver.SetActive(true);
        }

        //CotDien ko di qua duoc
        //if (col.CompareTag("CotDien"))
        //{
        //    curHealth -= 1;
        //    if (curHealth > 0)
        //    {
        //        StartCoroutine(Enabled1());
        //    }
        //    else
        //        animation.SetBool("isDead", true);

        //}
    }
 

    //}
    //public void Continue1()
    //{
    // //   animation.SetBool("isKnock", false);
    //   // x = 270;
    //}
    public void Continue(){
        animation.SetBool("FallInLove", false);
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        animation.SetBool("FallInLove", false);
    }
    IEnumerator Wait2()
    {
        yield return new WaitForSeconds(1.3f);
        OnDeadEnd();
    }
    IEnumerator Wait3()
    {
        yield return new WaitForSeconds(2.7f);
        OnDeadEnd();
    }

    private void FixedUpdate()
    {
 
        Flip();
        if (Physics2D.OverlapCircle(groundPoints.position, groundRadius, whatIsGround) || grounded)
        {
            
            grounded = true;
            isJump = false;
        }
        if (isIdle==true )
        {
            Move();
        }

        animation.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animation.SetBool("Grounded", grounded);
        animation.SetFloat("vSpeed", rb.velocity.y);
        animation.SetBool("isDead", isDead);

        if (Input.GetKey(KeyCode.UpArrow) && grounded)
        {
            isJump = true;
            grounded = false;
            animation.SetBool("Grounded", grounded);
            soundJump.SetActive(true);
            rb.AddForce(Vector2.up * 615);
            rb.AddForce(Vector2.right * 105);
            StartCoroutine(Wait5());

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            animation.SetBool("isCrouch", true);
            soundCrouch.SetActive(true);
            StartCoroutine(Wait5());
        }

        //if (slideChangeWithGestures && gestureListener)
        //{
        //    if (gestureListener.IsJump() && grounded)
        //    {
        //        isJump = true;
        //        grounded = false;
        //        animation.SetBool("Grounded", grounded);
        //        soundJump.SetActive(true);
        //        rb.AddForce(Vector2.up * 620);
        //        rb.AddForce(Vector2.right * 110);
        //        StartCoroutine(Wait5());
        //    }
        //    if (gestureListener.IsSquat())
        //    {
        //        //Debug.Log("Squat!!!");
        //        animation.SetBool("isCrouch", true);
        //        soundCrouch.SetActive(true);
        //        StartCoroutine(Wait5());
        //    }
        //}
  
    }
    IEnumerator Enabled()
    {
        camPlayer.enabled = false;
        yield return new WaitForSeconds(0.08f);
        camPlayer.enabled = true;
        yield return new WaitForSeconds(0.08f);
        camPlayer.enabled = false;
        yield return new WaitForSeconds(0.08f);
        camPlayer.enabled = true;
        yield return new WaitForSeconds(0.08f);
        camPlayer.enabled = false;
        yield return new WaitForSeconds(0.08f);
        camPlayer.enabled = true;
    }
    IEnumerator Enabled1()
    {
        for(int i=0; i <= 15; i++)
        {
            camPlayer.enabled = false;
            yield return new WaitForSeconds(0.08f);
            camPlayer.enabled = true;
            yield return new WaitForSeconds(0.08f);
        }
 
    }
    void Move()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        rb.AddForce(Vector2.right * x);
    }
    void Jump()
    {
        if (grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump * jumpForce);
        }
        else
        {
            rb.AddForce(Vector2.right * 50);
        }
    }
    void Flip()
    {
        if ((horizontal<0 && facingRight==true)||(horizontal>0 && facingRight==false))
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
    public void HurtEnd()
    {
        animation.SetBool("isHurt", false);
        Debug.Log("Hurt End");
    }

    public void OnDeadEnd()
    {
        //GameObject clone = (GameObject)Instantiate(original, new Vector3(this.transform.position.x +2f, this.transform.position.y+2f , this.transform.position.z), Quaternion.identity);
       // Destroy(clone, 2f);
        writeJson.Save();
        x = 0;
        //list[1].SetActive(true);
        StartCoroutine(Wait4());
        gameOver.SetActive(true);
    }
    IEnumerator Wait4()
    {
        SoudHienDiem.SetActive(true);
        yield return new WaitForSeconds(1.5f);
    }
    IEnumerator Wait5()
    {
        yield return new WaitForSeconds(1f);
        soundJump.SetActive(false);
        soundCrouch.SetActive(false);
    }

    IEnumerator CrouchChekingCoroutine()
    {
        while (true)
        {
            if (animation.GetBool("isCrouch"))
            { 
                yield return new WaitForSeconds(1f);
                if (!inPipe)
                {
                    animation.SetBool("isCrouch", false);
                }
            }
            yield return null;
        }
    }

}
