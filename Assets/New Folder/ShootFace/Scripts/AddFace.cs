using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFace : MonoBehaviour {
    public GUITexture btnTexture;
    public ShootingSceneManager shoot;
    public float x,x1;
    public float y,y1;
    public GUITexture playerAva;
    public GUITexture bestPlayerAva;
    public WriteJson json;
    public Texture best;

    // Use this for initialization
    void OnGUI() {
        if (GUI.Button(new Rect(x, y, 105, 105), btnTexture.texture))
        {
            shoot.faceImage.texture = btnTexture.texture;
          //  ava.texture = btnTexture.texture;
        }
        //if (GUI.Button(new Rect(x1, y1, 105, 105), Texture best))
        //{
        //   //bestPlayerAva.texture = json.BestAva();
        //   // Debug.Log(json.BestScore);
        //}
    }
    private void Start()
    {
        WriteJson writeJson = gameObject.GetComponent<WriteJson>();
        playerAva.texture = btnTexture.texture;


    }
}
