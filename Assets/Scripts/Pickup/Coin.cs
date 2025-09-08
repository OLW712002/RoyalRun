using UnityEngine;

public class Coin : Pickup
{
    [SerializeField] int score = 100;

    ScoreKeeper scoreKeeper;

    private void Start()
    {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }

    protected override void OnPickup()
    {
        scoreKeeper.AddScore(score);
    }
}
