using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TimerText;       // HUD label (UI)
    [SerializeField] TextMeshPro RoomTimerText;       // World-space label in the room
    [SerializeField] float remainingTime;

    void Update(){
        if(remainingTime > 0){
            remainingTime -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

            TimerText.text = timeString;
            if (RoomTimerText != null) RoomTimerText.text = timeString;
        } else {
            remainingTime = 0;
            TimerText.text = "You Lose";
            if (RoomTimerText != null) RoomTimerText.text = "You Lose";
            SceneManager.LoadScene("GameOver");
        }
    }
}