using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshProUGUI scoreText;

    int score = 0;

    private void Start()
    {
        scoreText.text = score.ToString();
    }

    public void AddScore(int value)
    {
        if (gameManager.CheckGameover) return;
        score += value;
        scoreText.text = score.ToString();
    }
}
