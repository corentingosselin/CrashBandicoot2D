using System;
using UnityEngine;

    public class ShieldDefense : MonoBehaviour
    {

        [SerializeField] private GameObject villager;

        private VillagerEnnemy villagerBehaviour;
        
        private void Start()
        {

            villagerBehaviour = villager.GetComponent<VillagerEnnemy>();


        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.CompareTag("Player"))
            {
                Vector2 v = col.contacts[0].point - (Vector2) transform.position;
                if (Mathf.Abs(Vector2.Angle(v, Vector3.up)) < 67.5)
                {
                    villagerBehaviour.OnJumpingTop(col.gameObject);
                }                
            
            }
        }
        
        
    }
