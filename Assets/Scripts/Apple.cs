using UnityEngine;

public class Apple : Pickup
{
    [SerializeField] float speedAmountAdjust = 2f;

    const string playerString = "Player";

    LevelGenerator levelGenerator;
    Animator playerAnimator;

    private void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
        playerAnimator = GameObject.Find(playerString).GetComponentInChildren<Animator>();
    }

    protected override void OnPickup()
    {
        levelGenerator.AdjustChunkSpeed(speedAmountAdjust);
        playerAnimator.SetFloat("RunSpeed", levelGenerator.GetRatioChunkMoveSpeed());
    }
}
