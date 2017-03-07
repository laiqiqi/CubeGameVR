namespace VRTK {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ChangePlayerCube : MonoBehaviour {
        public GameObject actualPlayerCube;
        public GameObject meshCube;
        public GameObject meshAndRigid;
        public GameObject squareArea;
        private Vector3 offset;         //Private variable to store the offset distance between the player and camera
        // Use this for initialization

        void Start() {
            //actualPlayerCube = VRTK_SDKManager.instance.actualBoundaries;
            //meshCube = this.transform.gameObject;
            //meshAndRigid = this.gameObject.transform.FindChild("MeshAndRigid").gameObject;
            //squareArea = this.gameObject.transform.FindChild("SquareArea").gameObject;
            //squareArea = meshAndRigid.transform.FindChild("SquareArea").gameObject;
            changeCube("SmallCube");
        }

        // Update is called once per frame
        void Update() {
            //this.transform.position = actualPlayerCube.transform.position;
        }

        public void changeCube(string cubeName) {
            float scaleSize = 0f;
            switch (cubeName) {
                case "SmallCube":
                    scaleSize = 0.2f;
                    break;
            }
            //BoxCollider colliderPlayerCube = actualPlayerCube.AddComponent<BoxCollider>();
            //colliderPlayerCube.size = new Vector3(scaleSize, scaleSize, scaleSize);
            ////colliderPlayerCube.transform.position = transform.position;
            //offset = transform.position - actualPlayerCube.transform.position;
            //actualPlayerCube.transform.position = transform.position;
            //this.transform.SetParent(actualPlayerCube.transform);
            //BoxCollider colliderPlayerCube = actualPlayerCube.AddComponent<BoxCollider>();
            //colliderPlayerCube.size = new Vector3(scaleSize, scaleSize, scaleSize);
            //colliderPlayerCube.transform.position = transform.position;
            //squareArea.transform.localPosition = new Vector3(0, -meshCube.transform.localScale.y/2, 0);
        }

        private void LateUpdate() {
            //transform.position = actualPlayerCube.transform.position + offset;
        }

        public static void doCreateNewCameraRig() {
            Destroy(VRTK_SDKManager.instance.actualBoundaries);
            GameObject go = (GameObject)Instantiate(Resources.Load("VRSimulatorCameraRig"));
            VRTK_SDKManager.instance.actualBoundaries = go;
        }
    }
}