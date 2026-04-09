using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Attach to a GameObject in the Win scene.
public class WinScreen : MonoBehaviour {
  public TextMeshProUGUI messageText;
  public string mainSceneName = "finalRoomModels";

  private void Start() {
    messageText.text = "You escaped the Airship";
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
  }

  // Hook this to a "Play Again" button
  public void PlayAgain() {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
    SceneManager.LoadScene(mainSceneName);
  }
}