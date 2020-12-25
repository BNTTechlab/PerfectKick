
﻿using UnityEngine;
using System.Collections;
using System;
//using Windows.Kinect;

public class GestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{

    // GUI Text to display the gesture messages.
    //public GUIText GestureInfo;
    public static Int64 IdPlayer;

	// GUI Text to display the gesture messages.
	public GUIText GestureInfo;
	

	private bool swipeLeft;
	private bool swipeRight;
	private bool swipeUp;
	private bool swipeDown;

    private bool RaiseLeftHand;
    private bool RaiseRightHand;

    public bool jump;
    public bool squat;
    private bool wave;
    private bool isWaveRecognized = false;
    private Int64 waveUserId;
    public float d;

    public ShootingSceneManager shoot;
    public bool IsJump()
    {
        if (jump)
        {
            jump = false;
            return true;
        }

        return false;
    }
    public bool IsSquat()
    {
        if (squat)
        {
            squat = false;
            return true;
        }

        return false;
    }
    public bool IsWave()
    {
        if (wave)
        {
            wave = false;
            return true;
        }

        return false;
    }
    public bool IsRaiseLeftHand()
    {
        if (RaiseLeftHand)
        {
            RaiseLeftHand = false;
            return true;
        }
        return false;
    }
    public bool IsRaiseRightHand()
    {
        if (RaiseRightHand)
        {
            RaiseRightHand = false;
            return true;
        }
        return false;
    }

	
	public bool IsSwipeLeft()

	{
		if(swipeLeft)
		{
			swipeLeft = false;
			return true;
		}
		
		return false;
	}
	
	public bool IsSwipeRight()
	{
		if(swipeRight)
		{
			swipeRight = false;
			return true;
		}
		
		return false;
	}
	
	public bool IsSwipeUp()
	{
		if(swipeUp)
		{
			swipeUp = false;
			return true;
		}
		
		return false;
	}
	
	public bool IsSwipeDown()
	{
		if(swipeDown)
		{
			swipeDown = false;
			return true;
		}
		
		return false;
	}
	

	public void UserDetected(long userId, int userIndex)
	{
		// detect these user specific gestures
		KinectManager manager = KinectManager.Instance;
		
		manager.DetectGesture(userId, KinectGestures.Gestures.SwipeLeft);
		manager.DetectGesture(userId, KinectGestures.Gestures.SwipeRight);

        manager.DetectGesture(userId, KinectGestures.Gestures.Jump);
        manager.DetectGesture(userId, KinectGestures.Gestures.Squat);
        manager.DetectGesture(userId, KinectGestures.Gestures.Wave);
        manager.DetectGesture(userId, KinectGestures.Gestures.RaiseLeftHand);
        manager.DetectGesture(userId, KinectGestures.Gestures.RaiseRightHand);


        //if(GestureInfo != null)
        //{
        //	GestureInfo.GetComponent<GUIText>().text = "Swipe left or right to change the slides.";
        //}
    }
	

	public void UserLost(long userId, int userIndex)
	{
		if(GestureInfo != null)
		{
			GestureInfo.GetComponent<GUIText>().text = string.Empty;
		}

	}

	public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture, 
	                              float progress, KinectInterop.JointType joint, Vector3 screenPos)
	{
		// don't do anything here
	}

	public bool GestureCompleted (long userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectInterop.JointType joint, Vector3 screenPos)
	{

        KinectManager manager = KinectManager.Instance;

        if (gesture == KinectGestures.Gestures.Jump && waveUserId == userId)
        {
            //  Debug.Log("Nhay"+manager.GetJointPosition(userId, (int)KinectInterop.JointType.Head).y);
            if (manager.GetJointPosition(userId, (int)KinectInterop.JointType.Head).y > shoot.positionHead.y + d)
            {
                jump = true;
            }
        }
            if (gesture == KinectGestures.Gestures.Squat && waveUserId == userId )
        {
            squat = true;
        }
            //if(gesture == KinectGestures.Gestures.Wave)
            //wave = true;
        if (gesture == KinectGestures.Gestures.RaiseLeftHand || gesture == KinectGestures.Gestures.RaiseRightHand && !isWaveRecognized)
        {
            RaiseLeftHand = true;
            RaiseRightHand = true;
            isWaveRecognized = true;
            waveUserId = userId;
            Debug.Log("NGười chơi giơ tay:"+waveUserId);
            IdPlayer = waveUserId;
        }

		string sGestureText = gesture + " detected";
		if(GestureInfo != null)
		{
			GestureInfo.GetComponent<GUIText>().text = sGestureText;
		}
		
		if(gesture == KinectGestures.Gestures.SwipeLeft)
			swipeLeft = true;
		else if(gesture == KinectGestures.Gestures.SwipeRight)
			swipeRight = true;
		else if(gesture == KinectGestures.Gestures.SwipeUp)
			swipeUp = true;
		else if(gesture == KinectGestures.Gestures.SwipeDown)
			swipeDown = true;
		
		return true;

	}

	public bool GestureCancelled (long userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectInterop.JointType joint)
	{
		// don't do anything here, just reset the gesture state
		return true;
	}
	
}
