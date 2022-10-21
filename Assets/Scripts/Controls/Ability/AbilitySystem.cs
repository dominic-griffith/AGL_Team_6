using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySystem : MyNamespace.System
{
    [Header("Dependencies")] 
    [SerializeField]
    private TMP_Text bulletCounterText;
    [SerializeField]
    private Transform leftArmTransform;
    [SerializeField]
    private Transform rightArmTransform;
    [SerializeField]
    private Transform forwardArmTransform;
    [SerializeField]
    private Transform backwardArmTransform;
    [SerializeField]
    private Transform topLeftArmTransform;
    [SerializeField]
    private Transform bottomLeftArmTransform;
    [SerializeField]
    private Transform bottomRightArmTransform;
    [SerializeField]
    private Transform topRightArmTransform;
    [SerializeField] 
    private MovementSystem movementSystem;
    
    [Header("Settings")]
    [SerializeField]
    private AbilitySettings abilitySettings;

    [Header("Settings Arm")] 
    [SerializeField] [Range(0f, 5f)]
    private float leftRightPadding;
    [SerializeField] [Range(0f, 5f)]
    private float topBottomPadding;
    
    
    //inner private
    private float _lastShotTime = 0f;
    private bool _shotFired = false;
    private Transform _currentTransform;
    private Vector2 _lookDirection;
    private float _lookAngle;
    private float _lookDirectionX;
    private float _lookDirectionY;
    private Camera _camera;
    private int _currentFireballsShot = 0;
    private bool _textCounterIsNull;
    
    //inner public
    public bool ShotFired { get { return _shotFired; } set { _shotFired = value; } } 
    public GameObject ProjectilePrefab { get; set; } = null;

    private void Awake()
    {
        _camera = Camera.main;
        _currentTransform = leftArmTransform;
        _textCounterIsNull = bulletCounterText == null ? true : false;
    }

    public void Update()
    {
        UpdateArmTransform();
        
        if (Time.time > _lastShotTime + abilitySettings.rateOfFireTime  && ShotFired)
        {
            ShotFired = false;
            _lastShotTime = Time.time;
            if (_currentFireballsShot < abilitySettings.MaxFireballs)
            {
                UpdateProjectileVelocity();
                _currentFireballsShot++;
            }
            else
            {// no more projectiles
                Debug.Log("No more ammo");
            }
        }
        UpdateBulletCounter();
    }

    /**
    * This class attempts to change the arm transform that the player is facing
    * made it so animation its is own thing
    * TODO: currently doesnt work in some cases overall buggy 
    */
    private void UpdateArmTransform()
    {
        _lookDirectionX = _camera.ScreenToWorldPoint(Input.mousePosition).x - gameObject.transform.position.x;
        _lookDirectionY = _camera.ScreenToWorldPoint(Input.mousePosition).y - gameObject.transform.position.y;
        
        if (_lookDirectionX < -topBottomPadding) 
        {// Mouse is to left of player
            if (_lookDirectionY < -leftRightPadding) 
            {// Face character to LEFT, DOWN
                _currentTransform = bottomLeftArmTransform;
            } 
            else if (_lookDirectionY > leftRightPadding) 
            {// Face character to LEFT, TOP
                _currentTransform = topLeftArmTransform;
            } 
            else //if (_lookDirectionY == 0) 
            {// Face character LEFT
                _currentTransform = leftArmTransform;
            }
        }
        else if (_lookDirectionX > topBottomPadding)
        {// Mouse is to right of player
            if (_lookDirectionY < -leftRightPadding) 
            {// Face character to RIGHT, DOWN
                _currentTransform = bottomRightArmTransform;
            } 
            else if (_lookDirectionY > leftRightPadding) 
            {// Face character to RIGHT, TOP
                _currentTransform = topRightArmTransform;
            } 
            else //if (_lookDirectionY == 0) 
            {// Face character RIGHT
                _currentTransform = rightArmTransform;
            }
        }
        else if (_lookDirectionY > 0) 
        {// FACE character TOP
            _currentTransform = forwardArmTransform;
        }
        else if (_lookDirectionY < 0) 
        {// FACE character DOWN
            _currentTransform = backwardArmTransform;
        }
    }

    private void UpdateProjectileVelocity()
    {
        ApplyForwardSpeedMultiplier(abilitySettings.ShootProjectile(this));
    }

  
    private void ApplyForwardSpeedMultiplier(GameObject gameObjectBullet)
    {
        if (gameObject == null)
        {
            Debug.Log("Have shot max num of projectiles");
            return;
        }
        SetMouseInput();
        
        gameObjectBullet.transform.position = _currentTransform.position;
        _currentTransform.rotation = Quaternion.Euler(0, 0,_lookAngle);
        gameObjectBullet.transform.rotation = Quaternion.Euler(0, 0, _lookAngle+ abilitySettings.FireballOffset);
        gameObjectBullet.GetComponent<Rigidbody2D>().AddForce(_currentTransform.right * abilitySettings.MaxFireballSpeed, ForceMode2D.Impulse);
    }

    private void SetMouseInput()
    {
        _lookAngle = Mathf.Atan2(_lookDirectionY, _lookDirectionX) * Mathf.Rad2Deg;
    }

    private void UpdateBulletCounter()
    {
        if (_textCounterIsNull) return;
        
        bulletCounterText.text = $"Bullets: {abilitySettings.MaxFireballs-_currentFireballsShot}/{abilitySettings.MaxFireballs}";
    }
}
