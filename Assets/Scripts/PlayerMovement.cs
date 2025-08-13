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
        Debug.Log(movement);
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
        Vector3 rawMovePosition = currentPos + moveDir * moveSpeed * Time.fixedDeltaTime;
        float xClamp = Mathf.Clamp(rawMovePosition.x, -horizontalLimit, horizontalLimit);
        float yClamp = Mathf.Clamp(rawMovePosition.z, backLimit, forwardLimit);
        rb.MovePosition(new Vector3(xClamp, 0f, yClamp));
    }
}
