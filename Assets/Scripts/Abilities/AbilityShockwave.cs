using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ability: Shockwave ability
/// </summary>
public class AbilityShockwave : Ability
{
    public float timeBetweenShockwaves = 1f;
    public int numberOfShockwaves = 10;

    [Header("Shockwave"), SerializeField] private GameObject shockwaveEffect;

    /// <summary>
    /// Starts the shockwave spawning coroutine
    /// </summary>
    [ContextMenu("Activate Ability: Shockwave")]
    protected override void RunAbility()
    {
        StartCoroutine(Shockwave());
    }

    /// <summary>
    /// Spawns some shockwaves at the players position with a wait in between each one spawned
    /// </summary>
    /// <returns></returns>
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
