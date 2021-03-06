using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit2D : MonoBehaviour
{
    private bool canBeDead; //If we can destroy the object
    private Vector3 screen; //Position on the screen
    
    public bool canResize;
    public bool notHittingResetsStreaks;
    public bool looseLifeOnHit;
    [Header("Death Effect")]
    public GameObject splat;
    public GameObject sliced;
    public Color splatColor;
    [Header("Health")] public int maxHealth = 1;
    [SerializeField] private int currentHealth = 1;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    void Update ()
    {
        //Set screen position
        screen = Camera.main.WorldToScreenPoint(transform.position);
        //If we can die and are not on the screen
        if (canBeDead && screen.y < -20)
        {
            //Destroy
            if(notHittingResetsStreaks)
                NinjaPlayer.instance.ResetStreak();
            Destroy(gameObject);
        }
        //If we cant die and are on the screen
        //for the fruit to appear on screen the first time
        else if (!canBeDead && screen.y > -10)
        {
            //We can die
            canBeDead = true;
        }
    }
    
    public void Hit()
    {
        currentHealth--;
        if (gameObject.tag == "Fruit")
        {
            if (currentHealth == 0)
            {
                GameObject s = Instantiate(sliced, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), transform.rotation);
                s.transform.localScale = transform.localScale;
            }
            
            GameObject g = Instantiate(splat, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), transform.rotation);
            g.GetComponent<SpriteRenderer>().color = splatColor;
        } else 
        {
            if (looseLifeOnHit)
            {
                NinjaPlayer.instance.playerLives--;
                GameObject g = Instantiate(splat, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), transform.rotation);
            }
                
        }

        
        if(currentHealth <= 0) 
            Destroy(gameObject);
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Hit();
        }
    }

    /// <summary>
    /// Called when fruit is hit by a shockwave
    /// </summary>
    public void ShockwaveHit()
    {
        if (tag.Equals("Enemy")) // if is bomb
        {
            Destroy(gameObject);
            GameObject g = Instantiate(splat, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), transform.rotation);
            
            // Less particles than with a normal explosion
            var main = g.GetComponent<ParticleSystem>().main;
            main.maxParticles = 100;
        }
    }
}
