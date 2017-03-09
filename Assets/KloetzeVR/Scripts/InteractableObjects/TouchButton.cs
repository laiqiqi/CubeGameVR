using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchButton : MonoBehaviour {
    public GameObject wallToMove;
    public Vector3 goal = new Vector3(0, 0, 0);
    
    public float rateOfMovement = 10.0f;
    // Use this for initialization

    public void moveWall() {
        Debug.Log("moveWall");
        StartCoroutine("moveLinear");
    }

    public void Start() {
        goal = new Vector3(wallToMove.transform.position.x, -0.2f, wallToMove.transform.position.z);
    }

    public void Update() {
        if (Input.GetKeyUp(KeyCode.K)) {
            moveWall();
        }
    }

    IEnumerator moveLinear() {
        while (true) {
            Vector3 start = wallToMove.transform.position;
            if (start == goal)
                break;
            wallToMove.transform.position = Vector3.MoveTowards(start, goal, Time.deltaTime * rateOfMovement);
            yield return null;
        }
    }
}
