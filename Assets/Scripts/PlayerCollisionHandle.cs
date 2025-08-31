using UnityEngine;
using System.Collections;

public class PlayerCollisionHandle : MonoBehaviour
{
    [SerializeField] float immortalTime = 2f;
    [SerializeField] float speedAmountAdjust = -3f;

    bool isImmortal = false;
    string hitStringInAnimator;

    LevelGenerator levelGenerator;
    Animator playerAnimator;

    private void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
        hitStringInAnimator = GetComponent<PlayerMovement>().GetHitString();
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
