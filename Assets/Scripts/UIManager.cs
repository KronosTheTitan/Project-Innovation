using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{

    public TextMeshProUGUI heldTreasure;
    public TextMeshProUGUI depositedTreasure;
    public Button playButton;
    public Button settingsButton;
    public Button quitButton;
    public GameObject settingScreen;
    public GameObject[] missleList;
    public Slider volumeSlider;




    public void Start()
    {
        playButton.onClick.AddListener(playGame);
        settingsButton.onClick.AddListener(settingsScreen);
        quitButton.onClick.AddListener(quitGame);

    }

    public Slider throttleSlider;
    

    [SerializeField] AudioSource engineSource;


    

    


    public void updateStats()
    {
        heldTreasure.text = ("Treasure Held:" + GameManager.Instance.treasureCollected.ToString());
        depositedTreasure.text = ("Treasure Deposited:" + GameManager.Instance.treasureDeposited.ToString());
    }

    public void removeAmmo(int x)
    {
        missleList[x].gameObject.SetActive(false);
    }

    public void updateAmmo(int x)
    {
        for(int i = 0; i < x; i++)
        {
            missleList[i].gameObject.SetActive(true);
        }
    }

   

    void playGame()
    {
        SceneManager.LoadScene("CalibrateScene");
    }

    void settingsScreen()
    {
        settingScreen.SetActive(true);

    }

    void quitGame()
    {
        Application.Quit();

    }

    public void pauseMenu()
    {
        settingScreen.gameObject.SetActive(true);
        Time.timeScale = 0;

    }

    public void resume()
    {
        settingScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    

}
