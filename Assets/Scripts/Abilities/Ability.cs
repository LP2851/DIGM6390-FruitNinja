using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Abstract class for creating abilities
/// </summary>
public abstract class Ability : MonoBehaviour
{
    #region Inspector
    
    [Header("Ability Details")] public AbilityDetails abilityData;
    [Header("Gameplay"), SerializeField] private float killDecreaseCooldownAmount = 1f;
    public bool available = false;
    public float countdown;
    public bool isActive = false;
    
    #endregion

    #region Double Click Variables

    private float lastClickTime;
    private const float DOUBLE_CLICK_TIME = 0.2f;

    #endregion

    void Awake()
    {
        AbilityChargeBar chargeBar = FindObjectOfType<AbilityChargeBar>();
        chargeBar.SetAbility(this);
        countdown = abilityData.cooldown;
    }

    /// <summary>
    /// Speeds up the ability charge countdown when the player gets a kill
    /// </summary>
    public void PlayerGotKill()
    {
        if (!isActive) countdown -= killDecreaseCooldownAmount;
    }

    /// <summary>
    /// Called on Update(), updates the countdown and sets variables as needed - isActive, available.
    /// </summary>
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

    /// <summary>
    /// Uses the ability
    /// </summary>
    private void Use()
    {
        if (available)
        {
            available = false;
            countdown = abilityData.duration;
            isActive = true;
            RunAbility();
        }
    }

    /// <summary>
    /// Updates the countdown and checks if there has been a double click to activate the ability
    /// </summary>
    void Update()
    {
        UpdateCountdown();
        if (Input.GetMouseButtonDown(0) && CheckDoubleClick())
        {
            Use();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void EndAbility()
    {
        // I dont know if I need this yet
    }

    /// <summary>
    /// Abstract method that is called when the user has activated the ability (after double click) 
    /// </summary>
    protected abstract void RunAbility();

    
    /// <summary>
    /// Returns true if the user has double clicked
    /// </summary>
    /// <returns>True if the user has double clicked, otherwise false</returns>
    private bool CheckDoubleClick()
    {
        if (Time.time - lastClickTime <= DOUBLE_CLICK_TIME)
        {
            lastClickTime = Time.time;
            return true;
        }

        lastClickTime = Time.time;
        return false;
    }
}