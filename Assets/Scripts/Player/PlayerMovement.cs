using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using static UnityEngine.Rendering.DebugUI;

public class PlayerMovement : Player
{
    [SerializeField] float moveSpeed = 10f;

    [Header("MoveLimit")]
    [SerializeField] float horizontalLimit = 4f;
    [SerializeField] float forwardLimit = 10f;
    [Tooltip("X and Y are the maximum and minimum values for the back limit, respectively.")]
    [SerializeField] Vector2 backLimitRange = new Vector2(0f, -4.5f);
    [SerializeField] float backLimitChangeDuration = 1f;
    [SerializeField] PlayerMoveRange playerMoveRange;

    float backLimit;

    Vector2 movement;
    Rigidbody rb;
    PlayerCollisionHandle playerCollisionHandle;

    private void Awake()
    {
        backLimit = backLimitRange.x;
        rb = GetComponent<Rigidbody>();
        playerCollisionHandle = GetComponent<PlayerCollisionHandle>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
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

    public void AdjustBackLimit(float value)
    {
        StopAllCoroutines();
        StartCoroutine(AdjustBackLimitCoroutine(value));
    }

    IEnumerator AdjustBackLimitCoroutine(float value)
    {
        float elapsedTime = 0f;
        float currenBackLimit = backLimit;
        while (elapsedTime < backLimitChangeDuration)
        {
            elapsedTime += Time.deltaTime;
            backLimit = Mathf.Lerp(currenBackLimit, value, elapsedTime / backLimitChangeDuration);
            playerMoveRange.AdjustBackLimitLine(backLimit);
            yield return null;
        }
    }

    public float GetHorizontalLimit => horizontalLimit;

    public float GetForwardLimit => forwardLimit;

    public float GetBackLimit => backLimit;

    public Vector2 GetBackLimitRange => backLimitRange;
}
