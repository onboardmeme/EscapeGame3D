using UnityEngine;

public class InventoryItem_Key : InventoryItem{
  public GameObject door;

  public override void Use() {
    door.SetActive(false);
  }
}
