using UnityEngine;

public class Apple : Pickup
{
    [SerializeField] float speedIncreaseAmount = 2f;

    LevelGenerator levelGenerator;

    public void Init(LevelGenerator lg)
    {
        levelGenerator = lg;
    }

    protected override void OnPickup()
    {
        levelGenerator.AdjustChunkSpeed(speedIncreaseAmount);
    }
}
