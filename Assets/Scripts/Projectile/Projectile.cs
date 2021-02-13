using System;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
        
    //useless right now
    
    protected Rigidbody2D rigidbody2D;
    protected Collider2D collider2D;
    [SerializeField] protected float directionSpeed;
        
    private void Start()
    {
        collider2D = GetComponent<Collider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    public virtual void move()
    {
        rigidbody2D.velocity = new Vector2(-directionSpeed,0);
    }
        
    private void FixedUpdate()
    {
        move();
    }
}