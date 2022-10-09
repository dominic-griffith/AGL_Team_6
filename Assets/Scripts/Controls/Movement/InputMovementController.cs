using UnityEngine;

/// <summary>
/// Controls the movement system with input.
/// </summary>

public class InputMovementController : InputController<MovementSystem>
{
    /*[Header("Dependencies")]
    
    [SerializeField]
    [Tooltip("The transform that points in the forward arm direction.")]
    private Transform armTransform;*/
    
    [HideInInspector]
    public float forwardAxis;
    [HideInInspector]
    public float rightAxis;
        
    private bool _isInstanceNotNull;

    private void Update()
    {
        forwardAxis = CalculateAxis(controls.forward, controls.backward);
        rightAxis = CalculateAxis(controls.right, controls.left);

        system.PlayerDirection = new Vector2(rightAxis, forwardAxis);
        system.MovementDirection = new Vector3 ( rightAxis, forwardAxis, 0f).normalized;
    }
    
    private float CalculateAxis(KeyCode positive, KeyCode negative)
    {
        float result = 0;

        if (Input.GetKey(positive))
        {
            result += 1;
        }

        if (Input.GetKey(negative))
        {
            result -= 1;
        }

        return result;
    }

    private void OnDisable()
    {
        system.MovementDirection = Vector3.zero;
        system.Rigidbody.velocity = Vector3.zero;
    }
}