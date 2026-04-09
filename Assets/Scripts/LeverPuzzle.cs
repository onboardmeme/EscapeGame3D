using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Attach to the "switches" parent GameObject.
public class LeverPuzzle : MonoBehaviour {
  // Drag all 5 LeverSwitch boxes here in order (left to right)
  public List<LeverSwitch> levers;

  // Wire this to whatever opens when the puzzle is solved
  public UnityEvent onPuzzleSolved;

  // Toggles the clicked lever and its immediate neighbors (lights-out rules)
  public void Toggle(int index) {
    for (int i = index - 1; i <= index + 1; i++) {
      if (i >= 0 && i < levers.Count)
        levers[i].SetLit(!levers[i].IsLit);
    }

    if (IsSolved()) onPuzzleSolved.Invoke();
  }

  private bool IsSolved() {
    foreach (var lever in levers)
      if (!lever.IsLit) return false;
    return true;
  }
}
