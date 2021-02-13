using System;
using DefaultNamespace;
using UnityEngine;

public abstract class Box : MonoBehaviour
{

    [SerializeField] private bool particles = false;
    [SerializeField] protected Collider2D playerCollider;
    [SerializeField] protected Collider2D boxCollider;

    // Used for song generic
    protected String boxName;
    private CharacterController2D characterController2D;
    protected Rigidbody2D rigidbody2D;
    protected Animator animator;
    protected AudioManager audioManager;
    
    // Use this for initialization
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        characterController2D = FindObjectOfType<CharacterController2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Init();
        boxName += "_";
    }


    public virtual void Init()
    {
    }

    private void FixedUpdate() { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //when tornado is used by player
        if (other.gameObject.name == "AttackHitBox")
        {
            if (this is IronBox) return;
            if (rigidbody2D.velocity.y > -0.1)
            {
                Break();
            }
        }
    }

    /**
     * No secret here
     */
    public virtual void OnJumpingBox(GameObject player) {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,3F);
        player.GetComponent<Animator>().Play("crash_jump");
        audioManager.Play("bounce");

    }

    public bool broken;
    public virtual void Break()
    {
        //disable collision, and let's play the animation before removing
        animator.Play(boxName + "break");
        boxCollider.isTrigger = true;
        playerCollider.isTrigger = true;
        broken = true;
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        if (particles)
        {
            GetComponent<BoxBreakParticle>().SetupParticles();
        }
        audioManager.Play("break");
        Destroy(gameObject,0.1F);

    }

    private void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Floor"))
        {
            //debug box when touch the ground
            Physics2D.IgnoreCollision(characterController2D.gameObject.GetComponent<Collider2D>(),
                playerCollider, false);

        }
        if (col.collider.CompareTag("Player"))
        {
            Collider2D collider = col.collider;
            float RectWidth = collider.bounds.size.x;
            Vector3 contactPoint = col.contacts[0].point;
            Vector3 center = playerCollider.bounds.center;
 
            //check top
            if (contactPoint.y > center.y && //checks that circle is on top of rectangle
                (contactPoint.x < center.x + RectWidth / 2 && contactPoint.x > center.x - RectWidth / 2))
            {
                OnJumpingBox(col.gameObject);
            }
            
            //check if box is ready to fall
            if (rigidbody2D.velocity.y < 0 && broken)
            {
                //falling
                Physics2D.IgnoreCollision(col.collider,playerCollider);
            }

        } 
    }
    
}