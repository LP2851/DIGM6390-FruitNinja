using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AbilityChargeBar : MonoBehaviour
{
    
    private Slider slider;
    [SerializeField] private Text text;
    [SerializeField] private Image fillAreaFill; 

    private Ability ability;
    
    public void SetAbility(Ability ability)
    {
        slider = GetComponent<Slider>();
        this.ability = ability;
        slider.minValue = 0;
        slider.maxValue = ability.abilityData.cooldown;
        slider.value = slider.maxValue - ability.countdown;
        fillAreaFill.color = ability.abilityData.chargeBarColor;
    }

    void Update()
    {
        if(ability == null) return;

        if (ability.available)
        {
            text.text = "Ability " + ability.abilityData.abilityName + " is ready! Double click to use.";
        } else if (ability.isActive)
        {
            text.text = "Ability " + ability.abilityData.abilityName + " is Active!";
        }
        else
        {
            
            slider.value = slider.maxValue - ability.countdown;

            int val = (int)(slider.normalizedValue * 100);
            text.text = "Progress to " + ability.abilityData.abilityName + " Ability: " + val + "%";
        }
    }
    
    
}
