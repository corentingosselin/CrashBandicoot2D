using System;
using UnityEngine;

public class PathEnnemy : Ennemy
{


    [SerializeField] private Vector2 leftCorner = Vector2.zero;
    [SerializeField] private Vector2 rightCorner = Vector2.zero;

    protected override void Init()
    {
        base.Init();
        direction = -1;
    }
    
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(leftCorner, 0.1F);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(rightCorner, 0.1F);
    }


    public override void Walk()
    {
        base.Walk();

        if (leftCorner != null)
        {
            if (transform.position.x <= leftCorner.x)
            {
                if (direction <= -1)
                {
                    //left reached
                    direction = 1;
                    Flip();
                }
            }
        }

        if (rightCorner != null)
        {
            if (transform.position.x >= rightCorner.x)
            {
                if (direction >= 1)
                {
                    //right reached
                    direction = -1;
                    Flip();
                }
            }
        }
    }
    
    protected virtual void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}