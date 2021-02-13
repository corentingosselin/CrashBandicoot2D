using System;
using UnityEngine;
using UnityEngine.Events;


public class Trigger : MonoBehaviour
{
    
    //generic trigger not used right now
    
    public UnityEvent OnTrigger;
    public UnityEvent OnTriggerLeft;

    private GameObject player;
    private bool trigged;
    [SerializeField] private float distanceTriggerQuit = 5F;
    private void Awake()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        if (OnTrigger == null)
            OnTrigger = new UnityEvent();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 pos = transform.TransformPoint(distanceTriggerQuit, 0,0);
        Gizmos.DrawSphere(pos, 0.1F);
    }

    private void Update()
    {

        if (!trigged && player.transform.position.x >= transform.position.x)
        {
            OnTrigger.Invoke();
            trigged = true;
        } else if (trigged)
        {
            if (player.transform.position.x >= transform.position.x)
            {
                float dist = Vector3.Distance(player.transform.position, transform.position);
                if (dist > distanceTriggerQuit)
                {
                    OnTriggerLeft.Invoke();   
                }
                
            }

        }

    }
    
}