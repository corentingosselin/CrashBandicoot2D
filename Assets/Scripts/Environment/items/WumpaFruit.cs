using System;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using Random = UnityEngine.Random;

namespace Environment
{
    
    
    public class WumpaFruit : Item
    {
        
        public AudioClip eatClip;
        
        //Just idea 
        /*  [SerializeField]
          GameObject wumpaDestroy;
          
          public override void Crush()
          {
              Instantiate(wumpaDestroy, new Vector3(transform.position.x, transform.position.y, transform.position.z),
                  Quaternion.identity);
          }
  
          private bool once = false;
          private void OnTriggerEnter2D(Collider2D other)
          {
              if (!other.gameObject.CompareTag("Damager")) return;
              if (once) return;
              once = true;
              Crush();
              Destroy(gameObject);
  
          }*/

        public override void Pickup(GameObject player)
        {
            GetComponent<AudioSource>().volume = Random.Range(0.1F, 0.3F);
            GetComponent<AudioSource>().PlayOneShot(eatClip);
            base.Pickup(player);
            player.GetComponent<Player>().EatWumpaFruit();
        }

        public override void Drop()
        { 
            base.Drop();
           rigidbody2D.velocity = new Vector2(Random.Range(-1F,1F), 2F);

        }
    }
}