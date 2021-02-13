using System;
using UnityEngine;


    public class EnnemySpawner : MonoBehaviour
    {

        [SerializeField] private GameObject enemy;
        [SerializeField] private float spawnRate = 4;
        

        public void StartSpawning()
        {
            InvokeRepeating("Spawn",1,spawnRate);
        }

        public void StopSpawning()
        {
            CancelInvoke();
            Destroy(gameObject);
        }

        private void Spawn()
        {
            Instantiate(enemy, new Vector3(transform.position.x , transform.position.y , transform.position.z), Quaternion.identity);
            
        }
        
        
    }
