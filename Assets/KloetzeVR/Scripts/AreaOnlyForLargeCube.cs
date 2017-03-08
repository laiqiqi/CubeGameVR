   
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AreaOnlyForLargeCube : MonoBehaviour {
        public GameObject actualPlayerCube;
        // Use this for initialization
        void Start () {
            actualPlayerCube = VRTK.VRTK_SDKManager.instance.actualBoundaries;
        }
	
	    // Update is called once per frame
	    void Update () {
		
	    }
    }
