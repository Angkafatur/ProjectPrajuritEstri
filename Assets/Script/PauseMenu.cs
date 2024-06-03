using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public Button resumeButton; // Referensi ke tombol Resume
    public Button restartButton;  // Referensi ke tombol Settings
    public Button homeButton;

    private bool isPaused = false;
    private Button[] buttons; // Array untuk menyimpan referensi tombol
    private int currentButtonIndex = 0; // Index tombol yang sedang difokuskan

    void Start()
    {
        // Inisialisasi array tombol
        buttons = new Button[] { resumeButton, restartButton, homeButton };

        // Menonaktifkan menu pause saat game dimulai
        pauseMenu.SetActive(false);

        Time.timeScale = 1;
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume(); 
            }
            else
            {
                Pause(); 
            }
        }

        if (isPaused)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                NavigateUp();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                NavigateDown();
            }
            else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                buttons[currentButtonIndex].onClick.Invoke();
            }
        }
    }

        

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;

        currentButtonIndex = 0;
        buttons[currentButtonIndex].Select();

    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Options()
    {
        SceneManager.LoadScene("Setting Menu");
        Time.timeScale = 1;
    }

    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
        AudioManager.Instance.musicSource.Stop();
    }

    void NavigateUp()
    {
        currentButtonIndex--;
        if (currentButtonIndex < 0)
        {
            currentButtonIndex = buttons.Length - 1;
        }
        buttons[currentButtonIndex].Select();
    }

    // Fungsi untuk navigasi ke bawah
    void NavigateDown()
    {
        currentButtonIndex++;
        if (currentButtonIndex >= buttons.Length)
        {
            currentButtonIndex = 0;
        }
        buttons[currentButtonIndex].Select();
    }
}