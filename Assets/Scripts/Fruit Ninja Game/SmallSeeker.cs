using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Controls a small seeker- this script probably could have extended ghost player
/// </summary>
[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class SmallSeeker : MonoBehaviour
{
    private GameObject target;
    [SerializeField] private float seekRadius = 2f;
    [SerializeField] private float movementSpeed = .1f;

    private Rigidbody2D rb;


    private void Start()
    {
        // Applying a small force to randomise position after spawn
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(0, 5), Random.Range(0, 5)), ForceMode2D.Impulse);
    }

    private void Update()
    {
        
        // Destroys this if has reached the cutoff point
        if (Camera.main.WorldToScreenPoint(transform.position).y < -20)
        {
            Destroy(gameObject);
        }
        
        // Finds a target and moves towards if it is found
        if (target == null)
        {
            FindTarget();
        }

        if (target != null)
        {
            rb.isKinematic = true;
            MoveTowardsTarget();
            return;
        }
        
        // Gravity is turned back on 
        rb.isKinematic = false;

    }

    /// <summary>
    /// Finds a target to attack
    /// </summary>
    void FindTarget()
    {
        Fruit2D[] allFruit = FindObjectsOfType<Fruit2D>();

        Fruit2D closestFruit = null;
        float closestFruitDistance = float.MaxValue;

        foreach (Fruit2D f in allFruit)
        {
            if (f.CompareTag("Fruit") && f.transform.position.y >= -2f)
            {
                float distance = Vector2.Distance(transform.position, f.transform.position);
                if (distance < closestFruitDistance)
                {
                    closestFruit = f;
                    closestFruitDistance = distance;
                }
            }
        }

        if (closestFruit != null && closestFruitDistance <= seekRadius)
            target = closestFruit.gameObject;
    }
    
    /// <summary>
    /// Moves and rotates towards the target 
    /// </summary>
    void MoveTowardsTarget()
    {
        transform.position =
            Vector2.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);

        transform.LookAt(target.transform);
        transform.rotation = new Quaternion(0f, 0f, transform.rotation.z, 0f);
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    { 
        // If collides with a fruit then it hits it and dies
        if(other.gameObject.CompareTag("Fruit"))
        {
            other.gameObject.GetComponent<Fruit2D>().Hit();
            NinjaPlayer.instance.Score(true);
            Destroy(gameObject);
        }
    }
}
