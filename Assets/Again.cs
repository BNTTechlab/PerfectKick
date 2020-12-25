using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Again : MonoBehaviour {
    public List<GameObject> list;
    // Use this for initialization
    void Start () {
     
    }
	
	// Update is called once per frame
	void Update () {
		 list[0].SetActive(false);
	}  
}
