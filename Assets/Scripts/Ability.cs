    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Ability : MonoBehaviour
{
    #region Inspector
    [Header("Ability Details")] public AbilityDetails abilityData;

    [Header("Gameplay"), SerializeField] 
    private float killDecreaseCooldownAmount = 1f;

    public bool available = false;
    
    #endregion
    
    private float countdown;
    protected bool isActive = false;

    public void PlayerGotKill()
    {
        if (!isActive) countdown -= killDecreaseCooldownAmount;
    }

    void UpdateCountdown()
    {
        if (available) return;
        
        countdown -= Time.deltaTime;
        
        if (!(countdown <= 0f)) return;
        
        if (isActive)
        {
            isActive = false;
            countdown = abilityData.cooldown;
            EndAbility();
        }
        else
        {
            available = true;
        }

    }

    protected bool Use()
    {
        if (available)
        {
            available = false;
            countdown = abilityData.duration;
            isActive = true;
            RunAbility();
            return true;
        }
        else
        {
            return false;
        }

    }

    void Update()
    {
        UpdateCountdown();
    }

    private void EndAbility()
    {
        // I dont know if I need this yet
    }

    protected virtual void RunAbility()
    {
    }
    

}
