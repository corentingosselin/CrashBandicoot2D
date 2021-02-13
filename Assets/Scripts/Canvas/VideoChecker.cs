using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoChecker : MonoBehaviour
{
    private VideoPlayer vid;

    //check video ended

    void Start()
    {
        vid = GetComponent<VideoPlayer>();
        vid.loopPointReached += CheckOver;
    }
 
    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }

}
