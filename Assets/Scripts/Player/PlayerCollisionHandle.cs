using UnityEngine;
using System.Collections;

public class PlayerCollisionHandle : Player
{
    [SerializeField] float immortalTime = 2f;
    [SerializeField] float speedAmountAdjust = -3f;

    bool isImmortal = false;

    LevelGenerator levelGenerator;
    Animator playerAnimator;

    private void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isImmortal)
        {
            levelGenerator.AdjustChunkSpeed(speedAmountAdjust);
            playerAnimator.SetTrigger(hitStringInAnimator);
            StartCoroutine(ImmortalProcess());
        }
        
    }

    IEnumerator ImmortalProcess()
    {
        isImmortal = true;
        yield return new WaitForSecondsRealtime(immortalTime);
        isImmortal = false;
    }

    public bool PlayerIsImmortal()
    {
        return isImmortal;
    }
}
