using UnityEngine;

/// <summary>
/// Controls the ability system with input.
/// </summary>
public class InputAbilityController  : InputController<AbilitySystem>
{
    private void Update()
    {
        OnAbilityInput();
    }

    private void OnAbilityInput()
    {
        if (Input.GetKeyDown(controls.throwAbility) )
        {
            system.ShotFired = true;
        }
    }
}
