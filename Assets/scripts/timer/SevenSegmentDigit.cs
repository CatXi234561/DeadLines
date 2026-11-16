using UnityEngine;

public class SevenSegmentDigit : MonoBehaviour
{
    // Assign in Inspector in this order: A, B, C, D, E, F, G
    [SerializeField] private GameObject[] segments = new GameObject[7];

    // Bit order: A B C D E F G (bit 6 = A ... bit 0 = G)
    private static readonly byte[] DigitMasks =
    {
        0b1111110, // 0
        0b0110000, // 1
        0b1101101, // 2
        0b1111001, // 3
        0b0110011, // 4
        0b1011011, // 5
        0b1011111, // 6
        0b1110000, // 7
        0b1111111, // 8
        0b1111011  // 9
    };

    public void SetDigit(int value)
    {
        if (value < 0 || value > 9)
        {
            SetAll(false);
            return;
        }

        byte mask = DigitMasks[value];

        for (int i = 0; i < 7; i++)
        {
            // i=0 should control A (bit 6), i=6 controls G (bit 0)
            bool on = ((mask >> (6 - i)) & 1) == 1;
            segments[i].SetActive(on);
        }
    }

    public void SetAll(bool on)
    {
        for (int i = 0; i < segments.Length; i++)
        {
            segments[i].SetActive(on);
        }
    }
}