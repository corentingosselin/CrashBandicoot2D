using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class BoxBreakParticle : MonoBehaviour
    {

        private float radius = 0.1F;
        public Sprite[] sprites;
        private void Awake()
        {
            sprites = Resources.LoadAll<Sprite>("Boxes/broken_box");
        }

        
        private int particlePerSprite = 2;
        public void SetupParticles()
        {
            foreach (var sprite in sprites)
            {
                for (int i = 0; i < particlePerSprite; i++)
                {
                    
                    GameObject go = new GameObject(sprite.name + "_" + i);
                    go.layer = 16;
                    go.transform.position = transform.position;
                    float z = Random.Range(0, 360);
                    go.transform.localEulerAngles = new Vector3(0,0, z);
                    float size = 1.2F;
                    transform.localScale = new Vector3(size,size,size);

                    SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
                    sr.sprite = sprite;
                    sr.sortingLayerName = "boxes";
                    

                    //random spawn
                    float x = Random.Range(-radius, radius);
                    float y = Random.Range(-radius, radius);
                    go.transform.position = new Vector3(transform.position.x + x, transform.position.y + y , transform.position.z);

                    
                    //set direction opposed 
                    Vector2 direction = (go.transform.position - transform.position).normalized;
                    Rigidbody2D rb = go.AddComponent<Rigidbody2D>();
                    
                    rb.gravityScale = 0.2F;
                    float speed = 0.5F;
                    direction.Scale(new Vector2(speed,0.8F));
                    rb.velocity = direction;

                    
                    go.AddComponent<BoxCollider2D>();
                    Destroy(go,Random.Range(0.2F,1F));
                    
                    

                }

            }
        }





        private void Update()
        {
            
        }
    }
}