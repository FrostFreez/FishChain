using NUnit.Framework;
using TMPro;
using UnityEngine;

public class LoseScreenController : MonoBehaviour
{
    public TextMeshProUGUI[] textsStats;
    private void Awake()
    {
        textsStats = GetComponentsInChildren<TextMeshProUGUI>();
    }
    public void SetStats(PlayerStats stats)
    {
        textsStats[0].text = $"Your Max Speed was:\n {stats.maxSpeed:F2}";
        textsStats[1].text = $"Your Distance was:\n {stats.totalDistance:F2} m";
        textsStats[2].text = $"Your Time was:\n {stats.totalTime:F2} s";
    }
}
