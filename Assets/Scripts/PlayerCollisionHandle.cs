using UnityEngine;
using System.Collections;

public class PlayerCollisionHandle : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] float immortalTime = 2f;

    const string hitString = "Hit";
    bool isImmortal = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!isImmortal)
        {
            playerAnimator.SetTrigger(hitString);
            StartCoroutine(ImmortalProcess());
        }
        
    }

    IEnumerator ImmortalProcess()
    {
        isImmortal = true;
        yield return new WaitForSecondsRealtime(immortalTime);
        isImmortal = false;
    }
}
