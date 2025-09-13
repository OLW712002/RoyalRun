using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] float timeAmount = 5f;

    const string playerString = "Player";

    GameManager gameManager;

    public void Init(GameManager gm)
    {
        gameManager = gm;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerString))
        {
            gameManager.IncreaseTimeLeft(timeAmount);
        }
    }
}
