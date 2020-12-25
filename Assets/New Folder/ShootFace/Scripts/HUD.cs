using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public Sprite[] BrainSprite;

    public Image BrainUI;

    public PlayerControls player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
    }

    void FixedUpdate()
    {
        if (player.curHealth==0 || player.curHealth==1)
        {
            BrainUI.sprite = BrainSprite[1];
        }
             if (player.curHealth == 2)
        {
            BrainUI.sprite = BrainSprite[2];
        }

      //  BrainUI.sprite = BrainSprite[player.curHealth]; 
    }

}
