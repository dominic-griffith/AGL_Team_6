using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu]
public class MovementSettings : ScriptableObject 
{
    [Header("General Settings")]
    [SerializeField] public float movementSpeed = 3f;
    
    private MovementSystem _movementSystem;
    
    private bool IsAccelerating => _movementSystem.MovementDirection.x != 0;
    public float MovementSpeed => movementSpeed;

    [HideInInspector] public Vector3 playerVelocity;
    
    [PublicAPI] 
    public Vector3 UpdateVelocity(MovementSystem system)
    {
        _movementSystem = system;
        
        playerVelocity = CalculateTargetVelocity();
        return playerVelocity;
    }

    private Vector3 CalculateTargetVelocity()
    {
        Vector3 direction = _movementSystem.MovementDirection;
        float speed = _movementSystem.CurrentMaxSpeed;

        return new Vector3(direction.x * speed, direction.y * speed, 0f );
    }
}