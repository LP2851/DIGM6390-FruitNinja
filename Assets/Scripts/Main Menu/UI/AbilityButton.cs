using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        gameObject.name = "Ability Button: " + data.abilityName;
        nameText.text = data.abilityName;
        descriptionText.text = data.description;
    }

    private void Update()
    {
        GetComponent<Image>().color = (data.inUse) ? inUseColor : notInUseColor;
    }


    public void HoverEvent(bool isHoverOver)
    {
        animator.SetBool("isDisplay", isHoverOver);
    }

    public void SetAsActive()
    {
        if (data.inUse) return;
        PlayerDataImporter.instance.SetAbilityTo(data);
    }

}
