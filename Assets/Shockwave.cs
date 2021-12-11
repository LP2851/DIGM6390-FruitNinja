using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Shockwave : MonoBehaviour
{

    public float timeBeforeDestroy = 3f;
    
    void Start()
    {
        Invoke(nameof(CleanUp), timeBeforeDestroy);
    }


    void CleanUp()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Fruit2D>().ShockwaveHit();
        }
    }
}
