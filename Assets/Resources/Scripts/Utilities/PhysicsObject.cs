using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    [SerializeField] private Vector2 position;
    [SerializeField] private Vector2 velocity;
    [SerializeField] private Vector2 acceleration;
    [SerializeField] private float mass;

    public PhysicsObject(Vector2 position, float mass)
    {
        this.position = position;
        this.mass = mass;
    }

    public void AddForce(Vector2 force)
    {
        acceleration += force / mass;
    }

    //UTILIZAR CUSTOM UPDATE
    public void Update()
    {
        Vector2 deltaTime = default;
        velocity += acceleration * deltaTime;
        position += velocity * deltaTime;
        acceleration = Vector2.zero;
    }
    
    
}