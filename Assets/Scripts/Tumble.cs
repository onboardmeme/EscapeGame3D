using UnityEngine;

public enum TumbleDir {
  UP,
  DOWN
}

public class Tumble : MonoBehaviour {
  public Tumbler tumbler;
  public TumbleDir tumbleDir;

  public void Grab() {
    if (tumbleDir == TumbleDir.UP) {
      tumbler.ChangeState(TumblerState.TUMBLING_UP);
    }
    else {
      tumbler.ChangeState(TumblerState.TUMBLING_DOWN);
    }
  }
}
