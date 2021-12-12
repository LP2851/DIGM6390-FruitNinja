using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Script that controls the ghost friend
/// </summary>
public class GhostFriend : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathPS;
    [SerializeField] private float lifetime = 10f;

    private bool canMove = true;

    private GameObject target;

    [SerializeField] private float movementSpeed = 10f;

    void Update()
    {
        // Decrementing lifetime
        lifetime -= Time.deltaTime;
        
        // If not set to die then start process
        if (canMove && lifetime <= 0f)
        {
            EndOfLifetime();
            return;
        }

        // Tries to find a target and attack it
        if (canMove)
        {
            if (target == null)
            {
                FindTarget();
            }

            if (target != null)
            {
                MoveTowardsTarget();
            }
        }
    }

    /// <summary>
    /// Move the ghost player towards its target
    /// </summary>
    void MoveTowardsTarget()
    {
        transform.position =
            Vector2.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
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
            // Preventing player going too far down on screen
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

        if (closestFruit != null)
            target = closestFruit.gameObject;

    }

    /// <summary>
    /// Called when it is time to kill this ghost player
    /// </summary>
    void EndOfLifetime()
    {
        canMove = false;
        deathPS.Play();
        Invoke(nameof(DestroyThis), 2.1f);
    }

    /// <summary>
    /// Destroys this game object
    /// </summary>
    void DestroyThis()
    {
        Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Fruit"))
        {
            other.gameObject.GetComponent<Fruit2D>().Hit();
            NinjaPlayer.instance.Score(true);
        }
    }

}
