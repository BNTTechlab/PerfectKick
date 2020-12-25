using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash1 : MonoBehaviour {

    public GameObject original;
    // Use this for initialization
    void Start()
    {
        StartCoroutine("FlashRun1");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FlashRun1()
    {

        for (int i = 0; i < 20; i++)
        {
            int rnd = Random.Range(2, 8);
            int rnd2 = Random.Range(0, 1);
            yield return new WaitForSeconds(2.5f);
            GameObject clone = (GameObject)Instantiate(original, new Vector3(this.transform.position.x - rnd, this.transform.position.y + rnd2, this.transform.position.z), Quaternion.identity);
          //  yield return new WaitForSeconds(1f);
          //  GameObject clone1 = (GameObject)Instantiate(original, new Vector3(this.transform.position.x+ rnd, this.transform.position.y - rnd2, this.transform.position.z), Quaternion.identity);
           
        }

    }
}
