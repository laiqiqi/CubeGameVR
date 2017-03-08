namespace VRTK{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    /**
     * 
     * Mit diesem Script wird eine Area erzeugt, die je nach Höhe des playerCubes teleportierbare Fläche ist oder nicht
     * 
     * */
    public class NotTeleportableArea : MonoBehaviour {
        public GameObject actualPlayerCube;
        public GameObject meshAndRigid;
        public Transform tAreaForTeleport;
        public float highestPointPlayer;
        public float lowestPointWall;
        // Use this for initialization
        void Start() {
            actualPlayerCube = VRTK_SDKManager.instance.actualBoundaries;
            meshAndRigid = actualPlayerCube.transform.FindChild("CubeInteraction").FindChild("MeshAndRigid").gameObject;
            tAreaForTeleport = transform.FindChild("AreaForTeleport");
            tAreaForTeleport.position = new Vector3(tAreaForTeleport.position.x, 0.02f, tAreaForTeleport.position.z);
        }

        // Update is called once per frame
        void Update() {
            updateAreaTag();
        }

        void updateAreaTag() {
            highestPointPlayer = actualPlayerCube.transform.position.y + meshAndRigid.transform.localPosition.y + (meshAndRigid.transform.lossyScale.y / 2);
            lowestPointWall = transform.position.y - (transform.lossyScale.y / 2);
            if (highestPointPlayer <= lowestPointWall) {
                tAreaForTeleport.tag = "IncludeTeleport";
                tAreaForTeleport.gameObject.GetComponent<Renderer>().material = Resources.Load("TeleportableArea") as Material;
            } else {
                tAreaForTeleport.tag = "Untagged";
                tAreaForTeleport.gameObject.GetComponent<Renderer>().material = Resources.Load("MatNotTeleportArea") as Material;
            }
        }
    }
}