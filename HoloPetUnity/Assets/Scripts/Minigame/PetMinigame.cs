using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minigame;

namespace Minigame {

    public class PetMinigame : Pet {

        private static PetMinigame instance;
        public static PetMinigame Instance {
            get {
                if (instance == null)
                    instance = FindObjectOfType<PetMinigame>();
                return instance;
            }
        }

        public int happyCountNeededToWin = 6;
        public int happyCount;
        public float happyIncentiveIncreasePerCorrectMotivationalCommand = 25;
        public float happyIncentiveDecreasePerWrongMotivationalCommand = 15;
        public float happyIncentiveIncreasePerCorrectPunishmentCommand = 25;
        public float happyIncentiveIncreasePerWrongPunishmentCommand = 15;

        public float movementSpeedOnAngry, movementSpeedOnSad, movementSpeedOnHappy;

        public Emotion lastEmotionCollected;
        [SerializeField]
        private float incentiveToCollectHappyResource;
        public float IncentiveToCollectHappyResource {
            get { return incentiveToCollectHappyResource; }
            set { if (value >= 0 && value <= 100)
                    incentiveToCollectHappyResource = value; }
        }
        public System.Action OnGameOver;
     
        private void Awake() {
            instance = this;
        }

        public void OnResourceCollected(Resource resource) {
            resource.OnCollected();
            lastEmotionCollected = resource.emotion;

            if(resource.emotion == Emotion.Happy) {
                happyCount++;
                if (happyCount == happyCountNeededToWin)
                    GameOver();
                Head.MovementSpeed = movementSpeedOnHappy;
            } else {
                if(happyCount > 1)
                    happyCount -= 2;
                if (resource.emotion == Emotion.Angry)
                    Head.MovementSpeed = movementSpeedOnAngry;
                else
                    Head.MovementSpeed = movementSpeedOnSad;
            }
        }

        public void OnMotivatationRecieved() {
            if (lastEmotionCollected == Emotion.Happy)
                IncentiveToCollectHappyResource += happyIncentiveIncreasePerCorrectMotivationalCommand;
            else
                IncentiveToCollectHappyResource -= happyIncentiveDecreasePerWrongMotivationalCommand;
        }

        public void OnPunishmentRecieved() {
            if (lastEmotionCollected == Emotion.Happy)
                IncentiveToCollectHappyResource -= happyIncentiveDecreasePerWrongMotivationalCommand;
            else
                IncentiveToCollectHappyResource += happyIncentiveIncreasePerCorrectPunishmentCommand;
        }

        private void GameOver() {
            Debug.Log("Game Over");
            Resources.Clear();
            OnGameOver();
            Head.SlowDown();
        }
    }
}