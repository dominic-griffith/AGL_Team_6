using UnityEngine;

/// <summary>
/// Controls the movement system with input.
/// </summary>

public class InputMovementController : InputController<MovementSystem>
{
    [Header("Dependencies")] [SerializeField]
    private Animator animController;
    
    // other debugging stuff
    [HideInInspector]
    public float forwardAxis;
    [HideInInspector]
    public float rightAxis;

    public int CurrentAnimationState => _currentState;
    
    //animation stuff
    private static readonly int WalkNorth = Animator.StringToHash("northAnim");
    private static readonly int WalkSouth = Animator.StringToHash("southAnim");
    private static readonly int WalkEast = Animator.StringToHash("eastAnim");
    private static readonly int WalkWest = Animator.StringToHash("westAnim");
    private int _currentState;

    private void Update()
    {
        forwardAxis = CalculateAxis(controls.forward, controls.backward);
        rightAxis = CalculateAxis(controls.right, controls.left);
        
        SetAnimationState(forwardAxis, rightAxis);

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

    private void SetAnimationState(float forwardValAxis, float rightValAxis)
    {
        if (forwardAxis == 0f && rightAxis == 0f)
            animController.speed = 0f;
        else
            animController.speed = 1f;
        if (forwardValAxis == 1)
        {
            _currentState = WalkNorth;
            animController.CrossFade(WalkNorth,0,0);
        }
        else if (forwardValAxis == -1)
        {
            _currentState = WalkSouth;
            animController.CrossFade(WalkSouth,0,0);
        }
        else if (rightValAxis == 1)
        {
            _currentState = WalkEast;
            animController.CrossFade(WalkEast,0,0);
        }
        else if (rightValAxis == -1)
        {
            _currentState = WalkWest;
            animController.CrossFade(WalkWest,0,0);
        }
        
    }

    private void OnDisable()
    {
        system.MovementDirection = Vector3.zero;
        system.Rigidbody.velocity = Vector3.zero;
    }
}