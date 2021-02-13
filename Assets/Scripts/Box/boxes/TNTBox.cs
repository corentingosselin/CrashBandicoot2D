using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TNTBox : Box
{

    private bool triggered;
    private GameObject player;
   
    public override void Init()
    {
        base.Init();
        boxName = "tnt";
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    public override void OnJumpingBox(GameObject player)
    {
        if (triggered) return;
        base.OnJumpingBox(player);
        triggered = true;
        animator.Play(boxName + "countdown");
        StartCoroutine(ExplodeDelay());
    }



    public override void Break()
    {
        broken = true;
        foreach (var box in Utils.GetClosestObject(gameObject,0.5F,"Box"))
        {
            if (!box.name.Equals("CheckPointBox"))
            {
                if (box.TryGetComponent(out Box boxComponent))
                {
                    if (!boxComponent.broken)
                    {
                        boxComponent.Break();
                    }
                }
            }
        }
        
        animator.Play(boxName + "explode");

        boxCollider.isTrigger = true;
        playerCollider.isTrigger = true;
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        Destroy(gameObject,0.4F);
        
        
        //here check if player is around
        float dist = Vector2.Distance(player.transform.position, transform.position);
        if (dist <= 0.45F)
        {
            player.GetComponent<Player>().KillPlayer(DeadType.BURN);
        }

        
      
    }
    
    
    private IEnumerator ExplodeDelay()
    {
        audioManager.Play("tnt");
        yield return new WaitForSeconds(3);
        Break();
        
    }

}