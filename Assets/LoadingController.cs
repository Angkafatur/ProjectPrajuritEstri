using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Video;
public class LoadingController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Slider loadingBar;
    public VideoClip videoClip1;
    public Text progressText;
    public GameObject loadingScreen;


    private AudioManager audioManager; // Tambahkan referensi ke AudioManager
    private bool isFirstVideoFinished = false;

    void Start()
    {
        // Hide the loading bar initially
        loadingBar.gameObject.SetActive(false);

        // Set the first video clip
        videoPlayer.clip = videoClip1;

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
            loadingBar.gameObject.SetActive(true);
            StartCoroutine(LoadSceneAsync());
        }
    }

    IEnumerator LoadSceneAsync()
    {
        Debug.Log("Starting async scene loading.");
        AsyncOperation operation = SceneManager.LoadSceneAsync(2);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            // Update the loading bar progress
            loadingBar.value = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = operation.progress;
            progressText.text = loadingBar.value * 100f + "%";
            yield return null;

            if (audioManager != null)
            {
                audioManager.UnPauseMusic();
            }
        }
    }
}
