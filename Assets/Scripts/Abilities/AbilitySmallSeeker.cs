using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Ability: Small Seeker ability
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class AbilitySmallSeeker : Ability
{
    [Header("Small Seeker"), SerializeField] private GameObject seeker;
    [SerializeField] private int minSpawnAmount, maxSpawnAmount;

    /// <summary>
    /// Spawns a random number of seekers at the player's position
    /// </summary>
    protected override void RunAbility()
    {
        int spawnAmount = Random.Range(minSpawnAmount, maxSpawnAmount);

        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject go = Instantiate(seeker, transform.position, Quaternion.identity);

        }
    }
    
    /// <summary>
    /// If the ability is active and the player has collided with a fruit,
    /// then the ability is run.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive && other.gameObject.CompareTag("Fruit"))
        {
            RunAbility();
        }
    }
}
