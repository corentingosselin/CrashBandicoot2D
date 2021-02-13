using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using Random = UnityEngine.Random;


namespace Environment
{
    public abstract class Item : MonoBehaviour
    {

        //wait 1 sec before allowing pickup
        protected bool pickableTime = false;

        protected bool pickable = true;
        protected Rigidbody2D rigidbody2D;
        protected Transform transform;
        public bool isFromBox = true;

        public virtual void Pickup(GameObject player)
        {
            if (!pickable) return;
            pickable = false;
            GetComponent<SpriteRenderer>().enabled = false;
            if(GetComponent<Animator>() != null) 
                GetComponent<Animator>().enabled = false;
            
            foreach (var component in GetComponents<Collider2D>())
            {
                component.enabled = false;

            }
            Destroy(gameObject,1F);
        }
        
        
        
        private void OnCollisionStay2D(Collision2D col)
        {
            if (isFromBox)
            {
                if (!pickable)
                {
                    if (pickableTime && (col.gameObject.CompareTag("Floor") || col.gameObject.CompareTag("Box")))
                    {
                        pickable = true;
                        rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                        this.originalY = transform.position.y;
                    }
                }
            }
            
        }
        
        private IEnumerator AllowPickupTime()
        {
            yield return new WaitForSeconds(0.1F);
            pickableTime = true;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.CompareTag("Player") && (pickable && pickableTime) )
            {
                Pickup(other.gameObject);
            } else if (other.gameObject.CompareTag("Player") && isFromBox)
            {
                //pickup directly
                Pickup(other.gameObject);
            }

        }

        public virtual void Drop()
        {
            isFromBox = true;
            pickable = false;
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        
        private void Awake()
        {
            transform = GetComponent<Transform>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            //let's ignore collision, but still trigger it
            if (isFromBox)
            {
                Physics2D.IgnoreCollision(FindObjectOfType<Player>().GetComponent<Collider2D>(),
                    GetComponent<Collider2D>());
                
            }
        }

        private void Start()
        {
            this.originalY = transform.position.y;
            StartCoroutine(AllowPickupTime());
        }
        
        
        
        private float originalY;
        public float floatStrength = 0.03F; 
        
        void Update()
        {
            if(pickable)
                transform.position = new Vector3(transform.position.x,
                    originalY + ((float)Math.Sin(Time.time) * floatStrength),
                    transform.position.z);
        }
        
    }
}