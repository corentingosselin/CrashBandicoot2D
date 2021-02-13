using UnityEngine;

public class AkuAkuBox : Box
{


    private AkuAku akuAku;
    public override void Init()
    {
        base.Init();
        akuAku = GameObject.FindGameObjectWithTag("AkuAku").GetComponent<AkuAku>();
        boxName = "akuaku";
    }
    
    public override void OnJumpingBox(GameObject player)
    {
        base.OnJumpingBox(player);
        Break();
        
    }
    
    public override void Break()
    {
        base.Break();
        akuAku.AddFeather();
    }
    
}
