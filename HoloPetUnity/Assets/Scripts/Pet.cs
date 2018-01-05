using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minigame;

public class Pet : MonoBehaviour {

    private static Pet instance;
    public static Pet Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<Pet>();
            return instance;
        }
    }

    public PetHead Head { get; private set; }

    public int happyCountNeededToWin = 10;
    public Emotion currentEmotion;
    public int currentEmotionCount;
    public float currentEnergy;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        Transform[] bones = transform.GetChild(0).parent.GetComponentsInChildren<Transform>();
        Head = transform.GetChild(0).GetComponent<PetHead>();

        for (int i = 2; i < bones.Length; i++) {
            bones[i].gameObject.AddComponent<PetLimb>().Init(bones[i - 1]);
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
