using UnityEngine;

public class Apple : Pickup
{
    [SerializeField] float speedIncreaseAmount = 2f;
    [SerializeField] float timeAmount = 10f;

    LevelGenerator levelGenerator;
    GameManager gameManager;

    public void Init(LevelGenerator lg, GameManager gm)
    {
        levelGenerator = lg;
        gameManager = gm;
    }

    protected override void OnPickup()
    {
        levelGenerator.AdjustChunkSpeed(speedIncreaseAmount);
        gameManager.IncreaseTimeLeft(timeAmount);
    }
}
