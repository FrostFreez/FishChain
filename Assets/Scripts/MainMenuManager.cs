using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("JuanScreen");
    }
    public void HighScoreMenu()
    {
        SceneManager.LoadScene("HighScoreScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
