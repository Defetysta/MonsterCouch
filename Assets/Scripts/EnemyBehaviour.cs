using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private float enemyMoveSpeed;

    [SerializeField] 
    private float changeDirectionSeconds;
    
    private Vector2 areaHorizontalConstraints;
    private Vector2 areaVerticalConstraints;
    private bool isCaught;
    private bool isFleeing;

    private Vector3 randomDirection;
    private float directionRandomizationCountdown;
    
    public void Init(Vector2 areaHorizontalConstraints, Vector2 areaVerticalConstraints)
    {
        this.areaHorizontalConstraints = areaHorizontalConstraints;
        this.areaVerticalConstraints = areaVerticalConstraints;

        RandomizeDirection();
    }

    private void RandomizeDirection()
    {
        randomDirection = Random.insideUnitCircle.normalized * (enemyMoveSpeed * Time.fixedDeltaTime);
        directionRandomizationCountdown = 0f;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (isCaught == true)
        {
            return;
        }

        if (isFleeing == true)
        {
            FleeAway();
            
            return;
        }

        RunInRandomDirection();
    }

    private void RunInRandomDirection()
    {
        if (directionRandomizationCountdown > changeDirectionSeconds)
        {
            RandomizeDirection();
        }

        Vector3 newPosition = transform.position + randomDirection;
        if (newPosition.x < areaHorizontalConstraints.x || newPosition.x > areaHorizontalConstraints.y)
        {
            newPosition.x = transform.position.x - randomDirection.x;
        }
        
        if (newPosition.y < areaVerticalConstraints.x || newPosition.y > areaVerticalConstraints.y)
        {
            newPosition.y = transform.position.y - randomDirection.y;
        }

        transform.position = newPosition;
        directionRandomizationCountdown += Time.fixedDeltaTime;
    }
    
    private void FleeAway()
    {
        throw new System.NotImplementedException();
    }

    public void SetGetCaughtState(bool desiredState)
    {
        isCaught = desiredState;
    }
    
    public void SetIsFleeingState(bool desiredState)
    {
        isFleeing = desiredState;
    }
}
