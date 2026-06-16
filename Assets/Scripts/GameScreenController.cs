using TMPro;
using UnityEngine;

public class GameScreenController : MonoBehaviour
{
    public TextMeshProUGUI[] textsStats;
    
    private void Awake()
    {
        textsStats = GetComponentsInChildren<TextMeshProUGUI>(); 

    }
    public void SetStats(PlayerStats stats)
    {
        textsStats[0].text = $"Speed: {stats.horizontalSpeed:F2}";
        textsStats[1].text = $"Distance: {stats.totalDistance:F2} m";
        textsStats[2].text = $"Time: {stats.totalTime:F2} s";
    }
}
