using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class SlidePuzzleBoard : MonoBehaviour {
  // All 9 SlideBoardTile objects in inspector
  public List<SlideBoardTile> tiles;
  public UnityEvent onPuzzleSolved;

  private int[] grid;     // grid[i] = piece number at slot i, 0 = empty
  private int emptyIndex; // which slot is currently empty
  private bool puzzleActive;

  private static readonly int[] DEFAULT_LAYOUT = { 4, 6, 2, 8, 1, 5, 7, 3, 0 };

  // Sets up the board with the default layout and activates all tiles
  private void Start() {
    grid = (int[])DEFAULT_LAYOUT.Clone();
    emptyIndex = 8;
    puzzleActive = true;

    for (int i = 0; i < tiles.Count; i++) {
      tiles[i].gameObject.SetActive(true);
      tiles[i].SetPieceNumber(grid[i]);
    }
  }

  // Called when a tile is clicked — moves it into the empty slot if it is adjacent
  public void TrySlide(int tileIndex) {
    if (!puzzleActive) return;
    if (!IsAdjacentToEmpty(tileIndex)) return;

    grid[emptyIndex] = grid[tileIndex];
    grid[tileIndex] = 0;

    tiles[emptyIndex].SetPieceNumber(grid[emptyIndex]);
    tiles[tileIndex].SetPieceNumber(0);

    emptyIndex = tileIndex;

    if (IsSolved()) {
      puzzleActive = false;
      onPuzzleSolved.Invoke();
    }
  }

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
