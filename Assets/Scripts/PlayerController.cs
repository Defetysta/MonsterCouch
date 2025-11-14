using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    
    private Rigidbody2D bodyPhysics2D;
    private float horizontalInput;
    private float verticalInput;

    private void Awake()
    {
        bodyPhysics2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        bodyPhysics2D.linearVelocity = new Vector2(
            horizontalInput * Time.fixedDeltaTime * moveSpeed,
            verticalInput * Time.fixedDeltaTime * moveSpeed);
    }

    public void Move(InputAction.CallbackContext movementContext)
    {
        var movementContextValue = movementContext.ReadValue<Vector2>();
        horizontalInput = movementContextValue.x;
        verticalInput = movementContextValue.y;
    }
}
