using System;
using UnityEngine;
using UnityEngine.Events;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private SevenSegmentDigit[] digits;
    [Tooltip("Countdown duration in seconds")]
    [SerializeField] private float countdownDuration = 900f;

    private float timeRemaining;
    private bool isRunning;
    [SerializeField] private UnityEvent OnFinished = new UnityEvent();

    public event Action Finished;
    public float TimeRemaining => timeRemaining;
    public bool IsRunning => isRunning;

    public void StartTimer()
    {
        timeRemaining = Mathf.Max(0f, countdownDuration);
        isRunning = true;
        UpdateDisplay();

        if (timeRemaining <= 0f)
            Finish();
    }

    public void StopTimer()
    {
        isRunning = false;
    }
    public void ResetTimer()
    {
        timeRemaining = Mathf.Max(0f, countdownDuration);
        isRunning = false;
        UpdateDisplay();
    }

    private void Update()
    {
        if (!isRunning)
            return;

        timeRemaining = Mathf.Max(0f, timeRemaining - Time.deltaTime);
        UpdateDisplay();

        if (timeRemaining <= 0f)
            Finish();
    }

    private void UpdateDisplay()
    {
        if (digits == null || digits.Length < 4)
        {
            Debug.LogError("CountdownTimer needs four SevenSegmentDigit references for an MM:SS display.", this);
            return;
        }

        int totalSeconds = Mathf.CeilToInt(timeRemaining);
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        digits[0].SetDigit((minutes / 10) % 10);
        digits[1].SetDigit(minutes % 10);
        digits[2].SetDigit(seconds / 10);
        digits[3].SetDigit(seconds % 10);
    }

    private void Finish()
    {
        if (!isRunning)
            return;

        isRunning = false;
        OnFinished.Invoke();
        Finished?.Invoke();
    }
}
