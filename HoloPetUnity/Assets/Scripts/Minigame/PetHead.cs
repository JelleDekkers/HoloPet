using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame {

    public class PetHead : MonoBehaviour {

        public float movementSpeed = 5f;

        public enum TravelMode { Once, Loop, PingPong };
        public BezierSpline spline;
        public TravelMode travelMode;

        private float progress = 0f;
        private Transform cachedTransform;

        public float NormalizedT {
            get {
                return progress;
            } set {
                progress = value;
            }
        }

        [Range(0f, 0.06f)]
        public float relaxationAtEndPoints = 0.01f;
        //public float movementLerpModifier = 10f;
        public float rotationLerpModifier = 10f;
        public bool lookForward = true;

        private bool isGoingForward = true;
        private Quaternion initialRotation;

        void Awake() {
            cachedTransform = transform;
        }

        private void Start() {
            initialRotation = transform.rotation;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.GetComponent<Resource>())
                Pet.Instance.CollectResource(other.GetComponent<Resource>());
        }

        void Update() {
            float absSpeed = Mathf.Abs(movementSpeed);
            float targetSpeed = (isGoingForward) ? movementSpeed : -movementSpeed;

            Vector3 targetPos;
            if (absSpeed <= 2f)
                targetPos = spline.MoveAlongSpline(ref progress, targetSpeed * Time.deltaTime, maximumError: 0f);
            else if (absSpeed >= 40f)
                targetPos = spline.MoveAlongSpline(ref progress, targetSpeed * Time.deltaTime, increasedAccuracy: true);
            else
                targetPos = spline.MoveAlongSpline(ref progress, targetSpeed * Time.deltaTime);

            cachedTransform.position = targetPos;
            //cachedTransform.position = Vector3.Lerp( cachedTransform.position, targetPos, movementLerpModifier * Time.deltaTime );

            bool movingForward = (movementSpeed > 0f) == isGoingForward;

            if (lookForward) {
                Quaternion targetRotation;
                if (movingForward) {
                    targetRotation = Quaternion.LookRotation(spline.GetTangent(progress));
                    targetRotation *= initialRotation;
                } else
                    targetRotation = Quaternion.LookRotation(-spline.GetTangent(progress));

                cachedTransform.rotation = Quaternion.Lerp(cachedTransform.rotation, targetRotation, rotationLerpModifier * Time.deltaTime);
            }

            if (movingForward) {
                if (progress >= 1f - relaxationAtEndPoints) {
                    if (travelMode == TravelMode.Once)
                        progress = 1f;
                    else if (travelMode == TravelMode.Loop)
                        progress -= 1f;
                    else {
                        progress = 2f - progress;
                        isGoingForward = !isGoingForward;
                    }
                } 
            } else {
                if (progress <= relaxationAtEndPoints) {
                    if (travelMode == TravelMode.Once)
                        progress = 0f;
                    else if (travelMode == TravelMode.Loop)
                        progress += 1f;
                    else {
                        progress = -progress;
                        isGoingForward = !isGoingForward;
                    }
                } 
            }
        }

        public void ResetProgress() {
            progress = 0;
        }
    }
}