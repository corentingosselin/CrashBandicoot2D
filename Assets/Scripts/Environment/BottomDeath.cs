using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BottomDeath : MonoBehaviour
{
    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            col.gameObject.GetComponent<Player>().PlayerFalling();
        if (col.CompareTag("Damager"))
            col.gameObject.GetComponent<Ennemy>().Die(null);
        if (col.CompareTag("Ennemy"))
            col.gameObject.GetComponent<Ennemy>().Die(null);
        if (col.CompareTag("Item"))
            Destroy(col.gameObject);
    }

}
