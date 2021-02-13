using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AkuAku : MonoBehaviour
{
    
    private int FeatherNumber = 0;
    private GameObject playerObject;
    private CharacterController2D characterController;

    public AkuAku()
    {

    }
    
    
    public Sprite[] sprites;
    void Awake()
    {
        sprites = Resources.LoadAll<Sprite>("Sprites/aku_aku");

        //only for unity ui
        // sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath("Assets/Ressources/" + "aku_aku" + ".psd").OfType<Sprite>().ToArray();
    }
 
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        characterController = playerObject.GetComponent<CharacterController2D>();
        playerTransform = playerObject.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowPlayer();

            
    }

    public void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }



    private Transform playerTransform;
    [SerializeField] private float smoothTime = 0.35F;
    private Vector3 velocity = Vector3.zero; 
    private void FollowPlayer()
    {
        Vector3 targetPosition = playerTransform.TransformPoint(new Vector3(-0.2F, 0.5F, 0));
        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
    
    
    
    public bool RemoveFeather()
    {
        if (FeatherNumber <= 0) return false;
        FindObjectOfType<AudioManager>().Play("akuaku_death");
        this.FeatherNumber--;
        if (FeatherNumber <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = sprites[this.FeatherNumber - 1];

        }
        return true;
    }

    public void AddFeather()
    {
        if (FeatherNumber >= 3) return;
        if (FeatherNumber == 0)
        {
            //play aku sound
            FindObjectOfType<AudioManager>().Play("akuaku");
            gameObject.SetActive(true);
        }
        GetComponent<SpriteRenderer>().sprite = sprites[FeatherNumber];
        this.FeatherNumber++;
    }
}
