using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityShockwave : Ability
{
    public float timeBetweenShockwaves = 1f;
    public int numberOfShockwaves = 10;

    [Header("Shockwave"), SerializeField] private GameObject shockwaveEffect;

    [ContextMenu("Activate Ability: Shockwave")]
    protected override void RunAbility()
    {
        StartCoroutine(Shockwave());
    }

    private IEnumerator Shockwave()
    {
        for (int i = 0; i < numberOfShockwaves; i++)
        {
            Instantiate(shockwaveEffect, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenShockwaves);
        }

        isActive = false;
    }

    
}
