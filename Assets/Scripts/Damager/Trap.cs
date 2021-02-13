

using System;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{

    private Camera camera;
    
    
    private void Start()
    {
        camera = FindObjectOfType<Camera>();
        Init();
    }

    protected virtual void Init()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.Equals(camera))
    }

    protected virtual void Disable()
    {
        gameObject.SetActive(false);
        
    }
    
    protected bool activated = false;
    public void OnTriggerEvent()
    {
        if (activated) return;
        activated = true;
        TriggerStart();
    }
    
    protected virtual void TriggerStart()
    {
        
    }

    private void OnBecameInvisible()
    {
        Disable();
    }

    private void Update()
    {
    }
        

    private void OnApplicationQuit()
    {
        activated = false;
    }
}