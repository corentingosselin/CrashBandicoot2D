using UnityEngine;

public class ThemeMusic : MonoBehaviour
{
        
    public static ThemeMusic instance;
        

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            defaultClip = GetComponent<AudioSource>().clip;
            defaultVolume = GetComponent<AudioSource>().volume;

            DontDestroyOnLoad(gameObject);
        }

    }

    private AudioClip defaultClip;
    private float defaultVolume; 
    public void RebindDefaultMusic()
    {
        if (!GetComponent<AudioSource>().clip.Equals(defaultClip))
        {
            GetComponent<AudioSource>().clip = defaultClip;
            GetComponent<AudioSource>().volume = defaultVolume;
            GetComponent<AudioSource>().Play();
        }

    }

 
}