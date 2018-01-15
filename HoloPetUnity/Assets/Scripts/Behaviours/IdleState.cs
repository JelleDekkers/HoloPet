using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State {

    private float waitTime = 1f;
    private IEnumerator waitCoroutine;

    public IdleState(PetController pet) : base(pet) { }

    public override void Update() { }

    public override void OnStateEnter() {
        pet.pathFinder.SetNewRandomTarget();
        pet.walker.enabled = true;
        pet.walker.OnTargetReached += pet.pathFinder.SetNewRandomTarget;
        ArduinoInput.OnMotionDetected += MotionDetected;
    }

    public override void OnStateExit() {
        pet.walker.OnTargetReached -= pet.pathFinder.SetNewRandomTarget;
        ArduinoInput.OnMotionDetected -= MotionDetected;
    }

    public void MotionDetected(int direction) {
        pet.walker.enabled = false;
        if (waitCoroutine != null)
            pet.StopCoroutine(waitCoroutine);
        pet.StartCoroutine(waitCoroutine);
        // rotate towards direction
    }

    private IEnumerator WaitTimerAfterMotionDetected() {
        yield return new WaitForSeconds(waitTime);
        pet.walker.enabled = true;
    }
}
