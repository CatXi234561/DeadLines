using System.Linq;
using UnityEngine;

public class FIXFPS : MonoBehaviour
{
    void Start()
    {
        if (OVRPlugin.systemDisplayFrequenciesAvailable.Contains(90.0f))
        {
            OVRPlugin.systemDisplayFrequency = 90.0f;
        }
    }
}