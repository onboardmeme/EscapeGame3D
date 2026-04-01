using UnityEngine;
using State = FsmSwitchSimpleState;

public enum FsmSwitchSimpleState {
  IDLE,
  USING,
  USED
}

public class Fsm_Switch_Simple : MonoBehaviour {
  public State State { get; private set; }
  
  private void Start() {
    State = State.IDLE;
  }

  private void Update() {
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

  public void ChangeState(State newState) {
    State = newState;
  }
}
