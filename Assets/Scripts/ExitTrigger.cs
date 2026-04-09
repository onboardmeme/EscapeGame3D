using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour {
  public string winSceneName = "Win";

  private void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Player")) {
      SceneManager.LoadScene(winSceneName);
    }
  }
}
