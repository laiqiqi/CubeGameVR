namespace VRTK {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Linq;

    /**
     * 
     * Script damit, der Spieler seine Figur wechseln kann, mit den dazu passenden Eigenschaften der Figuren
     * 
     * */

    public class ChangePlayerCube : MonoBehaviour {
        public GameObject actualPlayerCube;
        public GameObject playArea;
        public GameObject vrtkRightController;
        public GameObject vrtkLeftController;
        private BoxCollider colliderPlayerCube;
        public GameObject meshAndRigid;
        public GameObject cubesToChange;
        public GameObject[] playerCubes;
        public List<Transform> tInteractionObjets;
        public int anzahlCubes = 4;
        public int actualCubeNameID;
        public enum cubeNames : int {SmallCube = 0, GrabCube = 1, LargeCube = 2, InteractionCube = 3, END = 4};
        public bool startDone = false;

        void Start() {
            actualPlayerCube = VRTK_SDKManager.instance.actualBoundaries;
            playArea = GameObject.Find("PlayArea");
            vrtkLeftController = VRTK_SDKManager.instance.scriptAliasLeftController;
            vrtkRightController = VRTK_SDKManager.instance.scriptAliasRightController;
            colliderPlayerCube = actualPlayerCube.AddComponent<BoxCollider>();
            meshAndRigid = this.gameObject.transform.FindChild("MeshAndRigid").gameObject;
            meshAndRigid.GetComponent<Renderer>().enabled = true;
            cubesToChange = this.gameObject.transform.FindChild("CubesToChange").gameObject;
            playerCubes = new GameObject[anzahlCubes];
            int childInt = 0; 
            foreach (Transform child in cubesToChange.transform) {
                playerCubes[childInt++] = child.gameObject;
                child.gameObject.SetActive(true);
            }

            foreach (Transform child in GameObject.Find("InteractionObjects").transform) {
                tInteractionObjets.Add(child.FindChild("RadialMenu"));
            }

            cubesToChange.transform.SetParent(null);
            actualCubeNameID = (int)cubeNames.GrabCube;
            //setzt den Capsule Collider des Spielers auf false, da wir den Box Collider nutzen wollen. 
            //Es wird nur auf false gesetzt und nicht Destroy() angewendet, da Scripte vom VRTK anscheinend 
            //den Capsule Collider benötigen (warum auch immer)
            actualPlayerCube.GetComponent<Collider>().enabled = false;

            resetFunctionsForPlayer();
            this.transform.SetParent(actualPlayerCube.transform);
            changeCube(actualCubeNameID);
            startDone = true;
        }

        // Update is called once per frame
        void Update() {
            if (Input.GetKeyUp(KeyCode.L)) {
                //Debug.Log("L Key was released. Change to CubeNr" + ((actualCubeNameInt+1)%( (int)cubeNames.END )));
                actualCubeNameID = nextCubeInt(actualCubeNameID) ;
                changeCube(actualCubeNameID);
            }
        }

        public void changeCube(int cubeID) {

            //vorherigen PlayerCube Parent auf das CubesToChange.transform setzen, 
            //damit dieser sich nicht mehr mit der Kamera bewegt
            if (startDone) {
                playerCubes[previousCubeInt(cubeID)].transform.SetParent(cubesToChange.transform);
            }

            float scaleSize = 0f;
            scaleSize = playerCubes[cubeID].transform.lossyScale.x;

            //BoxCollider für den playerCube setzen
            colliderPlayerCube.size = new Vector3(scaleSize, scaleSize, scaleSize);
            colliderPlayerCube.center = new Vector3(0, scaleSize / 2, -0);

            //auf die Position der PlayerCube springen und Farbe ändern
            actualPlayerCube.transform.position = playerCubes[cubeID].transform.position;

            setFunctionsForSelectedPlayerCube(cubeID);
            playerCubes[cubeID].transform.SetParent(transform);


            ///////alt//////////////

            ////Position für den zuvor ausgewählten Cube speichen
            //if (startDone) {
            //    Vector3 newPosition = actualPlayerCube.transform.position;
            //    newPosition.y += actualPlayerCube.transform.lossyScale.y / 2;
            //    playerCubes[previousCubeInt(cubeID)].transform.position = newPosition;
            //    playerCubes[previousCubeInt(cubeID)].SetActive(true);
            //}

            ////Mesh Form für den aktuellen Cube setzen
            //float scaleSize = 0f;
            //scaleSize = playerCubes[cubeID].transform.lossyScale.x;
            //meshAndRigid.transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
            //meshAndRigid.transform.localPosition = new Vector3(0, scaleSize / 2, 0);

            ////BoxCollider für den aktuellen Cube setzen
            //colliderPlayerCube.size = new Vector3(scaleSize, scaleSize, scaleSize);
            //colliderPlayerCube.center = new Vector3(0, scaleSize / 2, -0);

            ////auf die Position der aktuell augewählten Cube springen und Farbe ändern
            //actualPlayerCube.transform.position = playerCubes[cubeID].transform.position;
            //meshAndRigid.GetComponent<Renderer>().material = playerCubes[cubeID].GetComponent<Renderer>().material;
            //playerCubes[cubeID].SetActive(false);

            //setFunctionsForSelectedPlayerCube(cubeID);
        }

        private void LateUpdate() {
        }

        private int nextCubeInt(int cubeID) {
            return ( (cubeID + 1) % (int)cubeNames.END );
        }

        private int previousCubeInt(int cubeID) {
            return (cubeID-1+(int)cubeNames.END) % (int)cubeNames.END;
        }

        public void resetFunctionsForPlayer() {

        }

        public void setFunctionsForSelectedPlayerCube(int cubeID) {
            
            string gObj = playerCubes[cubeID].name;
            switch (gObj) {
                case "SmallCube":
                    vrtkRightController.GetComponent<VRTK_InteractGrab>().enabled = false;
                    vrtkLeftController.GetComponent<VRTK_InteractGrab>().enabled = false;
                    playArea.GetComponent<VRTK_PolicyList>().identifiers[1] = "IncludeTeleport";
                    setInteractionObjRadiant(false);
                    break;

                case "GrabCube":
                    vrtkRightController.GetComponent<VRTK_InteractGrab>().enabled = true;
                    vrtkLeftController.GetComponent<VRTK_InteractGrab>().enabled = true;
                    playArea.GetComponent<VRTK_PolicyList>().identifiers[1] = "IncludeTeleport";
                    setInteractionObjRadiant(false);
                    break;

                case "LargeCube":
                    vrtkRightController.GetComponent<VRTK_InteractGrab>().enabled = false;
                    vrtkLeftController.GetComponent<VRTK_InteractGrab>().enabled = false;
                    playArea.GetComponent<VRTK_PolicyList>().identifiers[1] = "LargeIncludeTeleport";
                    setInteractionObjRadiant(false);
                    break;

                case "InteractionCube":
                    vrtkRightController.GetComponent<VRTK_InteractGrab>().enabled = false;
                    vrtkLeftController.GetComponent<VRTK_InteractGrab>().enabled = false;
                    playArea.GetComponent<VRTK_PolicyList>().identifiers[1] = "IncludeTeleport";
                    setInteractionObjRadiant(true);
                    break;

                default:
                    Debug.Log("ERROR: Cube mit dem Namen " + gObj + " hat noch keine Funktionen");
                    break;
            }
        }

        private void setInteractionObjRadiant(bool radialOn) {
            foreach (Transform intObject in tInteractionObjets) {
                intObject.gameObject.SetActive(radialOn);
            }
        } 
    }
}