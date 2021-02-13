using UnityEngine;

public class Grass : MonoBehaviour
{
	
	/**
	 * Not used right now
	 */

    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
	{
        if(other.CompareTag("Player"))
		{
            anim.Play("hit_plant");
		}
	}
}
