using System;
using UnityEngine;
using System.Collections;
using System.Linq;

public class CheckPointBox : Box
{


    public AudioSource backgroundMusic;
    public AudioClip newMusic;
   

    // Each box has a unique identifier, useful to get which checkpoint is done
    public int identifier;
    private CheckPointSystem checkPointSystem;

    private void Awake()
    {
        backgroundMusic = FindObjectOfType<ThemeMusic>().gameObject.GetComponent<AudioSource>();
    }

    public override void Init()
    {
        base.Init();
        boxName = "checkpoint";
        checkPointSystem = GameObject.FindGameObjectWithTag("CheckPointSystem").GetComponent<CheckPointSystem>();
        // If the identifier is lower than the last checkpoint identifier, let's open it
        if (identifier  <= checkPointSystem.lastIdentifier)
        {
            StartCoroutine(OpenCheckPoint());
            transform.GetChild(2).gameObject.SetActive(true);
            playerCollider.enabled = false;
            broken = true;
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        }
    }

    private IEnumerator OpenCheckPoint()
    {
        yield return new WaitForSeconds(0.1F);
        animator.Play(boxName + "break");
    }
    
    public override void OnJumpingBox(GameObject pl)
    {
        base.OnJumpingBox(pl);
        Break();
    }

    public override void Break()
    {
        animator.Play(boxName + "break");
        boxCollider.enabled = false;
        playerCollider.enabled = false;
        broken = true;
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        checkPointSystem.SetCheckpoint(transform.position, identifier);
        transform.GetChild(2).gameObject.SetActive(true);
        
        //special feature, when the game is getting hard let's change the music !
        if (backgroundMusic != null && newMusic != null)
        { 
            backgroundMusic.clip = newMusic;
            backgroundMusic.volume = 0.03F;
            backgroundMusic.Play();
        }
    }

    
}
