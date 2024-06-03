using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneStart2 : MonoBehaviour
{

    public VideoPlayer videoPlayer;
    public VideoClip videoClip2;


    private AudioManager audioManager; // Tambahkan referensi ke AudioManager
    private bool isFirstVideoFinished = false;

    void Start()
    {
        // Hide the loading bar initially

        // Set the first video clip
        videoPlayer.clip = videoClip2;

        // Subscribe to video end event
        videoPlayer.loopPointReached += OnVideoEnd;

        // Start playing the first video
        videoPlayer.Play();

        // Cari AudioManager
        audioManager = AudioManager.Instance;
        if (audioManager != null)
        {
            audioManager.PauseMusic();
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        if (!isFirstVideoFinished)
        {
            Debug.Log("Second video ended. Showing loading bar and loading scene.");
            // Show the loading bar when the second video ends
            SceneManager.LoadScene("Main Menu");
        }
    }
}
