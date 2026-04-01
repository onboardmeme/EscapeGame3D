using System;
using System.Collections.Generic;
using UnityEngine;
using State = FsmJumpTableSimpleState;

public enum FsmJumpTableSimpleState {
  IDLE,
  USING,
  USED
}

public class Fsm_JumpTable_Simple : MonoBehaviour {
  public State State { get; private set; }
  private Dictionary<State, Action> stateMethods;

  private void Start() {
    State = State.IDLE;
    stateMethods = new() {
      //{ State.IDLE, State_Idle },
      [State.IDLE] = State_Idle,
      [State.USING] = State_Using,
      [State.USED] = State_Used,
    };
  }

  private void Update() {
    if (stateMethods.ContainsKey(State)) {
      stateMethods[State].Invoke();
    }
  }

  public void ChangeState(State newState) {
    State = newState;
  }

  private void State_Idle() {
    // idle things
  }

  private void State_Using() {
    // using things
  }

  private void State_Used() {
    // used things
  }
}
