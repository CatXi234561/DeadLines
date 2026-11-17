using UnityEngine;
using UnityEngine.Events;

public class CountdownTimer : MonoBehaviour
{
[SerializeField] private SevenSegmentDigit[] digits;
[Tooltip("Countdown duration in seconds")]
[SerializeField] private float countdownDuration = 900f; // 15 minutes
//private float previousTimeRemaining = -1f;
private float timeRemaining = 0f;
private bool isRunning;
[SerializeField] private UnityEvent OnFinished = new UnityEvent();


    public void StartTimer()
    {
        timeRemaining = countdownDuration;
        isRunning = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (!isRunning) return;

        if (digits.Length < 4)
        {Debug.LogError("CountdownTimer requires at least 4 digits assigned for MM:SS display.");
        return;}
        // if (timeRemaining == previousTimeRemaining) return; // Only update when time changes
        // previousTimeRemaining = timeRemaining; // Store the last time remaining to avoid unnecessary updates

        timeRemaining = Mathf.Max(0, countdownDuration - Time.timeSinceLevelLoad);
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        // digits in MM:SS
        if (digits.Length >= 4)
        {
            digits[0].SetDigit(minutes / 10);
            digits[1].SetDigit(minutes % 10);
            digits[2].SetDigit(seconds / 10);
            digits[3].SetDigit(seconds % 10);
        }

        if (timeRemaining <= 0)
        {
            // Countdown finished, you can add any additional logic here if needed
            OnFinished.Invoke();
            isRunning = false;
            return;
        }
    }
    
}
