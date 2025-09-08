using UnityEngine;

public class Coin : Pickup
{
    [SerializeField] int scoreAmount = 100;

    ScoreKeeper scoreKeeper;

    public void Init(ScoreKeeper sk)
    {
        scoreKeeper = sk;
    }

    protected override void OnPickup()
    {
        scoreKeeper.AddScore(scoreAmount);
    }
}
