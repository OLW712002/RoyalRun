using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;

    [Header("MoveLimit")]
    [SerializeField] float horizontalLimit = 4f;
    [SerializeField] float forwardLimit = 10f;
    [SerializeField] float backLimit = -2f;

    const string runSpeedStringInAnimator = "RunSpeed";
    const string hitStringInAnimator = "Hit";

    Vector2 movement;
    Rigidbody rb;
    PlayerCollisionHandle playerCollisionHandle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCollisionHandle = GetComponent<PlayerCollisionHandle>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        //transform.Translate(Vector3.right * movement.x * moveSpeed * Time.deltaTime);
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector3 currentPos = rb.position;
        Vector3 moveDir = new Vector3(movement.x, 0f, movement.y);
        Vector3 newPosition;
        if (playerCollisionHandle.PlayerIsImmortal())
        {
            newPosition = currentPos + moveDir * moveSpeed / 5 * Time.fixedDeltaTime;
        }
        else newPosition = currentPos + moveDir * moveSpeed * Time.fixedDeltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -horizontalLimit, horizontalLimit);
        newPosition.z = Mathf.Clamp(newPosition.z, backLimit, forwardLimit);
        rb.MovePosition(newPosition);
    }

    public string GetRunSpeedString()
    {
        return runSpeedStringInAnimator;
    }

    public string GetHitString()
    {
        return hitStringInAnimator;
    }
}
