using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene changing

public class Winner : MonoBehaviour
{
    public string Winning; // Type the scene name in the Inspector

    // For physical touch
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(Winning);
        }
    }
}