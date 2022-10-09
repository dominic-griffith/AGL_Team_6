using System;
using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// Moves a Rigidbody in a direction.
/// </summary>

public class MovementSystem : MyNamespace.System
{
    [Header("Settings")]
    
    [SerializeField]
    private MovementSettings movementSettings;
    
    [Header("Dependencies")]
    
    [SerializeField] 
    [Tooltip("The rigidbody that our movement force is applied to.")]
    private Rigidbody2D targetRigidbody;
    
    [SerializeField]
    [Tooltip("The forward direction used for calculating a sprint boost.")]
    private Transform lookDirection;
    

    // Internal State
    
    private float _speedMultiplier = 1f;
    private float _forwardSpeedMultiplier = 1f;
    
    [PublicAPI] public float SpeedMultiplier
    {
        get => _speedMultiplier;
        set => _speedMultiplier *= value;
    }
    
    [PublicAPI] public float ForwardSpeedMultiplier
    {
        get => _forwardSpeedMultiplier;
        set => _forwardSpeedMultiplier *= value;
    }

    [PublicAPI] public float CurrentRunningSpeed
    {
        get
        {
            Vector3 velocity = targetRigidbody.velocity;
            velocity.y = 0;
            return velocity.magnitude;
        }
    }
    [PublicAPI] public float CurrentMaxSpeed => movementSettings.MovementSpeed * SpeedMultiplier;
    [PublicAPI] public Vector3 MovementDirection { get; set; } = Vector3.zero;
    [PublicAPI] public Vector2 PlayerDirection { get; set; } = Vector3.zero;
    [PublicAPI] public Rigidbody2D Rigidbody => targetRigidbody;
    
    // Methods

    private void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        ApplyForwardSpeedMultiplier();
        targetRigidbody.velocity = movementSettings.UpdateVelocity(this);
    }

    private void ApplyForwardSpeedMultiplier()
    {
        float forwardSpeed = Vector3.Dot(MovementDirection, lookDirection.forward);
        
        if (forwardSpeed > 0.1f)
            MovementDirection += lookDirection.forward * (forwardSpeed * (ForwardSpeedMultiplier - 1));
    }
}