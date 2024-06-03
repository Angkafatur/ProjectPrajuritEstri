using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menang : MonoBehaviour
{
    public GameObject GameWinUI;

    public void gameWin()
    {
        GameWinUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
        AudioManager.Instance.musicSource.Stop();
    }

}
