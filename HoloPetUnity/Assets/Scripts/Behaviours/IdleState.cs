using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State {

    public IdleState(PetController pet) : base(pet) { }

    public override void Update() { }

    public override void OnStateEnter() {
        pet.pathFinder.SetNewRandomTarget();
        pet.walker.enabled = true;
        pet.walker.OnTargetReached += pet.pathFinder.SetNewRandomTarget;
    }

    public override void OnStateExit() {
        pet.walker.OnTargetReached -= pet.pathFinder.SetNewRandomTarget;
    }
}
