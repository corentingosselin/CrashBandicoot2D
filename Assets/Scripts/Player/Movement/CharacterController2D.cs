using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{

    private AkuAku akuAku;
    
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] public float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform[] m_GroundCheck;                           // A position marking where to check if the player is grounded.

    
    public bool stucked = false;
    public bool m_Grounded = false;            // Whether or not the player is grounded
    public Rigidbody2D m_Rigidbody2D;
    public bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;

    
    public Animator animator;
    private Player player;
    void Start()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
        akuAku = GameObject.FindGameObjectWithTag("AkuAku").GetComponent<AkuAku>();

    }

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;
	
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }
    
    private void FixedUpdate()
    {

    }

    private bool falling = false;
    private void Update()
    {

        //faling
        if (!falling && m_Rigidbody2D.velocity.y < -0.1F)
        {
            falling = true;
            if(!player.attacking)
                animator.Play("crash_jump");

        }

        if (!m_Grounded && isGrounded())
        {
            timeStucked = 0;
            stucked = false;
            m_Grounded = true;
            falling = false;
            OnLandEvent.Invoke();
        }
    }



    private int timeStucked = 0;
    void OnCollisionStay2D(Collision2D  collision) 
    {
        Collider2D collider = collision.collider;
        if(collider.CompareTag("Box") && !m_Grounded)
        {
            if (lastCollideSide == LastCollideSide.RIGHT)
            {
                timeStucked++;
                if (timeStucked >= 5)
                {
                    stucked = true;
                    m_Rigidbody2D.velocity = new Vector2(0.5F, 0);
                }
            } else if(lastCollideSide == LastCollideSide.LEFT)
            {
                timeStucked++;

                if (timeStucked >= 5)
                {
                    stucked = true;
                    m_Rigidbody2D.velocity = new Vector2(-0.5F,0);
                }
            }
            else
            {
                stucked = false;
            }
        }
    }




    private enum LastCollideSide
    {
        LEFT,
        RIGHT,
        TOP,
        BOTTOM
    }
    
    private LastCollideSide lastCollideSide;
    
    void OnCollisionEnter2D(Collision2D  collision) 
    {
      
        if(collision.collider.CompareTag("Box"))
        { 
            Collider2D collider = collision.collider;
            float RectWidth = GetComponent<Collider2D> ().bounds.size.x;
            float RectHeight = GetComponent<Collider2D> ().bounds.size.y;
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 center = collider.bounds.center;
 
            if (contactPoint.y > center.y && //checks that circle is on top of rectangle
                (contactPoint.x < center.x + RectWidth / 2 && contactPoint.x > center.x - RectWidth / 2))
            {
                lastCollideSide = LastCollideSide.TOP;
            }
            else if (contactPoint.y < center.y &&
                     (contactPoint.x < center.x + RectWidth / 2 && contactPoint.x > center.x - RectWidth / 2)) {
                lastCollideSide = LastCollideSide.BOTTOM;
            }
            else if (contactPoint.x > center.x &&
                     (contactPoint.y < center.y + RectHeight / 2 && contactPoint.y > center.y - RectHeight / 2)) {
                lastCollideSide = LastCollideSide.RIGHT;
                
            }
            else if (contactPoint.x < center.x &&
                     (contactPoint.y < center.y + RectHeight / 2 && contactPoint.y > center.y - RectHeight / 2)) {
                lastCollideSide = LastCollideSide.LEFT;
            }
        }
    }
    

    private bool isGrounded()
    {
        foreach (var groundChecker in m_GroundCheck)
        {
            Collider2D other = Physics2D.OverlapArea(groundChecker.position, groundChecker.position, m_WhatIsGround);
            if (other)
            {
                if (other.CompareTag("Box"))
                {
                    var yHalfExtents = other.bounds.extents.y;
                    var yCenter = other.bounds.center.y;
                    float yUpper = transform.position.y + (yCenter + yHalfExtents);
                    if(transform.position.y > yUpper)
                        return true;
                } else return true;
            }

        }
        return false;

    }


    public bool handleMove = true;
    public void Move(float move, bool jump)
    {

        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
        if (handleMove)
        {
            
            // And then smoothing it out and applying it to the character
            if(!stucked)
                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity,
                    m_MovementSmoothing);
            if(m_Grounded && Mathf.Abs(move) > 0 && !jump && !player.attacking) 
                animator.Play("crash_run");

        }

        // If the input is moving the player right and the player is facing left...
        if (move > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();

        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();

        }
        
        
		
        // If the player should jump...
      
        if (jump)
        {
            //print("try jump");
            // print("vel y " + m_Rigidbody2D.velocity.y);
            if (m_Rigidbody2D.velocity.y > 1.5D) return;
            if (isGrounded())
            {
                if(!player.attacking)
                    animator.Play("crash_jump");
                m_Grounded = false;
                // if(m_Rigidbody2D > m_Rigidbody2D.velocity <)
                m_Rigidbody2D.AddForce(new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce));
            }
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        
        akuAku.Flip();
        
        
        
    }
}