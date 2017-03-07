using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareAreaCollide : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Wall") {  //9 -> Layer Wall
            //print("bum " + col.transform.name);
            //Destroy(col.gameObject);
            //playerCamera.SetActive(false);
            print("SA Trigger Wall");
        }
    }

}
