using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour {

    public PetHead Head { get; private set; }

    private void Start() {
        Head = transform.GetChild(0).GetComponent<PetHead>();
    }
}
