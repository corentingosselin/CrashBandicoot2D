using System;
using System.Collections;
using UnityEngine;

namespace Plateforms
{
    public class OldFallingPlateform : MonoBehaviour
    {


        [SerializeField] private float timebeforeFall = 1F;
        private AudioManager audioManager;
        private bool fallen;
        private Animator animator;
        private Collider2D collider2D;

        private void Start()
        {
            audioManager = FindObjectOfType<AudioManager>();
            animator = GetComponent<Animator>();
            collider2D = GetComponent<Collider2D>();

        }
        
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Player"))
            {
                if(!fallen)
                    StartFalling();
            }
        }


        private void StartFalling()
        {
            //play shake
            fallen = true;
            animator.Play("trap_shaking");
            audioManager.Play("trap_shaking");
            StartCoroutine(Fall());
        }
        
        


        private IEnumerator Fall()
        {
            
            yield return new WaitForSeconds(timebeforeFall);
            animator.Play("trap_falling");
            audioManager.Play("trap_falling");
            collider2D.enabled = false;
            this.enabled = false;

        }

        
        
    }
}