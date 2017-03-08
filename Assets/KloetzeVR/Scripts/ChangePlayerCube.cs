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
        private BoxCollider colliderPlayerCube;
        public GameObject meshAndRigid;
        public GameObject cubesToChange;
        public GameObject[] playerCubeTransforms;
        //public enum cubeNames { SmallCube, GrabCube, LargeCube, InteractionCube};
        private int actualCubeNameInt;
        public enum cubeNames : int {SmallCube = 0, GrabCube = 1, LargeCube = 2, InteractionCube = 3, END = 4};
        public bool startDone = false;

        void Start() {
            actualPlayerCube = VRTK_SDKManager.instance.actualBoundaries;
            colliderPlayerCube = actualPlayerCube.AddComponent<BoxCollider>();
            meshAndRigid = this.gameObject.transform.FindChild("MeshAndRigid").gameObject;
            meshAndRigid.GetComponent<Renderer>().enabled = true;
            cubesToChange = this.gameObject.transform.FindChild("CubesToChange").gameObject;
            playerCubeTransforms = new GameObject[4];
            int childInt = 0; 
            foreach (Transform child in cubesToChange.transform) {
                playerCubeTransforms[childInt++] = child.gameObject;
                child.gameObject.SetActive(true);
            }
            cubesToChange.transform.SetParent(null);
            actualCubeNameInt = (int)cubeNames.GrabCube;
            changeCube(actualCubeNameInt);
            //meshAndRigid.transform.get
            startDone = true;

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
            
            scaleSize = playerCubeTransforms[cubeInt].transform.lossyScale.x;
            playerCubeTransforms[previousCubeInt(cubeInt)].SetActive(true);
            meshAndRigid.transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
            meshAndRigid.transform.localPosition = new Vector3(0, scaleSize/2, 0);
            if (startDone) {
                Vector3 newPosition = actualPlayerCube.transform.position;
                newPosition.y += actualPlayerCube.transform.lossyScale.y / 2;
                playerCubeTransforms[previousCubeInt(cubeInt)].transform.position = newPosition;
            }
            actualPlayerCube.GetComponent<Collider>().enabled = false;
            colliderPlayerCube.size = new Vector3(scaleSize, scaleSize, scaleSize);
            colliderPlayerCube.center = new Vector3(0, scaleSize/2, -0);
            this.transform.SetParent(actualPlayerCube.transform);
            actualPlayerCube.transform.position = playerCubeTransforms[cubeInt].transform.position;
            meshAndRigid.GetComponent<Renderer>().material = playerCubeTransforms[cubeInt].GetComponent<Renderer>().material;
            playerCubeTransforms[cubeInt].SetActive(false);
        }

        private void LateUpdate() {
            //transform.position = actualPlayerCube.transform.position + offset;
        }

        private int nextCubeInt(int cubeInt) {
            return ( (actualCubeNameInt + 1) % (int)cubeNames.END );
        }

        private int previousCubeInt(int cubeint) {
            return (cubeint-1+(int)cubeNames.END) % (int)cubeNames.END;
        }
    }
}