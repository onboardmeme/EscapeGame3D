using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using State = TumblerState;

public enum TumblerState {
  IDLE,
  TUMBLING_UP,
  TUMBLING_DOWN,
  DISABLED
}

public class Tumbler : MonoBehaviour {
  public int numberOfSides;

  public State State { get; private set; }
  private HashSet<KeyValuePair<State, State>> allowedTransitions;

  private Dictionary<State, Action> stateEnterMethods;
  private Dictionary<State, Action> stateStayMethods;
  private Dictionary<State, Action> stateExitMethods;

  private int number;
  private Quaternion curRot;
  private Quaternion targetRot;
  private float t;

  private Coroutine tumblingCoroutine;

  private void Start() {
    State = State.IDLE;
    tumblingCoroutine = null;
    number = 0;

    allowedTransitions = new() {
      new(State.IDLE, State.TUMBLING_UP),
      new(State.IDLE, State.TUMBLING_DOWN),
      new(State.TUMBLING_UP, State.IDLE),
      new(State.TUMBLING_DOWN, State.IDLE),
      new(State.IDLE, State.DISABLED),
    };

    stateEnterMethods = new() {
      [State.IDLE] = StateEnter_Idle,
      [State.TUMBLING_UP] = StateEnter_TumblingUp,
      [State.TUMBLING_DOWN] = StateEnter_TumblingDown,
      [State.DISABLED] = StateEnter_Disabled,
    };
    stateStayMethods = new() {
      [State.IDLE] = StateStay_Idle,
      [State.TUMBLING_UP] = StateStay_TumblingUp,
      [State.TUMBLING_DOWN] = StateStay_TumblingDown,
      [State.DISABLED] = StateStay_Disabled,
    };
    stateExitMethods = new() {
      [State.IDLE] = StateExit_Idle,
      [State.TUMBLING_UP] = StateExit_TumblingUp,
      [State.TUMBLING_DOWN] = StateExit_TumblingDown,
      [State.DISABLED] = StateExit_Disabled,
    };
  }

  private void Update() {
    if (stateStayMethods.ContainsKey(State)) {
      stateStayMethods[State].Invoke();
    }
  }

  private IEnumerator Tumble() {
    while (t < 1) {
      t += 1 * Time.deltaTime;
      transform.rotation = Quaternion.Lerp(curRot, targetRot, t);
      yield return new WaitForEndOfFrame();
    }
    transform.rotation = targetRot;
    ChangeState(State.IDLE);
  }

  //public int GetNumber() {
  //  return number + 1;
  //}
  public int GetNumber() => number + 1;

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
  private void StateEnter_TumblingUp() {
    curRot = transform.rotation;
    targetRot = curRot * Quaternion.Euler(Vector3.up * (360 / numberOfSides));
    t = 0;
    SoundManager.Play(SoundType.TUMBLE);
    tumblingCoroutine = StartCoroutine(Tumble());
  }
  private void StateEnter_TumblingDown() {
    curRot = transform.rotation;
    targetRot = curRot * Quaternion.Euler(Vector3.down * (360 / numberOfSides));
    t = 0;
    SoundManager.Play(SoundType.TUMBLE);
    //StopCoroutine(tumblingCoroutine);
    //StopAllCoroutines();
    StartCoroutine(Tumble());
  }
  private void StateEnter_Disabled() {
    // used enter method
  }

  private void StateStay_Idle() {
    // idle enter method
  }
  private void StateStay_TumblingUp() {
  }
  private void StateStay_TumblingDown() {
  }
  private void StateStay_Disabled() {
    // used enter method
  }

  private void StateExit_Idle() {
    // idle enter method
  }
  private void StateExit_TumblingUp() {
    number++;
    number %= numberOfSides;
    print($"number from tumbler is: {GetNumber()} from {name}");
  }
  private void StateExit_TumblingDown() {
    number--;
    if (number < 0) {
      number += numberOfSides;
    }
    print($"number from tumbler is: {GetNumber()} from {name}");
  }
  private void StateExit_Disabled() {
    // used enter method
  }
}
