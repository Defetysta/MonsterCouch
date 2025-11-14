using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private string interactableTagName;
    
    private Rigidbody2D bodyPhysics2D;
    private float horizontalInput;
    private float verticalInput;
    private TagHandle interactableTagHandle;

    private void Awake()
    {
        bodyPhysics2D = GetComponent<Rigidbody2D>();
        interactableTagHandle = TagHandle.GetExistingTag(interactableTagName);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(interactableTagHandle))
        {
            var enemyBehaviour = other.GetComponent<EnemyBehaviour>();
            if (enemyBehaviour != null)
            {
                enemyBehaviour.SetGetCaughtState(true);
            }
        }
    }
}
