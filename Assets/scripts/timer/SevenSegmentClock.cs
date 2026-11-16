using System;
using UnityEngine;

public class SevenSegmentClock : MonoBehaviour
{
    // Assign left to right: H1, H2, M1, M2, S1, S2 (or only first 4 for HH:MM)
    [SerializeField] private SevenSegmentDigit[] digits;
    [SerializeField] private GameObject colonObject; // optional blinking colon
    [SerializeField] private bool use24Hour = true;
    [SerializeField] private bool showSeconds = true;

    private int _lastSecond = -1;

    private void Update()
    {
        DateTime now = DateTime.Now;

        // Update only when second changes
        if (now.Second == _lastSecond) return;
        _lastSecond = now.Second;

        int hour = use24Hour ? now.Hour : ((now.Hour % 12 == 0) ? 12 : now.Hour % 12);
        int minute = now.Minute;
        int second = now.Second;

        // Need at least 4 digits for HH:MM
        if (digits.Length >= 4)
        {
            digits[0].SetDigit(hour / 10);
            digits[1].SetDigit(hour % 10);
            digits[2].SetDigit(minute / 10);
            digits[3].SetDigit(minute % 10);
        }

        // Optional seconds if you assigned 6 digits
        if (showSeconds && digits.Length >= 6)
        {
            digits[4].SetDigit(second / 10);
            digits[5].SetDigit(second % 10);
        }

        if (colonObject != null)
        {
            // Blink every second
            colonObject.SetActive(second % 2 == 0);
        }
    }
}