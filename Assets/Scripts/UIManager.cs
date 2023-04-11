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


    public void updateStats()
    {
        heldTreasure.text = ("Treasure Held:" + GameManager.Instance.treasureCollected.ToString());
        depositedTreasure.text = ("Treasure Deposited:" + GameManager.Instance.treasureDeposited.ToString());
    }

    


}
