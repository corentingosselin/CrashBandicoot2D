using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DestroyableItem : MonoBehaviour
{

    
    /**
     *
     * Not used now for this project
     */

    [SerializeField] private float forceY;
    

    private Rigidbody2D[] rigidbodies;
    
    // Start is called before the first frame update
    void Start()
    {
        Component[] components = gameObject.GetComponentsInChildren<Component>(false);
        rigidbodies = new Rigidbody2D[components.Length];
        for (var i = 0; i < components.Length; i++)
        {
            if (components[i].GetComponent<Rigidbody2D>() != null)
            {
               rigidbodies[i] = components[i].GetComponent<Rigidbody2D>();
               float randTorque = Random.Range(-5, 5);
               float randForceY = Random.Range(-forceY,  forceY);
               rigidbodies[i].AddForce(new Vector2(randForceY,randForceY));
               rigidbodies[i].AddTorque(randTorque);
            }
        }
        Invoke("DestroySelf",Random.Range(3F,6F));
    }
    

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}