using System.Collections;
using System.Linq;
using Canvas;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private AudioManager audioManager;
    
    [SerializeField] private GameObject lifeCanvas;
    [SerializeField] private GameObject wumpaCanvas;

    private Displayer lifeDisplayer;
    private Displayer wumpaDisplayer;
    
    [SerializeField] private GameObject attackHitBox;
    [SerializeField] private GameObject deadPlayer;

    private AkuAku akuAku;
    private Animator animator;

    private bool dead = false;

    public Vector3 spawnPoint;
    

    private CheckPointSystem checkPointSystem;
   
    public bool isJumping = false;
    

    private void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        checkPointSystem = GameObject.FindGameObjectWithTag("CheckPointSystem").GetComponent<CheckPointSystem>();
        animator = GetComponent<Animator>();
        akuAku = GameObject.FindGameObjectWithTag("AkuAku").GetComponent<AkuAku>();
        lifeDisplayer = lifeCanvas.GetComponent<Displayer>();
        wumpaDisplayer = wumpaCanvas.GetComponent<Displayer>();
        spawnPoint = transform.position;
        
        
        //get the position spawn
        if (checkPointSystem.checkpointsUnlocked.Count > 0)
        {
            lifeDisplayer.DisplayInfo(checkPointSystem.lifeAmount);
            Vector3 position = checkPointSystem.checkpointsUnlocked.Last();
            position.y += 0.35F;
            transform.position = position;
        }
        else
        {
            transform.position = spawnPoint;
            TeleportStart();
        }
        

    }

    private void TeleportStart()
    {

        //disable render
        GetComponent<SpriteRenderer>().enabled = false;
        //disable movement
        GetComponent<CharacterController2D>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;

        //Play Teleporter Animation
        audioManager.Play("portal_start");
        GameObject.FindGameObjectWithTag("Teleporter").GetComponent<Animator>().Play("teleport_start");
        StartCoroutine(AllowPlaying());
    }

    private IEnumerator AllowPlaying()
    {
        yield return new WaitForSeconds(3F);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CharacterController2D>().enabled = true;
        GetComponent<PlayerMovement>().enabled = true;
    }
    
    
    private int tornadoCount = 2;
    public bool attacking = false;
    // Update is called once per frame
    void Update()
    {
        if (dead) return;
        if (Input.GetButtonDown("Fire1") && !attacking && !PauseMenu.Paused && GetComponent<SpriteRenderer>().enabled)
        {
            if(tornadoCount > 0)
                SpinAttack();
        }
        
    }
    
    
    public void SpinAttack()
    {
        audioManager.Play("spin");
        attacking = true;
        attackHitBox.SetActive(true);
        tornadoCount--;
        animator.Play("crash_attack");
        StartCoroutine(StopSpinAttack());
        if(tornadoCount >= 1)
            StartCoroutine(ResetAttack());

    }

    private IEnumerator StopSpinAttack()
    {
        yield return new WaitForSeconds(0.3F);
        attacking = false;
        animator.Rebind();
        attackHitBox.SetActive(false);
    }
    
    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(2);
        tornadoCount = 2;
    }
    
    

    public void addLife()
    {
        checkPointSystem.lifeAmount++;
        audioManager.Play("life");
        lifeDisplayer.DisplayInfo(checkPointSystem.lifeAmount);

    }



    public IEnumerator Respawn()
    {
        
      
        checkPointSystem.lifeAmount--;
        if (checkPointSystem.lifeAmount < 0)
        {
            checkPointSystem.lifeAmount = 0;
            checkPointSystem.ResetCheckPoint(); 
            //here reset default music 
            FindObjectOfType<ThemeMusic>().RebindDefaultMusic();
            
        }
        yield return new WaitForSeconds(4F);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (deadGameObject != null)
        {
            Destroy(deadGameObject);
        }
        
        
        //freeze player
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<Animator>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        animator.Rebind();
        GetComponent<CharacterController2D>().enabled = true;
        GetComponent<PlayerMovement>().enabled = true;
        dead = false;

       
    }
    
    
    // Use this only for ennemies or obstacles
    //this kind of kill check akuaku
    public void KillPlayer(DeadType deadType)
    {
        if (dead) return;
        if (akuAku.RemoveFeather())
        {
            return;
        }
        audioManager.Play("woah");
        if (deadType == DeadType.HEAVEN)
        {
            audioManager.Play("angel");
        } else if (deadType == DeadType.BURN)
        {
            audioManager.Play("burn");
        }

        //freeze player
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        animator.Rebind();
        
        //first check if aku aku is enabled ?
        StartCoroutine(PlayDieAnimation(deadType));
        StartCoroutine(Respawn());
        GetComponent<CharacterController2D>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        dead = true;
    }


    private GameObject deadGameObject;
    public IEnumerator PlayDieAnimation(DeadType deadType)
    {
        yield return new WaitForSeconds(0.5F);

        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        deadGameObject = Instantiate(deadPlayer, 
            new Vector3(transform.position.x , transform.position.y , transform.position.z), Quaternion.identity);
        deadGameObject.GetComponent<Animator>().Play("crash_" + deadType.ToString().ToLower());
        if (deadType == DeadType.BURN)
        {
            //TODO play burn sound
            Rigidbody2D rigidbody2D = deadGameObject.AddComponent<Rigidbody2D>();
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        } else if (deadType == DeadType.HEAVEN)
        {
            audioManager.Play("angel");
            deadGameObject.AddComponent<DeadPlayerHeaven>();
        }
        
    }
    
    
    
    

    // Use this only for player falling
    public void PlayerFalling()
    {
        if (dead) return;
        //play fall animation and tp to the last checkpoint
        //TODO play falling sound
        StartCoroutine(PlayDieAnimation(DeadType.HEAVEN));
        StartCoroutine(Respawn());
        GetComponent<CharacterController2D>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        dead = true;
    }
    
    


    public void EatWumpaFruit()
    {
        checkPointSystem.wumpaFruitAmount++;
        wumpaDisplayer.DisplayInfo(checkPointSystem.wumpaFruitAmount);
        if (checkPointSystem.wumpaFruitAmount >= 100)
        {
            addLife();
            checkPointSystem.wumpaFruitAmount = 0;
        }
    }
    

    

}