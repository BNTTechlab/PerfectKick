using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour {
    public float smoothtimeX;
    public float smoothtimeY;
    public Vector2 velocity;

    public GameObject player;

    public Vector2 minpos, maxpos;
    public bool bound;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (player.active)
        {
            float posX = Mathf.SmoothDamp(this.transform.position.x+3, player.transform.position.x, ref velocity.x, smoothtimeX);
            float posY = Mathf.SmoothDamp(this.transform.position.y, player.transform.position.y, ref velocity.y, smoothtimeY);
            transform.position = new Vector3(posX+4, posY, transform.position.z);

            if (bound)
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, minpos.x, maxpos.x),
                    Mathf.Clamp(transform.position.y, minpos.y, maxpos.y),
                    Mathf.Clamp(transform.position.z, transform.position.z, transform.position.z));
            }
        }
    }
}
