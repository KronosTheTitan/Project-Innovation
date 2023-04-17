using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    public void Pause()
    {
        Time.timeScale = 0;
        menu.SetActive(true);
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
