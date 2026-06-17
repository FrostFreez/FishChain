using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private PlayerController player;

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
        player = FindAnyObjectByType<PlayerController>();

    }

    public void Lose()
    {
        PlayerStats.Instance.dead = true;
        UIManager.Instance.ShowLoseScreen();
        player.anim.SetTrigger("Die");
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
