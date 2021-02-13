using UnityEngine;
using System.Collections;
using Environment;

public class LifeBox : Box
{
    
    [SerializeField] GameObject life;

    public override void Init()
    {
        base.Init();
        boxName = "life";
    }
    
    public override void OnJumpingBox(GameObject player)
    {
        base.OnJumpingBox(player);
        Break();
        
    }
    
    public override void Break()
    {
        base.Break();
        Instantiate(life, new Vector3(transform.position.x , transform.position.y , transform.position.z), Quaternion.identity);
            
        
    }

}
