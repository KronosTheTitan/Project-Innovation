using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{

    public TextMeshProUGUI heldTreasure;
    public TextMeshProUGUI depositedTreasure;
    public Button playButton;
    public Button settingsButton;
    public Button quitButton;
    public GameObject settingScreen;



    public void Start()
    {
        playButton.onClick.AddListener(playGame);
        settingsButton.onClick.AddListener(settingsScreen);
        quitButton.onClick.AddListener(quitGame);
    }

    public void updateStats()
    {
        heldTreasure.text = ("Treasure Held:" + GameManager.Instance.treasureCollected.ToString());
        depositedTreasure.text = ("Treasure Deposited:" + GameManager.Instance.treasureDeposited.ToString());
    }



    void playGame()
    {
        SceneManager.LoadScene("PlayTestSprint2");
    }

    void settingsScreen()
    {
        settingScreen.SetActive(true);

    }

    void quitGame()
    {
        Application.Quit();

    }

}
