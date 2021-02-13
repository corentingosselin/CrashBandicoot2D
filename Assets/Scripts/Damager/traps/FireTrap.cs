using UnityEngine;

namespace DefaultNamespace
{
    
    
    /**
     * Not used for this project...
     */
    public class FireTrap : Trap
    {
        
        [SerializeField] private int time = 1;
        // [SerializeField] private int time = 5;
        private SpriteRenderer sprite;
        //[SerializeField] private Sprite off;
        
        protected override void Init()
        {
            base.Init();
            sprite = GetComponent<SpriteRenderer>();
        }

        protected override void TriggerStart()
        {
            InvokeRepeating("ToggleFlame", 0, time);
        }

        private bool onFire;
        private void ToggleFlame()
        {
            onFire = !onFire;
            sprite.enabled = onFire;
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                col.gameObject.GetComponent<Player>().KillPlayer(DeadType.BURN);
            } else if (col.gameObject.CompareTag("Damager"))
            {
                col.gameObject.GetComponent<Ennemy>().Die(null);
            }
        }
        
    }
}