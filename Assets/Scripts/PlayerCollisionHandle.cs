using UnityEngine;

public class PlayerCollisionHandle : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;

    private void OnCollisionEnter(Collision collision)
    {
        playerAnimator.SetTrigger("Hit");
        Debug.Log(collision.gameObject.name);
    }
}
