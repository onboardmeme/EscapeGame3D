using UnityEngine;

// Attach to each lever Box (Box 1–5 under switches).
// Tag the Box "Clickable" so CustomCursor detects clicks.
public class LeverSwitch : MonoBehaviour {
  // Set 0–4 in inspector (left to right)
  public int index;
  public LeverPuzzle puzzle;

  // Drag the green lever arm mesh here (the child named "Lever")
  public Transform leverArm;

  // Drag the Renderer of the green part of the lever here
  public Renderer leverRenderer;

  // Tweak these colors in the inspector if needed
  public Color colorOff = new Color(0.05f, 0.25f, 0.05f);
  public Color colorOn  = new Color(0.1f,  1f,    0.1f);

  // Y position of lever arm when on (off uses the modeled resting position)
  public float yPosOn = 0.03f;

  public bool IsLit { get; private set; }

  private Vector3 posOff;

  private void Start() {
    posOff = leverArm != null ? leverArm.localPosition : Vector3.zero;
    IsLit = false;
    if (leverRenderer != null)
      leverRenderer.material.SetColor("_BaseColor", colorOff);
  }

  // Called by CustomCursor.SendMessage("Grab")
  public void Grab() {
    puzzle.Toggle(index);
  }

  public void SetLit(bool lit) {
    IsLit = lit;
    SoundManager.Play(SoundType.LEVER);

    if (leverRenderer != null)
      leverRenderer.material.SetColor("_BaseColor", lit ? colorOn : colorOff);

    if (leverArm != null) {
      Vector3 pos = posOff;
      if (lit) pos.y = yPosOn;
      leverArm.localPosition = pos;
    }
  }
}
