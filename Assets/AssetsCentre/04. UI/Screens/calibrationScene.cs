using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class calibrationScene : MonoBehaviour
{

   public GameObject waitScreen;
    public GameObject calibrationScreen;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(calibrate());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   IEnumerator calibrate()
    {

        yield return new WaitForSeconds(3);
        calibrationScreen.gameObject.SetActive(true);
        waitScreen.gameObject.SetActive(false);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("tutorialScene");


    }
}
