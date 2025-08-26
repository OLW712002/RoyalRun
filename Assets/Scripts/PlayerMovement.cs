using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;

    [Header("MoveLimit")]
    [SerializeField] float horizontalLimit = 4f;
    [SerializeField] float forwardLimit = 10f;
    [SerializeField] float backLimit = -2f;

    Vector2 movement;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
        Vector3 newPosition = currentPos + moveDir * moveSpeed * Time.fixedDeltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -horizontalLimit, horizontalLimit);
        newPosition.z = Mathf.Clamp(newPosition.z, backLimit, forwardLimit);
        rb.MovePosition(newPosition);
    }
}
