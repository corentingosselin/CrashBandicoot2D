using System.Collections;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    /**
     * spike trap is the only trap, that"s why it is not inherited by Trap.cs
     */
    
    private Animator animator;
    private AudioSource[] audioSource;
    
    [SerializeField] private float firstStartDelay = 0F;
    
    private Vector2 startPos;
    private Vector2 endPos;
    [SerializeField] private float heightOffset = 1F;
    [SerializeField] private float speed = 6;
    
    [SerializeField] private float shakingDuration = 2;
    [SerializeField] private float waitBeforeUp = 2;
    [SerializeField] private float waitBeforeReshaking = 1;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        startPos = new Vector2(transform.position.x, transform.position.y);
        endPos = new Vector2(transform.position.x,transform.position.y - heightOffset);
        audioSource = GetComponents<AudioSource>();
        
        
        StartCoroutine(FirstStart());
    }
    
    private void Shake() {
        
    }

    private bool isShaking = false;
    private bool move = false;
    private bool replace = false;
    
    private void Move()
    {
        //send spikeDown() in 2 sec
        transform.position = Vector2.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, endPos) <= 0.1)
        {
            move = false;
            StartCoroutine(GoUp());
        }
    }
    
    public IEnumerator FirstStart()
    {
        yield return new WaitForSeconds(firstStartDelay);
        StartCoroutine(GoDown());
        isShaking = true;
    }

    private void MoveReplace()
    {
        transform.position = Vector2.MoveTowards(transform.position, startPos, 1 * Time.deltaTime);
        if (Vector2.Distance(transform.position, startPos) <= 0.1)
        {
            replace = false;
            transform.position = startPos;
            StartCoroutine(StartAnim());

        }
    }
    
    void Update()
    {
        if(move)
            Move();
        
        if(isShaking)
            Shake();
        
        if(replace)
            MoveReplace();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Player>().KillPlayer(DeadType.HEAVEN);
        } else if (col.gameObject.CompareTag("Damager"))
        {
            col.gameObject.GetComponent<Ennemy>().Die(null);
        } else if (col.gameObject.CompareTag("Ennemy"))
        {
            col.gameObject.GetComponent<Ennemy>().Die(null);
        }
    }

    public IEnumerator GoDown()
    {
        yield return new WaitForSeconds(shakingDuration);
        isShaking = false;
        move = true;
    }
    
    public IEnumerator StartAnim()
    {
        yield return new WaitForSeconds(waitBeforeReshaking);
        StartCoroutine(GoDown());
        isShaking = true;
        audioSource[1].Play();
        animator.Play("spike_shake");

    }
    
    public IEnumerator GoUp()
    {
        audioSource[0].Play();
        yield return new WaitForSeconds(waitBeforeUp);
        replace = true;
    }
}