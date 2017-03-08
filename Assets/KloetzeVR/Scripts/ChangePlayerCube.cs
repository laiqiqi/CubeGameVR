namespace VRTK {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /**
     * 
     * Script damit, der Spieler seine Figur wechseln kann, mit den dazu passenden Eigenschaften der Figuren
     * 
     * */

    public class ChangePlayerCube : MonoBehaviour {
        public GameObject actualPlayerCube;
        public GameObject cubeInteraction;
        public GameObject meshAndRigid;
        BoxCollider colliderPlayerCube;
        public GameObject[] playerCubeTransforms = new GameObject[4];
        //public enum cubeNames { SmallCube, GrabCube, LargeCube, InteractionCube};
        private int actualCubeNameInt;
        public enum cubeNames : int {SmallCube = 0, GrabCube = 1, LargeCube = 2, InteractionCube = 3, END = 4};

        void Start() {
            actualPlayerCube = VRTK_SDKManager.instance.actualBoundaries;
            colliderPlayerCube = actualPlayerCube.AddComponent<BoxCollider>();
            cubeInteraction = this.transform.gameObject;
            meshAndRigid = this.gameObject.transform.FindChild("MeshAndRigid").gameObject;
            actualCubeNameInt = (int)cubeNames.GrabCube;
            changeCube(actualCubeNameInt);
            //meshAndRigid.transform.fi
        }

        // Update is called once per frame
        void Update() {
            if (Input.GetKeyUp(KeyCode.L)) {
                //Debug.Log("L Key was released. Change to CubeNr" + ((actualCubeNameInt+1)%( (int)cubeNames.END )));
                actualCubeNameInt = nextCubeInt(actualCubeNameInt) ;
                changeCube(actualCubeNameInt);
            }
        }

        public void changeCube(int cubeInt) {
            float scaleSize = 0f;
            switch (cubeInt) {
                case (int)cubeNames.SmallCube:
                    scaleSize = 0.5f;
                    break;
                case (int)cubeNames.GrabCube:
                    scaleSize = 0.8f;
                    break;
                case (int)cubeNames.LargeCube:
                    scaleSize = 1;
                    break;
                case (int)cubeNames.InteractionCube:
                    scaleSize = 0.8f;
                    break;
                default:
                    print("Error: Cube wechseln nicht möglich zu " + cubeInt);
                    break;
            }
            meshAndRigid.transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
            meshAndRigid.transform.localPosition = new Vector3(0, scaleSize/2, 0);
            actualPlayerCube.GetComponent<Collider>().enabled = false;
            colliderPlayerCube.size = new Vector3(scaleSize, scaleSize, scaleSize);
            colliderPlayerCube.center = new Vector3(0, scaleSize/2, -0);
            //this.transform.SetParent(actualPlayerCube.transform);
            //squareArea.transform.localPosition = new Vector3(0, -meshCube.transform.localScale.y/2, 0);
            
            
        }

        private void LateUpdate() {
            //transform.position = actualPlayerCube.transform.position + offset;
        }

        private int nextCubeInt(int cubeInt) {
            return ( (actualCubeNameInt + 1) % (int)cubeNames.END );
        }
    }
}