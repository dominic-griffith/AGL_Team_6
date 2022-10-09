using System;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class AbilitySystem : MyNamespace.System
{
    [Header("Dependencies")] 
    [SerializeField]
    private Transform leftArmTransform;
    [SerializeField]
    private Transform rightArmTransform;
    [SerializeField]
    private Transform forwardArmTransform;
    [SerializeField]
    private Transform backwardArmTransform;
    [SerializeField] 
    private MovementSystem movementSystem;
    
    [Header("Settings")]
    [SerializeField]
    private AbilitySettings abilitySettings;
    
    //inner private
    private float _lastShotTime = 0f;
    private bool _shotFired = false;
    private Transform _currentTransform;
    private Vector2 _lookDirection;
    private float _lookAngle;
    
    //inner public
    public bool ShotFired { get { return _shotFired; } set { _shotFired = value; } } 
    public GameObject ProjectilePrefab { get; set; } = null;

    private void Start()
    {
        _currentTransform = leftArmTransform;
    }

    public void Update()
    {
        if (Time.time > _lastShotTime + abilitySettings.rateOfFireTime  && ShotFired)
        {
            ShotFired = false;
            _lastShotTime = Time.time;
            UpdateProjectileVelocity();
        }
        
    }

    private void UpdateProjectileVelocity()
    {
        //ProjectilePrefab = abilitySettings.ShootProjectile(this);
        ApplyForwardSpeedMultiplier(abilitySettings.ShootProjectile(this));
    }

  
    private void ApplyForwardSpeedMultiplier(GameObject gameObject)
    {
        if (gameObject == null)
        {
            Debug.Log("Have shot max num of projectiles");
            return;
        }
        SetCurrentTransform();
        SetMouseInput();

        /*gameObject.transform.position = _currentTransform.position;
        gameObject.transform.rotation = _currentTransform.rotation;
        gameObject.GetComponent<Rigidbody2D>().AddForce(_currentTransform.up * abilitySettings.MaxFireballSpeed, ForceMode2D.Impulse);*/
        gameObject.transform.position = _currentTransform.position;
        _currentTransform.rotation = Quaternion.Euler(0,0,_lookAngle);
        gameObject.transform.rotation = Quaternion.Euler(0,0,_lookAngle+ abilitySettings.FireballOffset);
        gameObject.GetComponent<Rigidbody2D>().AddForce(_currentTransform.right * abilitySettings.MaxFireballSpeed, ForceMode2D.Impulse);
    }

  
    /**
    * This class attempts to change the arm transform that the player is facing
    * made it so animation its is own thing
    * TODO: currently doesnt work in some cases overall buggy 
    */
    private void SetCurrentTransform()
    {
        _currentTransform = movementSystem.PlayerDirection switch
        {
            Vector2 v when v.Equals(Vector2.up) => forwardArmTransform,
            Vector2 v when v.Equals(Vector2.down) => backwardArmTransform,
            Vector2 v when v.Equals(Vector2.right) => rightArmTransform,
            Vector2 v when v.Equals(Vector2.left) => leftArmTransform,
            Vector2 {x:> 0.5f, y:> 0.5f} => rightArmTransform,
            Vector2 {x:> 0.5f, y:> -0.5f} => rightArmTransform,
            Vector2 {x: -0.5f, y:> -0.5f} => leftArmTransform,
            Vector2 {x:> -0.5f, y:> 0.5f} => leftArmTransform,
            _ => _currentTransform
        };
    }

    private void SetMouseInput()
    {
        _lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _lookAngle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg;
    }
}
