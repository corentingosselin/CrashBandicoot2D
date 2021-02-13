using UnityEngine;
using System.Collections;

public class UpBox : Box
{
    
    public override void Init()
    {
        base.Init();
        boxName = "up";
    }
    
    
    public override void OnJumpingBox(GameObject player)
    {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,4F);
        player.GetComponent<Animator>().Play("crash_jump");
        animator.Play("up_bounce");
        audioManager.Play("bounce");
    }

    public override void Break()
    {
        base.Break();
    }
}
