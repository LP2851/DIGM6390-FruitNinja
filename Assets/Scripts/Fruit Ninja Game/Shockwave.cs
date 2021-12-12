using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls a shockwave
/// </summary>
[RequireComponent(typeof(Animator))]
public class Shockwave : MonoBehaviour
{
    public float timeBeforeDestroy = 3f;
    
    void Start()
    {
        Invoke(nameof(CleanUp), timeBeforeDestroy);
    }


    /// <summary>
    /// Destroys this game object
    /// </summary>
    void CleanUp()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Hits enemies with shockwave on collision
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Fruit2D>().ShockwaveHit();
        }
    }
}
