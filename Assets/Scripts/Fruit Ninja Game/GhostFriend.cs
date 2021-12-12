using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GhostFriend : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathPS;
    [SerializeField] private float lifetime = 10f;

    private bool canMove = true;

    private GameObject target;

    [SerializeField] private float movementSpeed = 10f;

    void Update()
    {
        lifetime -= Time.deltaTime;
        if (canMove && lifetime <= 0f)
        {
            EndOfLifetime();
            return;
        }

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

    void MoveTowardsTarget()
    {
        transform.position =
            Vector2.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
    }
    
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

        if (closestFruit != null)
            target = closestFruit.gameObject;

    }

    void EndOfLifetime()
    {
        canMove = false;
        deathPS.Play();
        Invoke(nameof(DestroyThis), 2.1f);
    }

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
