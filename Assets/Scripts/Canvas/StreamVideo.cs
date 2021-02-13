using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{

    
    /**
     * Animator video for the menu
     */

    public GameObject[] buttons;
    
    public VideoPlayer[] animatedBackGround;
    public RawImage rawImage;
    public VideoPlayer introVideo;
    public AudioSource audioSource;
    // Use this for initialization
    void Start () {
        StartCoroutine(PlayIntro());
        StartCoroutine(PopButton());
    }


    private void StartAnimation()
    {

        if (Random.Range(0, 100) >= 30)
        {
            StartCoroutine(PlayNextBackgroundAnimation(1F));
        }

    }


    private int currentVideo = 0;
    private IEnumerator PlayNextBackgroundAnimation(float time)
    {
        VideoPlayer vp = animatedBackGround[currentVideo];
        vp.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(time);
        while (!vp.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        rawImage.texture = vp.texture;
        vp.Play();
        
        currentVideo++;
        if (currentVideo >= buttons.Length)
            currentVideo = 0;
    }


    private IEnumerator PopButton()
    {
        yield return new WaitForSeconds(1.5F);
        foreach (var button in buttons)
        {
            button.SetActive(true);
        }
        InvokeRepeating("StartAnimation",0,5F);

    }
    
    
    private IEnumerator PlayIntro()
    {
        introVideo.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (!introVideo.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        rawImage.texture = introVideo.texture;
        introVideo.Play();
        if(audioSource != null)
            audioSource.Play();
        
      
    }
}