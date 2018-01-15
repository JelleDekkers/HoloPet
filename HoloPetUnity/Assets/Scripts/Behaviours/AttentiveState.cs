using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentiveState : State {

    public AttentiveState(PetController pet) : base(pet) { }

    public override void Update() { }

    public override void OnStateEnter() {
        // look at player real world space
        // walker uit?
    }

    public override void OnStateExit() { }
}
