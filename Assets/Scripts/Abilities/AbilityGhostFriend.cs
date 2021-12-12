using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ability: Ghost Friend 
/// </summary>
public class AbilityGhostFriend : Ability
{
    
    [Header("Ghost Friend"), SerializeField] private GameObject ghostFriend;
    
    /// <summary>
    /// Spawns a ghost friend at the players position
    /// </summary>
    [ContextMenu("Run Ability: Ghost Friend")]
    protected override void RunAbility()
    {
        Instantiate(ghostFriend, transform.position, Quaternion.identity);
    }
}
