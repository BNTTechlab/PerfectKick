using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour {
   // public PlayerControls player;
    // Use this for initialization
    void Start () {
       // player.x = 0;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Bu");
    }
}
