using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] GameObject gameoverText;
    [SerializeField] float startTime = 5f;

    float timeLeft;
    bool isGameover = false;

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
}
