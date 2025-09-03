using UnityEngine;

public class Apple : Pickup
{
    [SerializeField] float speedAmountAdjust = 2f;

    LevelGenerator levelGenerator;

    private void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }

    protected override void OnPickup()
    {
        levelGenerator.AdjustChunkSpeed(speedAmountAdjust);
    }
}
