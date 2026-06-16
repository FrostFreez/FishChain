using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public LoseScreenController loseScreen;
    public GameScreenController gameScreen;

    private void Start()
    {
        ShowGameScreen();
    }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Update()
    {
        if (gameScreen.gameObject.activeSelf)
        {
            gameScreen.SetStats(PlayerStats.Instance);
        }
    }
    public void ShowGameScreen()
    {
        loseScreen.gameObject.SetActive(false);
        gameScreen.gameObject.SetActive(true);
    }
    public void ShowLoseScreen()
    {
        loseScreen.gameObject.SetActive(true);
        gameScreen.gameObject.SetActive(false);
        loseScreen.SetStats(PlayerStats.Instance);
    }
}
