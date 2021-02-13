using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class VillagerEnnemy : Ennemy
{
   
    
    [SerializeField] private GameObject feetTrigger;
    [SerializeField] private GameObject shieldAttack;
    [SerializeField] private GameObject shieldDefense;
    
    private GameObject trigger;
    private Vector3 firstTriggerPosition;
    private Vector3 firstPosition;
    [SerializeField] private Vector2 returnBackTrigger = Vector2.zero;
    
    protected override void Init()
    {
        base.Init();
        trigger = transform.GetChild(2).gameObject;
        firstTriggerPosition = trigger.transform.position;
        collider2D = shieldDefense.GetComponent<Collider2D>();
        defaultState = EnnemyState.WAITING;
        firstPosition = transform.position;
        animName = "villager_"; 
    }
    
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(firstPosition, 0.1F);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(returnBackTrigger, 0.1F);
    }

    
    
    
    
   public override void Update()
    {
        base.Update();
        
     


        //attacking
        if (currentState == EnnemyState.WALKING_FORWARD)
        {
            if (direction == -1 && transform.position.x <= returnBackTrigger.x)
            {
                direction = 1;
                Flip();
            } else if (direction == 1 && transform.position.x >= firstPosition.x)
            {
                direction = -1;
                Flip();
                SwitchState(EnnemyState.WAITING);
                trigger.SetActive(true);
                defaultState = EnnemyState.WAITING;
            }
        }

        if (trigger.activeSelf 
            && currentState != EnnemyState.STUNNED
            && player.transform.position.x >= trigger.transform.position.x 
            && player.transform.position.x < firstPosition.x)
        {
            Attack();
        }

        if (feetTrigger != null)
        {
            Collider2D other = Physics2D.OverlapArea(feetTrigger.transform.position, feetTrigger.transform.position);
            if (other)
            {
                if (other.gameObject.Equals(shieldAttack))
                {
                    if (currentState != EnnemyState.STUNNED)
                    {
                        Stunning();
                    }
                }
            }
        }

    }

   private void Attack()
   {
       defaultState = EnnemyState.WALKING_FORWARD;
       SwitchState(EnnemyState.WALKING_FORWARD);
       direction = -1;
       trigger.SetActive(false);
   }
   
   private void Stunning()
  {
      shieldAttack.SetActive(false);
      shieldDefense.SetActive(true);

      unStunningAnimationTask = UnStunnedAnimation();
      StartCoroutine(unStunningAnimationTask);
      SwitchState(EnnemyState.STUNNED);
   
  }
    
  private IEnumerator unStunningAnimationTask;
  private IEnumerator unStunningTask;

  public IEnumerator UnStunned()
  {
      yield return new WaitForSeconds(1);
      unStunningAnimationTask = null;
      SwitchState(defaultState); 
      unStunningTask = null;
      shieldAttack.SetActive(true);
      shieldDefense.SetActive(false);
  }


  public IEnumerator UnStunnedAnimation()
  {
      yield return new WaitForSeconds(5);
      animator.Play(animName + "unstunned");
      unStunningTask = UnStunned();
      StartCoroutine(unStunningTask);

  }


    public override void Walk()
    {
        base.Walk();
    }

    public override void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (shieldAttack.activeInHierarchy)
            {
                if (direction == -1 && player.transform.position.x < transform.position.x)
                {
                    return;
                } else if(direction == 1 && player.transform.position.x > transform.position.x)
                {
                    return;
                }
                
                other.gameObject.GetComponent<Player>().KillPlayer(DeadType.HEAVEN);
            }
            
            
            
        }
    }

    private void Bounce()
    {
        StopCoroutine(unStunningAnimationTask);  
        if(unStunningTask != null)
            StopCoroutine(unStunningTask);
        animator.Rebind();
        animator.Play(animName + "bounce");
        player.GetComponent<Animator>().Play("crash_jump");
        unStunningAnimationTask = UnStunnedAnimation();
        StartCoroutine(unStunningAnimationTask);
    }

    public override void OnJumpingTop(GameObject player)
    {
        base.OnJumpingTop(player);
        Bounce();
    }
    protected void Flip()
    {
        speed = direction == -1 ? 0.9F : 0.6F;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }

    public override void Die(GameObject pl)
    {
        if (pl != null)
        {

            if (shieldAttack.activeInHierarchy)
            {
                if (direction == -1 && pl.transform.position.x < transform.position.x)
                {
                    return;
                }
                else if (direction == 1 && pl.transform.position.x > transform.position.x)
                {
                    return;
                }
            }
        }

        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
        
        
        base.Die(pl);
       
            
        
    }
}