using System.Collections.Generic;
using UnityEngine;
using State = FsmSwitchAdvancedState;

public enum FsmSwitchAdvancedState {
  IDLE,
  USING,
  USED
}

public class Fsm_Switch_Advanced : MonoBehaviour {
  public State State { get; private set; }
  private HashSet<KeyValuePair<State, State>> allowedTransitions;

  private void Start() {
    State = State.IDLE;
    allowedTransitions = new() {
      new(State.IDLE, State.USING),
      new(State.USING, State.USED),
    };
  }

  private void Update() {
    StateStay();
  }

  public void ChangeState(State newState) {
    if (allowedTransitions.Contains(new(State, newState))) {
      StateExit();
      State = newState;
      StateEnter();
    }
  }

  private void StateEnter() {
    switch (State) {
      case State.IDLE:
        //do idle things
        break;

      case State.USING:
        // use this thing
        break;

      case State.USED:
        // used state stuff
        break;
    }
  }
  private void StateStay() {
    switch (State) {
      case State.IDLE:
        //do idle things
        break;

      case State.USING:
        // use this thing
        break;

      case State.USED:
        // used state stuff
        break;
    }
  }
  private void StateExit() {
    switch (State) {
      case State.IDLE:
        //do idle things
        break;

      case State.USING:
        // use this thing
        break;

      case State.USED:
        // used state stuff
        break;
    }
  }
}
