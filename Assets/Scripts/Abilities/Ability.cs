using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    void Update()
    {
        UpdateCountdown();
        if (Input.GetMouseButtonDown(0) && CheckDoubleClick())
        {
            Use();
        }
    }

    private void EndAbility()
    {
        // I dont know if I need this yet
    }

    protected abstract void RunAbility();

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