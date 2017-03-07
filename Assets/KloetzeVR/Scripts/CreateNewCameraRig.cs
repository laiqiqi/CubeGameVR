namespace VRTK {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CreateNewCameraRig : MonoBehaviour {
        public GameObject actualPlayerCube;
        public static CreateNewCameraRig instance;
        // Use this for initialization
        void Start() {
            instance = null;
        }

        // Update is called once per frame
        void Update() {

        }

        public static void doCreateNewCameraRig(){
            Destroy(VRTK_SDKManager.instance.actualBoundaries);
            GameObject go = (GameObject)Instantiate(Resources.Load("VRSimulatorCameraRig"));
            VRTK_SDKManager.instance.actualBoundaries = go;
        }
    }
}