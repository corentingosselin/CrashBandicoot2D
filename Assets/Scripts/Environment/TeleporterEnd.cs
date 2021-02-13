using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TeleporterEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            //destroy player maybe ?
            transform.parent.GetComponent<Animator>().Play("teleport_end");
            FindObjectOfType<AudioManager>().Play("portal_end");
            Destroy(FindObjectOfType<AkuAku>().gameObject);
            Destroy(other.gameObject);
            FindObjectOfType<ThemeMusic>().gameObject.SetActive(false);
            StartCoroutine(Win());
        }
        
    }

    private IEnumerator Win()
    {
        yield return new WaitForSeconds(2.5F);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
