using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour {
  public Sprite sprHover;
  public Color clrHover;

  private InputSystem_Actions input;
  private Sprite sprDefault;
  private Color clrDefault;
  private Image img;
  private Renderer lastRenHoveredOver;
  private Color defColorOfLastHoveredObj;

  private static readonly Color CLR_HOVERED_OVER_OBJ = Color.cyan;

  private void Awake() {
    img = GetComponent<Image>();
    sprDefault = img.sprite;
    clrDefault = img.color;
    input = new();
    input.Enable();
    input.UI.Enable();
    lastRenHoveredOver = null;
  }

  private void Update() {
    img.sprite = sprDefault;
    img.color = clrDefault;
    if (lastRenHoveredOver != null) {
      lastRenHoveredOver.material.color = defColorOfLastHoveredObj;
      lastRenHoveredOver = null;
    }
    var ray = Camera.main.ScreenPointToRay(transform.position);
    if (Physics.Raycast(ray, out RaycastHit hit, 100)) {
      if (hit.collider.CompareTag("Clickable")) {
        img.sprite = sprHover;
        img.color = clrHover;
        var ren = hit.collider.gameObject.GetComponent<Renderer>();
        if (ren != null) {
          lastRenHoveredOver = ren;
          defColorOfLastHoveredObj = ren.material.color;
          //ren.material.color = CLR_HOVERED_OVER_OBJ;
          ren.material.SetColor("_BaseColor", CLR_HOVERED_OVER_OBJ);
        }
      }
      if (input.UI.Click.WasPressedThisFrame()) {
        hit.collider.gameObject.SendMessage("Grab", SendMessageOptions.DontRequireReceiver);
      }
    }
  }
}
