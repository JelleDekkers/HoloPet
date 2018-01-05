using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame {

    public class PetLimb : MonoBehaviour {

        [SerializeField]
        private float followSpeed = 20;

        private Transform parent;
        private float maxDistance;
        private Quaternion initialRotation;

        private void Start() {
            maxDistance = Vector3.Distance(transform.position, parent.position);
            initialRotation = transform.rotation;
        }

        public void Init(Transform parent) {
            this.parent = parent;
        }

        private void Update() {
            if (Vector3.Distance(transform.position, parent.position) > maxDistance) {
                transform.position = Vector3.Lerp(transform.position, parent.position, followSpeed * Time.deltaTime);
                transform.LookAt(parent);
                transform.rotation *= initialRotation;

                //var lookPos = parent.position - transform.position;
                //lookPos.x = 0;
                //var rotation = Quaternion.LookRotation(lookPos);
                //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
            }
        }
    }
}