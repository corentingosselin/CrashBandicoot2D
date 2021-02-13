using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.PlayerLoop;

public class SkunkEnnemy : Ennemy
{
    
    protected override void Init()
    {
        base.Init();
        defaultState = EnnemyState.WALKING_FORWARD;
        animName = "skunk_"; //turtle_die, turtle_walk, ect
    }
    


    
    
    public override void OnJumpingTop(GameObject pl)
    {
        collider2D.enabled = false;
        base.OnJumpingTop(pl);
        player.GetComponent<Animator>().Play("crash_jump");
        
        //die
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        SwitchState(EnnemyState.DIYING);
        if(pl != null)
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), collider2D);
        if(animName != null)
            animator.Play(animName + "die");
        StartCoroutine(RemoveEnnemy());
    }


    public override void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.CompareTag("Player"))
        {
            Collider2D collider = col.collider;
            float RectWidth = collider.bounds.size.x;
            Vector3 contactPoint = col.contacts[0].point;
            Vector3 center = collider2D.bounds.center;

            if (contactPoint.y > center.y && //checks that circle is on top of rectangle
                (contactPoint.x < center.x + RectWidth / 2 && contactPoint.x > center.x - RectWidth / 2))
            {
                OnJumpingTop(col.gameObject);
                return;
            }
        


            if (currentState == EnnemyState.DIYING) return;
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
    
    public override IEnumerator RemoveEnnemy()
    {
        yield return new WaitForSeconds(1F);
        Destroy(gameObject);
    }
    
    
    public override void Walk()
    {
        base.Walk();
    }

   
}