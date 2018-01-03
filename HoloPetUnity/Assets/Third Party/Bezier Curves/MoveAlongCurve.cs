using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BezierAsset {

    public class MoveAlongCurve : MonoBehaviour {

        [SerializeField] private BezierCurve spline;
        [SerializeField] private float duration;

        private float progress;

        //private Quaternion initialRotation;

        //private void Start() {
        //    initialRotation = transform.rotation;
           
        //}

        void Update() {
            if (spline == null)
                return;

            progress += Time.deltaTime / duration;

            if (progress > 1f)
                progress = progress - 1f;
            
            transform.position = spline.GetPointAt(progress);
            Vector3 rot = spline.GetDirection(progress) - transform.position;
            transform.rotation = Quaternion.LookRotation(rot);
            
            //transform.LookAt(rot);
            //transform.rotation *= initialRotation;
        }
    }
}