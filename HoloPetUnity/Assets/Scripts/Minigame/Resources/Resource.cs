using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Emotion {
    Happy = 0,
    Sad = 1,
    Angry = 2
}

public enum EmotionColor {
    Blue = 0,
    Yellow = 1,
    Red = 2
}

namespace Minigame {

    public class Resource : MonoBehaviour {

        public Vector3 movementDirection;
        public float movementSpeed = 1;
        [SerializeField] private float eatDuration = 0.5f;

        private bool hasBeenVisible;

        public Emotion emotion = Emotion.Happy;
        public int energyValue = 10;

        public void Update() {
            transform.Translate(movementDirection * movementSpeed * Time.deltaTime, Space.World);
        }

        public IEnumerator GetEaten() {
            Vector3 start = transform.localScale;
            Vector3 target = new Vector3(0.1f, 0.1f, 0.1f);
            float timer = 0;

            GetComponent<BoxCollider>().enabled = false;

            while (timer < eatDuration) {
                transform.localScale = Vector3.Lerp(start, target, timer / eatDuration);
                timer += Time.deltaTime;

                yield return null;
            }

            Resources.Remove(this);
            Destroy(gameObject);
        }

        private void OnBecameVisible() {
            hasBeenVisible = true;
        }

        private void OnBecameInvisible() {
            if (!hasBeenVisible)
                return;
            Destroy(gameObject);
        }
    }
}