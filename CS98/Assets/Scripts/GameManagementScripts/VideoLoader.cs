using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
 
public class VideoLoader : MonoBehaviour
{
 
     VideoPlayer video;
 
    void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += CheckOver;
 
         
    }
 
 
     void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        // goes directly to Tutorial after the end of the animation
        SceneManager.LoadScene("Tutorial"); 
    }
}