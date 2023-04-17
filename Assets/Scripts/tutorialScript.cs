using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tutorialScript : MonoBehaviour
{

    public GameObject[] tutList;
    public int stage = 0;
    public GameObject nextButtonObject;

   
   public void nextButton()
    {
        if (stage >= 8)
        {
            Destroy(nextButtonObject);
            SceneManager.LoadScene("Final Scene");
        }
        tutList[stage].gameObject.SetActive(false);
        stage++;
        tutList[stage].gameObject.SetActive(true);

       

    }
  

    void Start()
    {
        
    }

    
}
