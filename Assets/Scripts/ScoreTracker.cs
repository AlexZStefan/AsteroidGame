using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    private static int score;
    private static ScoreTracker instance;

    [SerializeField] private Text text;

    private void Start()
    {
        if (instance == null) instance = this;
        text = GetComponent<Text>();

    }

    private void OnEnable()
    {
        Menu.onRestart += ResetScore;
    }

    private void OnDisable()
    {
        Menu.onRestart -= ResetScore;
    }

    public static void ResetScore()
    {
        score = 0;
        instance.UpdateScore();
    }

    public static void AddScore(int value)
    {
        score += value;
        instance.UpdateScore();
    }

    private void UpdateScore()
    {
        text.text = "Score:"  + score;
    }

}
