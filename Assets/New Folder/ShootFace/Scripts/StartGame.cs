using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {
    //Dectect_Kinect
    public bool slideChangeWithGestures = true;
    public bool slideChangeWithKeys = true;
    public List<Texture> slideTextures;
    public List<GameObject> horizontalSides;
    public bool autoChangeAlfterDelay = false;
    private GestureListener gestureListener;
    //end_Detect

    public List<GameObject> list;
    public KinectManager kinectManager;
    // Use this for initialization
    void Start () {
        gestureListener = Camera.main.GetComponent<GestureListener>();
    }
    private void FixedUpdate()
    {
        if (slideChangeWithGestures && gestureListener)
        {
            if (gestureListener.IsRaiseLeftHand() || gestureListener.IsRaiseRightHand())
            {
                //Debug.Log(kinectManager)
                list[0].SetActive(true);
                //Application.LoadLevel(1);
               Debug.Log("RaiseYourHand!!!");

            }
        }
    }
    void OnGUI()
    {

    }
}
