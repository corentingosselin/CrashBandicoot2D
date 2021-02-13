using UnityEngine;

namespace Environment
{
    public class LifeItem : Item
    {
        public override void Pickup(GameObject player)
        {
            base.Pickup(player);
            player.GetComponent<Player>().addLife();
        }
    }
}