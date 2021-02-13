using UnityEngine;
using System.Collections;

public class NitroBox : Box
{
    
    private GameObject player;

    
    public override void Init()
    {
        base.Init();
        boxName = "nitro";
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    public override void OnJumpingBox(GameObject player)
    {
        base.OnJumpingBox(player);
        Break();
    }

    public override void Break()
    {
        
        //here check if player is around
        float dist = Vector2.Distance(player.transform.position, transform.position);
        if (dist <= 0.45F)
        {
            player.GetComponent<Player>().KillPlayer(DeadType.BURN);
        }
        
        animator.Play(boxName + "explode");
        audioManager.Play("nitro");

        boxCollider.isTrigger = true;
        playerCollider.isTrigger = true;
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        Destroy(gameObject,0.4F);
        broken = true;
        
        //get other box around and explode them !
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
    }

 
    
    
    
}