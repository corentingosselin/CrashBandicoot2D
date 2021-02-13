using System;
using UnityEngine;

    public class ShieldAttack : MonoBehaviour
    {

        private VillagerEnnemy villager;
        private void Start()
        {
            villager = transform.parent.gameObject.GetComponent<VillagerEnnemy>();
        }


        private void OnCollisionEnter2D(Collision2D col)
        {
            //check if player is on left
                if (col.gameObject.CompareTag("Player"))
                {
                    if (villager.direction == -1 && col.transform.position.x > transform.position.x)
                    {
                        return;
                    } else if(villager.direction == 1 && col.transform.position.x < transform.position.x)
                    {
                        return;
                    }
                    col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(villager.direction * 6f,0);
                }
                
            
        }
        
        
    }
