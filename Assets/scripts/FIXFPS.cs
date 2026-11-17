using System.Linq;
using UnityEngine;

public class FIXFPS : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 90;
        if (OVRPlugin.systemDisplayFrequenciesAvailable.Contains(90.0f))
        {
            OVRPlugin.systemDisplayFrequency = 90.0f;
        }
    }
}