using TMPro;
using UnityEngine;

// Attach to each of the 9 tile GameObjects on the board.
// Tiles find their board automatically via parent.
public class SlideBoardTile : MonoBehaviour {
  // Set 0–8 in inspector (row-major: top-left=0, top-right=2, bottom-right=8)
  public int tileIndex;

  private SlidePuzzleBoard board;
  private TextMeshPro label;
  private new Renderer renderer;
  private int pieceNum;

  private void Awake() {
    board = GetComponentInParent<SlidePuzzleBoard>();
    label = GetComponentInChildren<TextMeshPro>();
    renderer = GetComponent<Renderer>();
  }

  // Updates the tile's number, hides the tile if num is 0 (empty slot), otherwise shows it with the correct label
  public void SetPieceNumber(int num) {
    pieceNum = num;
    bool haspiece = num != 0;
    if (renderer != null) renderer.enabled = haspiece;
    if (label != null) label.text = haspiece ? num.ToString() : "";
  }
  public void Grab() {
    if (pieceNum != 0) board.TrySlide(tileIndex);
  }
}
