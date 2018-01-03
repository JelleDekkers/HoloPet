using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {

    [SerializeField] private Vector2 movementDirection;
    [SerializeField] private float movementSpeed = 1;
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

        while(timer < eatDuration) {
            transform.localScale = Vector3.Lerp(start, target, timer / eatDuration);
            timer += Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnBecameVisible() {
        hasBeenVisible = true;
    }

    private void OnBecameInvisible() {
        if (!hasBeenVisible)
            return;

        Debug.Log(name + " became invisible from camera, destroy() called");
        Destroy(gameObject);
    }
}
