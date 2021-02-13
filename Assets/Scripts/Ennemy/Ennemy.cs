using System;
using System.Collections;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    
    protected EnnemyState defaultState;
    protected EnnemyState currentState;
    protected Rigidbody2D rigidbody2D;
    protected Animator animator;
    protected Collider2D collider2D;
    protected string animName;
    protected GameObject player;
    private AudioManager audioManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();
        audioManager = FindObjectOfType<AudioManager>();
        Init();
        currentState = defaultState;
    }

    protected virtual void Init()
    {

    }


    private void UpdateCurrentState()
    {
        if (currentState == null) return;
        switch (currentState)
        {
            case EnnemyState.WAITING:
                break;
            case EnnemyState.DIYING:
                break;
            case EnnemyState.STUNNED:
                break;
            case EnnemyState.WALKING_FORWARD:
                Walk();
                break;
        }
    }
    
    
    public void SwitchState(EnnemyState state)
    {
        if (currentState == state) return;
        this.currentState = state;
        switch (currentState)
        {
            case EnnemyState.WAITING:
                InitWaiting();
                break;
            case EnnemyState.DIYING:
                break;
            case EnnemyState.STUNNED:
                InitStunned();
                break;
            case EnnemyState.WALKING_FORWARD:
                InitWalking();
                break;

        }
    }

    protected virtual void InitWalking()
    {
        //play walking animation
        if(animName != null)
            animator.Play(animName + "walk");

    }
    protected virtual void InitWaiting()
    {
        //play idle animation
    }

    protected virtual void InitStunned()
    {
        //play stunned animation
        if(animName != null)
            animator.Play(animName + "stunned");
    }

    


    [SerializeField] protected float speed = 0.2F;
    private Vector2 movement;
    
    // -1 because the ennemy is moving to the opposite of the player
    //1 or 0
    public int direction = -1;

    public virtual void Walk()
    {
        movement.Set(speed * direction, rigidbody2D.velocity.y);
        rigidbody2D.velocity = movement;
    }
    

    //died by player
    public virtual void Die(GameObject pl)
    {
        SwitchState(EnnemyState.DIYING);
        if(pl != null)
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), collider2D);
        collider2D.enabled = false;
        if(animName != null)
            animator.Play(animName + "die");
        audioManager.Play(animName + "die");
        StartCoroutine(RemoveEnnemy());
        if(pl != null)
            rigidbody2D.velocity = new Vector2(pl.GetComponent<CharacterController2D>().m_FacingRight ? 8 : -8 ,1);
    }

    public virtual IEnumerator RemoveEnnemy()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (player.transform.position.x > transform.position.x && Vector2.Distance(player.transform.position , transform.position) > 5F)
            Destroy(gameObject);
        
        UpdateCurrentState();
    }
    
    
    
    
    public virtual void OnJumpingTop(GameObject player) {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,4F);
        audioManager.Play("bounce");
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "AttackHitBox")
        {
            //if (this is IronBox) return;
            audioManager.Play("hit");
            Die(player);
            
        }
    }


    [SerializeField] private float topAngle = 67.5F;
    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            Vector2 v = col.contacts[0].point - (Vector2) transform.position;
            if (Mathf.Abs(Vector2.Angle(v, Vector3.up)) < topAngle)
            {
                OnJumpingTop(col.gameObject);
            }                
            
        }
    }
    
}