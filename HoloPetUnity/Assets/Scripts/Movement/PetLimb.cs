using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetLimb : MonoBehaviour {

    private Pet controller;
    private Transform parent;
    private float maxDistance;
    private Quaternion initialRotation;

    private void Start() {
        maxDistance = Vector3.Distance(transform.position, parent.position);
        initialRotation = transform.rotation;
    }

    public void Init(Pet controller, Transform parent) {
        this.controller = controller;
        this.parent = parent;
    }

    private void Update() {
        if (Vector3.Distance(transform.position, parent.position) > maxDistance) {
            transform.position = Vector3.Lerp(transform.position, parent.position, controller.movementSpeed * Time.deltaTime);
            transform.LookAt(parent);
            transform.rotation *= initialRotation;
        }
    }
}
