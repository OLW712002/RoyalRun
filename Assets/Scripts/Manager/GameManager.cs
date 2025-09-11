using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] GameObject gameoverText;
    [SerializeField] float startTime = 5f;
    [SerializeField] float maxTimeLeft = 30f;

    float timeLeft;
    bool isGameover = false;

    public bool CheckGameover => isGameover;

    void Start()
    {
        timeLeft = startTime;
    }

    void Update()
    {
        if (isGameover) return;
        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString("F1");
        if (timeLeft <= 0f)
        {
            isGameover = true;
            Time.timeScale = 0.1f;
            playerMovement.enabled = false;
            gameoverText.SetActive(true);
        }
    }

    public void IncreaseTimeLeft(float value)
    {
        timeLeft += value;
        if (timeLeft >= maxTimeLeft) timeLeft = maxTimeLeft;
    }
}
