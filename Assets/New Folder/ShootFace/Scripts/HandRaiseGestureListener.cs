using UnityEngine;
using System.Collections;
using System;
//using Windows.Kinect;

public class HandRaiseGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
	// GUI Text to display the gesture messages.
	public GUIText GestureInfo;
	
	private bool swipeLeft;
	private bool swipeRight;
	private bool swipeUp;
	private bool swipeDown;
	private bool handRaise;

	private long handRaiseUserId;
    private bool isWaveRecognized = false;
    private Int64 waveUserId;
    public static Int64 IdPlayer;

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

	public bool IsHandRaise()
	{
		if (handRaise)
		{
			handRaise = false;
			return true;
		}

		return false;
	}

	public long GetHandRaiseUserId()
	{
		return handRaiseUserId;
	}
	

	public void UserDetected(long userId, int userIndex)
	{
		// detect these user specific gestures
		KinectManager manager = KinectManager.Instance;
		
		manager.DetectGesture(userId, KinectGestures.Gestures.RaiseLeftHand);
		manager.DetectGesture(userId, KinectGestures.Gestures.RaiseRightHand);

		
		if(GestureInfo != null)
		{
			GestureInfo.GetComponent<GUIText>().text = "Swipe left or right to change the slides.";
		}
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
		else if(gesture == KinectGestures.Gestures.RaiseLeftHand || gesture == KinectGestures.Gestures.RaiseRightHand)
		{
			handRaise = true;
			handRaiseUserId = userId;
            isWaveRecognized = true;
            waveUserId = userId;
            Debug.Log("NGười chơi giơ tay:" + waveUserId);
            IdPlayer = waveUserId;
        }
		
		return true;
	}

	public bool GestureCancelled (long userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectInterop.JointType joint)
	{
		// don't do anything here, just reset the gesture state
		return true;
	}
	
}
