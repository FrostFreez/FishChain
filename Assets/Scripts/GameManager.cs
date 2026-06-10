using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Lose()
    {
        Debug.Log("Player has lost the game.");
        UIManager.Instance.ShowLoseScreen();
    }
    public void ResetGame()
    {
        PlayerStats.Instance.horizontalSpeed = 0;
        PlayerStats.Instance.totalDistance = 0;
        PlayerStats.Instance.totalTime = 0;
        UIManager.Instance.ShowGameScreen();
    }

}
