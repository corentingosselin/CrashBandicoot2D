using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.PlayerLoop;

public class TurtleEnnemy : PathEnnemy
{
    
    protected override void Init()
    {
        base.Init();
        defaultState = EnnemyState.WALKING_FORWARD;
        animName = "turtle_"; //turtle_die, turtle_walk, ect
    }
    

    private void Stunning(GameObject player)
    {
        
        //if already stunned 
        //restore the stunning task
        //bounce
        if (currentState == EnnemyState.STUNNED)
        {
            StopCoroutine(unStunningAnimationTask);  
            if(unStunningTask != null)
                StopCoroutine(unStunningTask);
            animator.Rebind();
            animator.Play(animName + "bounce");
            player.GetComponent<Animator>().Play("crash_jump");
            unStunningAnimationTask = UnStunnedAnimation();
            StartCoroutine(unStunningAnimationTask);
            return;
        }

        rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        
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
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

    }


    public IEnumerator UnStunnedAnimation()
    {
        yield return new WaitForSeconds(5);
        animator.Play(animName + "unstunned");
        unStunningTask = UnStunned();
        StartCoroutine(unStunningTask);

    }
    
    
    public override void OnJumpingTop(GameObject player)
    {
        base.OnJumpingTop(player);
        //play return back turtle
        Stunning(player);
    }


    public override void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2D(col);
        if (currentState != EnnemyState.STUNNED)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                Player player = col.gameObject.GetComponent<Player>();
                if (!player.attacking)
                {
                    col.gameObject.GetComponent<Player>().KillPlayer(DeadType.HEAVEN);
                }
                else
                {
                    Die(col.gameObject);
                }
            }
        }
    }
    
    
    public override void Walk()
    {
        base.Walk();
    }

    public override void Die(GameObject player)
    {
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        base.Die(player);
    }
}