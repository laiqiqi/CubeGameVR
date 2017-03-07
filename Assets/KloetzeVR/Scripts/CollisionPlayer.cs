using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPlayer : MonoBehaviour {
    public GameObject playerCameraBlack;

    // Use this for initialization
    void Start () {
        playerCameraBlack = this.gameObject.transform.FindChild("CameraBlack").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Wall") {  //9 -> Layer Wall
            //print("bum " + col.transform.name);
            //Destroy(col.gameObject);
            //playerCamera.SetActive(false);
            playerCameraBlack.SetActive(true);
        }
    }
    //private void OnCollisionStay(Collision col) {
    //    if (col.gameObject.tag == "Wall") {  //9 -> Layer Wall
    //        print(this.transform.TransformPoint(this.transform.localPosition));
    //        Vector3 oldCamPosition = vrSimulatorCameraRig.transform.position;
    //        Vector3 newCamPosition = this.transform.TransformPoint(this.transform.localPosition);
    //        newCamPosition.y = oldCamPosition.y;
    //        vrSimulatorCameraRig.transform.position = newCamPosition;
    //    }

    //}
    //private void OnTriggerEnter(Collider col) {
    //    if (col.gameObject.tag == "Wall") {  //9 -> Layer Wall
    //        //print("bum " + col.transform.name);
    //        //Destroy(col.gameObject);
    //        //playerCamera.SetActive(false);
    //        playerCameraBlack.SetActive(true);
    //        vrSimulatorCameraRig.transform.position = (this.transform.position);
    //    }
    //}
    void OnCollisionExit(Collision collisionInfo) {
        if (collisionInfo.gameObject.tag == "Wall") {
            //Debug.Log("Wand nicht mehr in Berührung");
            playerCameraBlack.SetActive(false);
            //playerCamera.SetActive(true);
        }
    }
    //private void OnTriggerExit(Collider collisionInfo) {
    //    if (collisionInfo.gameObject.tag == "Wall") {
    //        //Debug.Log("Wand nicht mehr in Berührung");
    //        playerCameraBlack.SetActive(false);
    //        //playerCamera.SetActive(true);
    //    }
    //}
}
