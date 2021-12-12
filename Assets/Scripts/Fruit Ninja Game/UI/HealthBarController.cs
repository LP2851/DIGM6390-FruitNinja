using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the health bar for the game
/// </summary>
public class HealthBarController : MonoBehaviour
{
    
    private int health = 3;
    [SerializeField] private Animator[] animators;

    /// <summary>
    /// Updates the health bar to show the player how many lives they have
    /// </summary>
    /// <param name="isLossOfLife">Is the player loosing a life or gaining a life</param>
    public void UpdateHealthBar(bool isLossOfLife) 
    {
        if (isLossOfLife)
        {
            animators[health - 1].SetBool("isAlive", false);
            health--;
        }
        else
        {
            health++;
            animators[health - 1].SetBool("isAlive", true);
        }
    }

}
