using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AbilitySettings : ScriptableObject
{
    [Header("General Settings")]
    [SerializeField] [Tooltip("the fireballs that can be shot until needing energy")]
    public int maxFireballs = 5;
    [SerializeField] [Tooltip("the fireball speed")]
    public float fireBallSpeed = 5f;
    [SerializeField] [Tooltip("the fireball rate of fire")]
    public float rateOfFireTime = 3f;
    [SerializeField] [Tooltip("the fireball offset when spawned")]
    public float fireballOffset = -90f;

    [Header("Dependencies")]
    [SerializeField]
    private GameObject fireballPrefab;
    
    //other
    private AbilitySystem _abilitySystem;
    private int currentFireballsShot = 0;

    public int CurrentFireballsShot => currentFireballsShot;
    public int MaxFireballs => maxFireballs;
    public float MaxFireballSpeed => fireBallSpeed;
    public float FireballOffset => fireballOffset;
    
    
    public GameObject ShootProjectile(AbilitySystem system)
    {
        _abilitySystem = system;
        //if ( CurrentFireballsShot >= MaxFireballs) return null;//when we shot the max num of projectiles
        
        return CreateProjectile();
    }

    private GameObject CreateProjectile()
    {
        //currentFireballsShot++;
        return Instantiate(fireballPrefab);
    }
}
