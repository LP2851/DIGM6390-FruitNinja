using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    private int health = 3;
    
    [SerializeField] private Animator[] animators;

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
