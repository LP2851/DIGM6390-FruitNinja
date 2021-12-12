using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controller for a ability button in the main menu
/// </summary>
[RequireComponent(typeof(Button), typeof(Animator))]
public class AbilityButton : MonoBehaviour
{
    
    public AbilityDetails data;
    
    public Text nameText, descriptionText;

    [ColorUsage(true, false)] public Color inUseColor, notInUseColor;

    private Animator animator;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnValidate()
    {
        // Updating the name of the button and the button contents
        gameObject.name = "Ability Button: " + data.abilityName;
        nameText.text = data.abilityName;
        descriptionText.text = data.description;
    }

    private void Update()
    {
        GetComponent<Image>().color = (data.inUse) ? inUseColor : notInUseColor;
    }


    /// <summary>
    /// Called when the player hovers over the button
    /// </summary>
    /// <param name="isHoverOver"></param>
    public void HoverEvent(bool isHoverOver)
    {
        animator.SetBool("isDisplay", isHoverOver);
    }

    /// <summary>
    /// Sets the ability this button represents are the active ability.
    /// </summary>
    public void SetAsActive()
    {
        if (data.inUse) return;
        PlayerDataImporter.instance.SetAbilityTo(data);
    }

}
