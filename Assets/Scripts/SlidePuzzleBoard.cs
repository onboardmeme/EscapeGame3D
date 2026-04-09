using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using State = SlidePuzzleState;

public enum SlidePuzzleState {
  IDLE,
  USING,
  USED
}

public class SlidePuzzleBoard : MonoBehaviour {
  // All 9 SlideBoardTile objects in inspector
  public List<SlideBoardTile> tiles;
  public UnityEvent onPuzzleSolved;

  public State State { get; private set; }
  private HashSet<KeyValuePair<State, State>> allowedTransitions;
  private Dictionary<State, Action> stateEnterMethods;
  private Dictionary<State, Action> stateStayMethods;
  private Dictionary<State, Action> stateExitMethods;

  private int[] grid;     // grid[i] = piece number at slot i, 0 = empty
  private int emptyIndex; // which slot is currently empty

  private static readonly int[] DEFAULT_LAYOUT = { 4, 6, 2, 8, 1, 5, 7, 3, 0 };

  // Sets up the FSM and activates the puzzle immediately
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

  // Called when a tile is clicked — moves it into the empty slot if it is adjacent
  public void TrySlide(int tileIndex) {
    if (State != State.USING) return;
    if (!IsAdjacentToEmpty(tileIndex)) return;

    grid[emptyIndex] = grid[tileIndex];
    grid[tileIndex] = 0;

    tiles[emptyIndex].SetPieceNumber(grid[emptyIndex]);
    tiles[tileIndex].SetPieceNumber(0);

    emptyIndex = tileIndex;

    if (IsSolved()) ChangeState(State.USED);
  }

  // --- State Enter ---
  private void StateEnter_Idle() { }

  private void StateEnter_Using() {
    // Sets up the board with the default layout and activates all tiles
    grid = (int[])DEFAULT_LAYOUT.Clone();
    emptyIndex = 8;
    for (int i = 0; i < tiles.Count; i++) {
      tiles[i].gameObject.SetActive(true);
      tiles[i].SetPieceNumber(grid[i]);
    }
  }

  private void StateEnter_Used() {
    // Puzzle is solved — fire the event
    onPuzzleSolved.Invoke();
  }

  // --- State Stay ---
  private void StateStay_Idle()  { }
  private void StateStay_Using() { }
  private void StateStay_Used()  { }

  // --- State Exit ---
  private void StateExit_Idle()  { }
  private void StateExit_Using() { }
  private void StateExit_Used()  { }

  // Returns true if the tile at index is directly next to the empty slot
  private bool IsAdjacentToEmpty(int index) {
    int row = index / 3, col = index % 3;
    int eRow = emptyIndex / 3, eCol = emptyIndex % 3;
    return Mathf.Abs(row - eRow) + Mathf.Abs(col - eCol) == 1;
  }

  // Solved state: [1,2,3,4,5,6,7,8,0]
  private bool IsSolved() {
    for (int i = 0; i < 8; i++) {
      if (grid[i] != i + 1) return false;
    }
    return grid[8] == 0;
  }
}