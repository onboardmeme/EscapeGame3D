using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TumblerPuzzle : MonoBehaviour {
  // set in inspector
  public List<Tumbler> tumblers;

  // private fields
  private Animator safeDoorAnimator;
  private int[] code;

  private void Start() {
    safeDoorAnimator = GetComponent<Animator>();
    code = new[] { 3, 4, 6, 1 };
  }

  private void Update() {
    bool comboCorrect = true;
    for (int i = 0; i < tumblers.Count; i++) {
      comboCorrect &= (tumblers[i].GetNumber() == code[i]);
    }
    if (comboCorrect) {
      //foreach (var tumbler in tumblers) {
      //  tumbler.ChangeState(TumblerState.DISABLED);
      //}
      tumblers.ForEach(tumbler => tumbler.ChangeState(TumblerState.DISABLED));
      safeDoorAnimator.SetTrigger("Door Open");
    }

    //bool[] correctNumbers = new bool[] {
    //  (tumblers[0].GetNumber() == code[0]),
    //  (tumblers[1].GetNumber() == code[1]),
    //  (tumblers[2].GetNumber() == code[2]),
    //  (tumblers[3].GetNumber() == code[3])
    //};
    //bool comboCorrect = correctNumbers.All();
  }
}
