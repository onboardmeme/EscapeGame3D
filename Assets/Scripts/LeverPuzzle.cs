using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using State = LeverPuzzleState;

public enum LeverPuzzleState {
  IDLE,
  USING,
  USED
}

// Attach to the "switches" parent GameObject.
public class LeverPuzzle : MonoBehaviour {
  // Drag all 5 LeverSwitch boxes here in order (left to right)
  public List<LeverSwitch> levers;
  public UnityEvent onPuzzleSolved;

  public State State { get; private set; }
  private HashSet<KeyValuePair<State, State>> allowedTransitions;
  private Dictionary<State, Action> stateEnterMethods;
  private Dictionary<State, Action> stateStayMethods;
  private Dictionary<State, Action> stateExitMethods;

  private void Start() {
    State = State.IDLE;

    allowedTransitions = new() {
      new(State.IDLE,  State.USING),
      new(State.USING, State.USED),
    };

    stateEnterMethods = new() {
      [State.IDLE]  = StateEnter_Idle,
      [State.USING] = StateEnter_Using,
      [State.USED]  = StateEnter_Used,
    };
    stateStayMethods = new() {
      [State.IDLE]  = StateStay_Idle,
      [State.USING] = StateStay_Using,
      [State.USED]  = StateStay_Used,
    };
    stateExitMethods = new() {
      [State.IDLE]  = StateExit_Idle,
      [State.USING] = StateExit_Using,
      [State.USED]  = StateExit_Used,
    };

    ChangeState(State.USING);
  }

  private void Update() {
    if (stateStayMethods.ContainsKey(State))
      stateStayMethods[State].Invoke();
  }

  public void ChangeState(State newState) {
    if (allowedTransitions.Contains(new(State, newState))) {
      stateExitMethods[State].Invoke();
      State = newState;
      stateEnterMethods[State].Invoke();
    }
  }

  // Toggles the clicked lever and its immediate neighbors (lights-out rules)
  public void Toggle(int index) {
    if (State != State.USING) return;

    for (int i = index - 1; i <= index + 1; i++) {
      if (i >= 0 && i < levers.Count)
        levers[i].SetLit(!levers[i].IsLit);
    }

    if (IsSolved()) ChangeState(State.USED);
  }

  // --- State Enter ---
  private void StateEnter_Idle()  { }
  private void StateEnter_Using() { }
  private void StateEnter_Used()  { onPuzzleSolved.Invoke(); }

  // --- State Stay ---
  private void StateStay_Idle()   { }
  private void StateStay_Using()  { }
  private void StateStay_Used()   { }

  // --- State Exit ---
  private void StateExit_Idle()   { }
  private void StateExit_Using()  { }
  private void StateExit_Used()   { }

  private bool IsSolved() {
    foreach (var lever in levers)
      if (!lever.IsLit) return false;
    return true;
  }
}
