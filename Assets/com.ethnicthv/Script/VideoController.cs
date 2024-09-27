using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace com.ethnicthv.Script
{
    public class VideoController : MonoBehaviour
    {
        public VideoPlayer videoPlayer;
        public int targetScene = 0;
        
        private void Start()
        {
            videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,"myFile.mp4");
            videoPlayer.loopPointReached += OnVideoEnd;
            videoPlayer.Play();
        }
        
        private void OnVideoEnd(VideoPlayer source)
        {
            Debug.Log("Video ended");
            SceneManager.LoadScene(targetScene);
        }
    }
}