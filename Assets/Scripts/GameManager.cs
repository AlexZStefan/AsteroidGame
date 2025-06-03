using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Menu mainMenu;
    [SerializeField] private GameObject enemyPrefab;
    private GameObject player;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }

        if (playerPrefab == null) playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
        if (enemyPrefab == null) enemyPrefab = Resources.Load<GameObject>("Prefabs/EnemySpaceShip");

        Initialise();
        Time.timeScale = 0f;
    }

    // this is for testing
    public void AddSmartRocket()
    {
        player.GetComponent<ShipFire>().EquipSmartMisile();
    }

    // this is for testing
    public void AddEnemy()
    {
        Instantiate(enemyPrefab);
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
