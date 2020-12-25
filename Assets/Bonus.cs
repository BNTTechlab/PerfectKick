using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {
    public GameObject original;
    // Use this for initialization
    void Start () {
        GameObject clone = (GameObject)Instantiate(original, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
