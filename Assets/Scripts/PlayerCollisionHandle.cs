using UnityEngine;
using System.Collections;

public class PlayerCollisionHandle : MonoBehaviour
{
    [SerializeField] float immortalTime = 2f;
    [SerializeField] float speedAmountAdjust = -3f;

    bool isImmortal = false;

    LevelGenerator levelGenerator;
    PlayerMovement playerMovement;
    Animator playerAnimator;

    private void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isImmortal)
        {
            levelGenerator.AdjustChunkSpeed(speedAmountAdjust);
            playerAnimator.SetFloat(playerMovement.GetRunSpeedString(), levelGenerator.GetRatioChunkMoveSpeed());

            playerAnimator.SetTrigger(playerMovement.GetHitString());
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
