using System;
using System.Collections;
using UnityEngine;

namespace Plateforms
{
    public class FallingPlateform : MonoBehaviour
    {


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


        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.collider.CompareTag("Player"))
            {
                if (!fallen)
                {
                    StartFalling();
                }
            }
        }


        private void StartFalling()
        {
            //play shake
            fallen = true;
            animator.Play("whitetrap_shaking");
            audioManager.Play("trap_shaking");
            StartCoroutine(Fall());
        }
        
        


        private IEnumerator Fall()
        {
            
            yield return new WaitForSeconds(1F);
            animator.Play("whitetrap_falling");
            audioManager.Play("trap_falling");
            collider2D.enabled = false;
            this.enabled = false;

        }

        
        
    }
}