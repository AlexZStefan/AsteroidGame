using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Menu mainMenu;
    private GameObject player;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }

        if (playerPrefab == null) playerPrefab = Resources.Load<GameObject>("Prefabs/Player");

        Initialise();
        Time.timeScale = 0f;
    }

    private void OnEnable()
    {
        Menu.onStartGame += StartGame;
        Menu.onRestart += StartGame;
    }

    private void OnDisable()
    {
        Menu.onStartGame -= StartGame;
        Menu.onRestart -= StartGame;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        if (player == null) player = Instantiate(playerPrefab);
    }

    public static void GameOver()
    {
        Time.timeScale = 0f;
        instance.mainMenu.EnableGameOverPanel();
    }

    private void Initialise()
    {
        ScreenWrap.SetCameraBounds();
    }
}
