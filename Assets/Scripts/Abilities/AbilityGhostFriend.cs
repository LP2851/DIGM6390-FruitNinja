using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityGhostFriend : Ability
{
    [Header("Ghost Friend"), SerializeField] private GameObject ghostFriend;
    
    [ContextMenu("Run Ability: Ghost Friend")]
    protected override void RunAbility()
    {
        Instantiate(ghostFriend, transform.position, Quaternion.identity);
    }
}
