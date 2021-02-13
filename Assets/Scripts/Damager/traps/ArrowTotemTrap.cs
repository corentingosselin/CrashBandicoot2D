using System;
using UnityEngine;

public class ArrowTotemTrap : Trap
{
    
    /**
     * Not used for this project...
     */


    public FlameProjectile flame;
    protected override void TriggerStart()
    {
        base.TriggerStart();
        InvokeRepeating("SpawnFlame", 0, 5);
    }


    protected override void Disable()
    {
        base.Disable();
        CancelInvoke("SpawnFlame");
        print("invisible ");

    }

    private void SpawnFlame()
    {
        Instantiate(flame, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
    }
}