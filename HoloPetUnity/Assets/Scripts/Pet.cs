using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour {

    public float movementSpeed = 5;

    private static Pet instance;
    public static Pet Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<Pet>();
            return instance;
        }
    }

    public int happyCountNeededToWin = 10;
    public Emotion currentEmotion;
    public int currentEmotionCount;
    public float currentEnergy;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        Transform[] bones = transform.GetChild(0).parent.GetComponentsInChildren<Transform>();
        for (int i = 2; i < bones.Length; i++) {
            bones[i].gameObject.AddComponent<PetLimb>().Init(this, bones[i - 1]);
        }
    }

    public void CollectResource(Resource resource) {
        StartCoroutine(resource.GetEaten());

        if (resource.emotion == currentEmotion) { 
            currentEmotion++;
        } else {
            currentEmotion = resource.emotion;
            currentEmotionCount = 1;
        }

        currentEnergy += resource.energyValue;
    }
}
