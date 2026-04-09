using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Attach to a GameObject in the GameOver scene.
public class GameOverScreen : MonoBehaviour {
  public TextMeshProUGUI messageText;
  public string mainSceneName = "finalRoomModels";

  private void Start() {
    messageText.text = "You didn't escape the Airship";
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
  }

  // Hook this to a "Try Again" button
  public void TryAgain() {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
    SceneManager.LoadScene(mainSceneName);
  }
}