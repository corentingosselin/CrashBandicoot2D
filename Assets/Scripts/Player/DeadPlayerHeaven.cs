
    using System;
    using UnityEngine;
    using UnityEngine.Experimental.PlayerLoop;

    public class DeadPlayerHeaven : MonoBehaviour
    {
        private void Start()
        {
            
        }
        
        //small up anim
        public void FixedUpdate()
        {
            transform.position = transform.TransformPoint(0, 0.01F, 0);
        }
    }
