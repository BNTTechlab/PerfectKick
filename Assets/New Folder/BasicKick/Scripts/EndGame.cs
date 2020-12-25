using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {
    public List<GameObject> list;
    public ShootingSceneManager shoot;

    SoundController soundController;
    // Use this for initialization
    void Start () {
        soundController = GameObject.Find("SoundController").GetComponent<SoundController>();
        ShootingSceneManager shoot = gameObject.GetComponent<ShootingSceneManager>();
        //if (GameData.Instance().score >=800)
        //{
			soundController.stopSoundBg();
            soundController.playSoundWin();
      //  }
        list[0].SetActive(false);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        shoot.photo = true;
       // list[0].SetActive(true);
         SceneManager.LoadScene("MiniGame");
    }
    public float ShowEndGame()
    {
        float tiLe = 0;
        Highscore highscore = GameObject.FindObjectOfType<WriteJson>().LoadHighScore();
        List<PlayerScore> sortedList = highscore.scores.OrderByDescending(o => o.score).ToList();

		if (sortedList.Count == 1) {
			return 1;
		}

    //    for (int i = 0; i < sortedList.Count; i++)
    //    {
			
    //        if (GameObject.FindObjectOfType<WriteJson>().shoot.namePhoto == sortedList.ElementAt(i).fileName)
    //        {
				//tiLe = (sortedList.Count - (i + 1)) / (float)(sortedList.Count - 1);
    //            if (tiLe < 0)
    //            {
    //                tiLe = 0;
    //            }
				//break;
    //        }
    //    }
        
        return tiLe;
    }
}
