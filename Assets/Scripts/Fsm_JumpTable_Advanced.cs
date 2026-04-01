using System;
using System.Collections.Generic;
using UnityEngine;
using State = FsmJumpTableAdvancedState;

public enum FsmJumpTableAdvancedState {
  IDLE,
  USING,
  USED
}

public class Fsm_JumpTable_Advanced : MonoBehaviour {
  public State State { get; private set; }
  private HashSet<KeyValuePair<State, State>> allowedTransitions;

  private Dictionary<State, Action> stateEnterMethods;
  private Dictionary<State, Action> stateStayMethods;
  private Dictionary<State, Action> stateExitMethods;

  private void Start() {
    State = State.IDLE;
    
    allowedTransitions = new() {
      new(State.IDLE, State.USING),
      new(State.USING, State.USED),
    };

    stateEnterMethods = new() {
      [State.IDLE] = StateEnter_Idle,
      [State.USING] = StateEnter_Using,
      [State.USED] = StateEnter_Used,
    };
    stateStayMethods = new() {
      [State.IDLE] = StateStay_Idle,
      [State.USING] = StateStay_Using,
      [State.USED] = StateStay_Used,
    };
    stateExitMethods = new() {
      [State.IDLE] = StateExit_Idle,
      [State.USING] = StateExit_Using,
      [State.USED] = StateExit_Used,
    };
  }

  private void Update() {
    if (stateStayMethods.ContainsKey(State)) {
      stateStayMethods[State].Invoke();
    }
  }

  public void ChangeState(State newState) {
    if (allowedTransitions.Contains(new(State, newState))) {
      stateExitMethods[State].Invoke();
      State = newState;
      stateEnterMethods[State].Invoke();
    }
  }

  private void StateEnter_Idle() {
    // idle enter method
  }
  private void StateEnter_Using() {
    // using enter method
  }
  private void StateEnter_Used() {
    // used enter method
  }

  private void StateStay_Idle() {
    // idle stay method
  }
  private void StateStay_Using() {
    // using stay method
  }
  private void StateStay_Used() {
    // used stay method
  }

  private void StateExit_Idle() {
    // idle exit method
  }
  private void StateExit_Using() {
    // using exit method
  }
  private void StateExit_Used() {
    // used exit method
  }
}
