namespace VRTK {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /**
     * 
     * Script damit die Square Area, also der teleportierbare Bereich sich mit dem Spieler bewegt
     * 
    **/

    public class FollowPlayer : MonoBehaviour {
        //public GameObject actualPlayer;
        //// Use this for initialization
        //void Start() {
        //    actualPlayer = VRTK_SDKManager.instance.actualBoundaries;
        //}

        //// Update is called once per frame
        //void Update() {
        //    float distanceWanted = 1.0f;

        //    Vector3 diff = transform.position - actualPlayer.transform.position;
        //    //diff.y = 0; // ignore Y
        //    transform.position = actualPlayer.transform.position + diff.normalized * distanceWanted;
        //}
        ////////////////////////////////////////////////////////////////////////
        //public float interpVelocity;
        //public float minDistance;
        //public float followDistance;
        //public GameObject target;
        //public Vector3 offset;
        //Vector3 targetPos;
        //// Use this for initialization
        //void Start() {
        //    targetPos = transform.position;
        //    target = VRTK_SDKManager.instance.actualBoundaries;
        //}

        //// Update is called once per frame
        //void FixedUpdate() {
        //    if (target) {
        //        Vector3 posNoZ = transform.position;
        //        posNoZ.z = target.transform.position.z;

        //        Vector3 targetDirection = (target.transform.position - posNoZ);

        //        interpVelocity = targetDirection.magnitude * 5f;

        //        targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

        //        transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);

        //    }
        //}
        ////////////////////////////////////////////////////////////////////////
        public GameObject objectToFollow;
        public float speed = 0.5f;
        int oldLayer = -1;
        int voidLayer;
        float offsetY;


        private void Start() {
            objectToFollow = VRTK_SDKManager.instance.actualBoundaries;
            voidLayer = LayerMask.NameToLayer("Void");
            DisableCollider(gameObject.GetComponent<Collider>());
            offsetY = objectToFollow.transform.position.y - this.transform.position.y;
            print(offsetY);
        }
        void Update() {
            float interpolation = speed * Time.deltaTime;

            Vector3 position = this.transform.position;
            position.y = 0.5f;
            position.x = Mathf.Lerp(this.transform.position.x, objectToFollow.transform.position.x, interpolation);
            position.z = Mathf.Lerp(this.transform.position.z, objectToFollow.transform.position.z, interpolation);
            this.transform.position = position;

            //Vector3 position = this.transform.position;
            //print(position);
            //position.y = Mathf.Lerp(this.transform.position.y, objectToFollow.transform.position.y, interpolation);
            ////position.y = objectToFollow.transform.position.y - offsetY;
            //position.x = this.transform.position.x;
            //position.z = this.transform.position.z;
            //this.transform.position = position;
            //print(position);
        }
        void DisableCollider(Collider col) {
            print("disable " + col.name);
            oldLayer = col.gameObject.layer;
            col.gameObject.layer = voidLayer;
        }

        void EnableCollider(Collider col) {
            col.gameObject.layer = oldLayer;
        }
    }
}