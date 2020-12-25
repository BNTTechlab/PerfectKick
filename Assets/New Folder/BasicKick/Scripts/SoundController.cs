using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
	//public
	public AudioClip startGameSound;//start game
	public AudioClip shotSound;//sut bogn
	public AudioClip shotSpeSound;//sut bong khi dang duoc combo
	public AudioClip shotExSound;//sut vao goal
	public AudioClip shotExTargetSound;//sut trung target = tieng vao target + tieng sut vao gon
	public AudioClip shotExTargetSpeSound;//sut trung target = tieng vao target khi dc combo + tieng sut vao gon

	public AudioClip shotMissSound;//sut truot
	public AudioClip timeupSound;//het gio
	public AudioClip bgSound;//sound bt
	public AudioClip bgSoundSpe;//sound dac biet khi duoc combo
	//public AudioClip enoughComboSound;//khi sut du 10 lan
	public AudioClip countDownSound;//con 10s dem nguoc

	public AudioClip bgSoundScene1;//nhac nen dung cho scene1, 3
	public AudioClip poseComplete;//chup anh xong
	public AudioClip playComplete;//choi xong

	//private
	private AudioSource sourceSoundBg;
	private AudioSource sourceSound;
	private AudioSource sourceSoundEffect;
	private AudioSource sourceSoundEffect1;

	private GameObject sound;
	private GameObject soundEffect;
	private GameObject soundEffect1;
	// Use this for initialization
	void Start () {
		sound = GameObject.Find ("asNormalSound");
		sourceSound = sound.GetComponent<AudioSource> ();
		sourceSound.loop = false;

		sourceSoundBg = gameObject.GetComponent<AudioSource> ();
		sourceSoundBg.loop = true;

		soundEffect = GameObject.Find ("asEffectSound");
		sourceSoundEffect = soundEffect.GetComponent<AudioSource> ();
		sourceSoundEffect.loop = false;

		soundEffect1 = GameObject.Find ("asEffect1Sound");
		sourceSoundEffect1 = soundEffect1.GetComponent<AudioSource> ();
		sourceSoundEffect1.loop = false;

		playSoundScene1 ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void playSoundStartCombo()
	{
		Debug.Log ("sut dc 10 lan: " );
		//sourceSoundEffect.clip = enoughComboSound;
		//sourceSoundEffect.Play ();
	}

	public void playSoundWin()
	{
		Debug.Log ("thang cuoc: " );
		sourceSoundEffect.clip = playComplete;
		sourceSoundEffect.Play ();
	}

    public void playSoundKinect()
    {
        Debug.Log("thang cuoc: ");
        sourceSoundEffect.clip = poseComplete;
        sourceSoundEffect.Play();
    }
    public void playSoundCountDown()
	{
		Debug.Log ("con 10s: " + GameData.Instance ().timePlay);
		sourceSoundEffect.clip = countDownSound;
		sourceSoundEffect.Play ();
	}

	void playSoundScene1() {
		sourceSoundBg.clip = bgSoundScene1;
		sourceSoundBg.Play ();
	}

    public void stopSoundBg() {
		Debug.Log ("dung sound bg: ");
		sourceSoundBg.Stop ();

    }

	public void playSoundBG(bool spe)
	{
		Debug.Log ("bat dau sound bg: " + spe);
		if (spe) {

			sourceSoundBg.clip = bgSoundSpe;

		} else {
			sourceSoundBg.clip = bgSound;
		}

		sourceSoundBg.Play ();
	}

	public void startGame()
	{
		sourceSound.clip = startGameSound;
		sourceSound.Play ();
	}

	public void finishGame()
	{
		sourceSound.clip = timeupSound;
		sourceSound.Play ();
	}

	public void shotMiss()
	{
		sourceSound.clip = shotMissSound;
		sourceSound.Play ();
	}

	public void shotGame()
	{
		Debug.Log ("sut bong mode nao: " + GameData.Instance ().gameMode);
		if (GameData.Instance ().gameMode) {
			sourceSound.clip = shotSound;
		} else {
			sourceSound.clip = shotSpeSound;
		}

		sourceSound.Play ();
	}

	/**
	 * re: 0--> sut truot, 1: sut trung gon, 2 sut trung target
	 **/
	public void resultShot(int re)
	{
		Debug.Log ("sut trugn muc tieu: " + re);
		if (re == 0) {
			sourceSound.clip = shotMissSound;
            sourceSound.Play();
        } else if (re == 1) {
            sourceSoundEffect.clip = shotExSound;
            sourceSoundEffect.Play();
        } else if (re == 2) {
			if (GameData.Instance ().gameMode) {
                sourceSoundEffect.clip = shotExTargetSound;
				sourceSoundEffect1.clip = shotExSound;
			} else {
                sourceSoundEffect.clip = shotExTargetSpeSound;
				sourceSoundEffect1.clip = shotSpeSound;
			}
            sourceSoundEffect.Play();
            sourceSoundEffect1.Play ();
		}

		
	}
}
