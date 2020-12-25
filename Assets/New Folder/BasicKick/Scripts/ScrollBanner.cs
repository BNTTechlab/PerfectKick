using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBanner : MonoBehaviour {
    // Use this for initialization
    float i = -15;
	void Start () {
		
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 offset = GetComponent<MeshRenderer>().material.mainTextureOffset;
        offset.x =i++;
        GetComponent<MeshRenderer>().material.mainTextureOffset = offset * (Time.deltaTime/50);

    }
}
