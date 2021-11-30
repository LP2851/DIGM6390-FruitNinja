using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private Vector3 acceleration, gravity, velocity, push;
    private float mass = 10.0f;
    public GameObject player;
    public float colliderRadius = 0.64f;
    
    public void ApplyForce(Vector3 force) {
        Vector3 a = force/mass;
        acceleration += a;
    }
    
    private void UpdatePosition() {
        velocity = velocity + acceleration;
        transform.position += velocity * Time.deltaTime;
        acceleration = new Vector3(0.0f, 0.0f); // for when you have multiple forces
    }
    
    void Start() {
        gravity = new Vector3(0, -1, 0);
        player = GameObject.FindWithTag("Player");
        colliderRadius *= transform.localScale.x;
    }
    
    void FixedUpdate() {
        ApplyForce(gravity);
        UpdatePosition();
        CheckCollisions();
    }

    void CheckCollisions()
    {
        
        float dist = Vector3.Distance(this.transform.position, player.transform.position);
        float overlap = dist - colliderRadius - 0.15f;

        if (overlap < 0)
        {
            GetComponent<Fruit2D>().Hit();
            player.GetComponent<NinjaPlayer>().Score(CompareTag("Fruit"));
        }
    }
}
