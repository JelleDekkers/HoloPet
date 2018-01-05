using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame {

    public enum Direction {
        up = 0,
        right = 1,
        down = 2,
        left = 3
    }

    public class PetMiniGameController : MonoBehaviour {

        [SerializeField] private float directMovementDistance = 5;
        [SerializeField] private Pathfinder pathfinder;

        [Tooltip("Commands for direct movements, such as: 'Go Left'")]
        [SerializeField] private string[] directMovementCommands;
        [Tooltip("Commands for the nearest direction, such a:s 'get the nearest left one'")]
        [SerializeField] private string[] nearestMovementCommands;
        [Tooltip("Commands for the nearest color, such as: 'get the blue one'")]
        [SerializeField] private string[] nearestColorCommands;
        [Tooltip("Motivational Commands such as 'Good Job'")]
        [SerializeField] private string[] motivationalCommands;
        [Tooltip("Punishing Commands such as 'No'")]
        [SerializeField] private string[] punishingCommands;

        private const string COMMAND_KEYWORD = "<>";
        private SpeechManager speechManager;
        private PetHead head;

        private Vector3[] directions = { Vector3.forward, Vector3.right, Vector3.back, Vector3.left };
        private List<string> directionalMovementCommandsList;
        private List<string> nearestMovementCommandsList;
        private List<string> nearestColorCommandsList;

        private void Start() {
            speechManager = new SpeechManager(GetAllKeywords());
            speechManager.OnPhraseRecognized += ParseCommand;
            head = transform.GetChild(0).GetComponent<PetHead>();
        }

        private string[] GetAllKeywords() {
            List<string> keywords = new List<string>();

            directionalMovementCommandsList = ParseCommandsToKeywords(directMovementCommands, new Direction());
            keywords.AddRange(directionalMovementCommandsList);
            nearestMovementCommandsList = ParseCommandsToKeywords(nearestMovementCommands, new Direction());
            keywords.AddRange(nearestMovementCommandsList);
            nearestColorCommandsList = ParseCommandsToKeywords(nearestColorCommands, new EmotionColor());
            keywords.AddRange(nearestColorCommandsList);

            keywords.AddRange(motivationalCommands);
            keywords.AddRange(punishingCommands);
            return keywords.ToArray();
        }

        private List<string> ParseCommandsToKeywords(string[] commands, Enum enumType) {
            List<string> keywords = new List<string>();
            int count = Enum.GetNames(enumType.GetType()).Length;
            foreach (string s in commands) {
                if (s.Contains(COMMAND_KEYWORD)) {
                    for (int i = 0; i < count; i++) {
                        string keyword = Enum.ToObject(enumType.GetType(), i).ToString();
                        string temp = s.Replace(COMMAND_KEYWORD, keyword);
                        keywords.Add(temp);
                    }
                } else {
                    keywords.Add(s);
                }
            }
            return keywords;
        }

        private void ParseCommand(string text) {
            Debug.Log("Command: <b>" + text + "</b>");

            if (directionalMovementCommandsList.Contains(text))
                DirectMoveCommandRecieved(text);
            else if (nearestMovementCommandsList.Contains(text))
                NearestMoveCommandRecieved(text);
            else if (nearestColorCommandsList.Contains(text))
                NearestColorCommandRecieved(text);
            else if (motivationalCommands.Contains(text))
                MotivationalCommandRecieved(text);
            else if (punishingCommands.Contains(text))
                PunishingCommandRecieved(text);
            else
                Debug.LogWarning("No corresponding command found for " + text);
        }

        private void DirectMoveCommandRecieved(string command) {
            Debug.Log("move command keyword: " + command);
            Direction dirEnum = GetDirectionFromCommand(command);
            Vector3 direction = directions[(int)dirEnum];
            Vector3 targetPosition = head.transform.position + (direction * directMovementDistance);
            pathfinder.SetNewTargetPosition(targetPosition);
        }

        private void NearestMoveCommandRecieved(string command) {
            Debug.Log("nearest movement command keyword: " + command);
        }

        private void NearestColorCommandRecieved(string command) {
            Debug.Log("nearest color command keyword: " + command);
        }

        private void MotivationalCommandRecieved(string command) {
            Debug.Log("motivational command keyword: " + command);
        }

        private void PunishingCommandRecieved(string command) {
            Debug.Log("punishment command keyword: " + command);
        }

        private Direction GetDirectionFromCommand(string text) {
            foreach (Direction dir in Enum.GetValues(typeof(Direction))) {
                if (text.Contains(dir.ToString()))
                    return dir;
            }

            Debug.LogWarning("No direction found in " + text);
            return Direction.up;
        }

        private EmotionColor GetColorEmotionFromCommand(string text) {
            foreach (EmotionColor dir in Enum.GetValues(typeof(EmotionColor))) {
                if (text.Contains(dir.ToString()))
                    return dir;
            }

            Debug.LogWarning("No emotionColor found in " + text);
            return EmotionColor.Blue;
        }
    }
}