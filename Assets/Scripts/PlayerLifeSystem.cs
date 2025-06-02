using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeSystem : MonoBehaviour
{
    private static int lives = 3;
    private static PlayerLifeSystem instance;

    [SerializeField] private Text text;

    private void Start()
    {
        if (instance == null) instance = this;
        text = GetComponent<Text>();
    }
    private void OnEnable()
    {
        Menu.onRestart += ResetLives;
    }

    private void OnDisable()
    {
        Menu.onRestart -= ResetLives;
    }
    public static void ResetLives()
    {
        lives = 3;
        instance.UpdateLives();
    }

    public static void DeductLife()
    {
        lives--;
        instance.UpdateLives();

        if (lives == 0) GameManager.GameOver();
    }

    private void UpdateLives()
    {
        text.text = "Lives:" + lives;
    }
}
