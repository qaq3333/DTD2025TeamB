using System;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI timerText;

    void Update()
    {
        timerText.text = TimeSpan.FromSeconds(Time.timeSinceLevelLoad).ToString(@"mm\:ss");
    }
}
