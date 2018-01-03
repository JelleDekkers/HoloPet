using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame {

    public class PetHead : MonoBehaviour {

        private void OnTriggerEnter(Collider other) {
            if (other.GetComponent<Resource>())
                Pet.Instance.CollectResource(other.GetComponent<Resource>());

        }
    }
}