using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoGame : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    // Start is called before the first frame update
    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }
    void Start()
    {
        videoPlayer.loopPointReached += LoadScene;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadScene(VideoPlayer vp)
    {
        if(vp == videoPlayer)
        SceneManager.LoadScene("Game");
    }
}
