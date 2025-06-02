using UnityEngine;


public class Menu : MonoBehaviour
{
    public delegate void RestartGameEvent(); 
    public static event RestartGameEvent onRestart;

    public delegate void StartGameEvent();
    public static event StartGameEvent onStartGame;

    [SerializeField] GameObject gameOverPanel; 

    public void StartGame()
    {
        onStartGame();
    }

    public void Restart()
    {
        onRestart();
        onStartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EnableGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
}
