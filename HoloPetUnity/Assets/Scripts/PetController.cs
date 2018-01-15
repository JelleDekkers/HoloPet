using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PetController : MonoBehaviour {

    public Pathfinder pathFinder;
    public PetHead walker;
    public PetAnimationController animController;

    public Vector3 sceneSize = new Vector3(5, 7, 5);

    [SerializeField] private VoiceCommand[] commands;

    private State state;

    private SpeechManager speechManager;
    private string[] parsedCommands;

    private void Start() {
        SetState(new SleepState(this));
        speechManager = new SpeechManager(GetAllKeywords());
        speechManager.OnPhraseRecognized += ParseCommandToKeyword;
    }

    private string[] GetAllKeywords() {
        List<string> keywords = new List<string>();

        foreach (VoiceCommand v in commands) {
            foreach (string s in v.commands)
                keywords.Add(s);
        }
        return keywords.ToArray();
    }

    private void ParseCommandToKeyword(string text) {
        Debug.Log("Command: <b>" + text + "</b>");

        VoiceCommand command;
        if (CommandsContain(text, out command)) {
            command.onCommandRecieved.Invoke();
        } else {
            Debug.LogWarning("No corresponding command found for " + text);
        }
    }

    public bool CommandsContain(string s, out VoiceCommand command) {
        foreach (VoiceCommand v in commands) {
            if (v.commands.Contains(s)) {
                command = v;
                return true;
            }
        }
        command = null;
        return false;
    }

    public void StartMinigame() {
        Debug.Log("start minigame");
    }

    public void SetState(State s) {
        if (state != null)
            state.OnStateExit();

        state = s;
        state.OnStateEnter();
    }

    public void SetState(int stateIndex) {
        switch(stateIndex) {
            case 0:
                SetState(new SleepState(this));
                return;
            case 1:
                SetState(new IdleState(this));
                return;
            case 2:
                SetState(new AttentiveState(this));
                return;
        }

        Debug.LogWarning("No state matching " + stateIndex + " going to idle");
        SetState(new IdleState(this));
    }
}
