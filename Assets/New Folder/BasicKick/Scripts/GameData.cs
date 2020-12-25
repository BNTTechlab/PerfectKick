using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData {
	//static variable
	public static GameData instance;
	//public variable
	public Sprite myImage;
	public string myPathImage;
	public int score;//điểm cho mỗi lần ghi bàn
	public int numShoot;//so lan da
	public int combo;//so lan sut trung lien tuc
    public int comboTime;
    public int timePlay;//thoi gian choi
	public ArrayList allScore = new ArrayList();
	public bool gameMode;//true: tinh diem binh thuong, false: x2 diem an dc
	//private variable

	/*singleton*/
	public static GameData Instance ()
	{
		if (instance == null) {
			instance = new GameData ();
		}
		return instance;
	}
}
