using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField]
    private GameObject loseScreen;
    [SerializeField]
    private TextMeshProUGUI speedText;
    [SerializeField]
    private TextMeshProUGUI distanceText;
    [SerializeField]
    private TextMeshProUGUI timeText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        speedText.fontSize = 40;
        distanceText.fontSize = 40;
        timeText.fontSize = 40;
    }
    public void ShowLoseScreen()
    {
        loseScreen.SetActive(true);

    }
    public void UpdateText()
    {
        speedText.text = "Speed: " + PlayerStats.Instance.horizontalSpeed.ToString("F2");
        distanceText.text = "Distance: " + PlayerStats.Instance.totalDistance.ToString("F2");
        timeText.text = "Time: " + PlayerStats.Instance.totalTime.ToString("F2");
    }
    public void ResetStats()
    {
        PlayerStats.Instance.horizontalSpeed = 0;
        PlayerStats.Instance.totalDistance = 0;
        PlayerStats.Instance.totalTime = 0;
    }

}
