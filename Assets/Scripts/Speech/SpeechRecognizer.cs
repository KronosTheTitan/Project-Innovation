﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SpeechRecognizerPlugin;

public class SpeechRecognizer : MonoBehaviour, ISpeechRecognizerPlugin
{
    [SerializeField] private Button startListeningBtn = null;
    [SerializeField] private Button stopListeningBtn = null;
    [SerializeField] private Toggle continuousListeningTgle = null;
    [SerializeField] private TMP_InputField maxResultsInputField = null;
    [SerializeField] private TextMeshProUGUI resultsTxt = null;
    [SerializeField] private TextMeshProUGUI errorsTxt = null;
    [SerializeField] private TextMeshProUGUI weaponsTxt = null;



    private SpeechRecognizerPlugin plugin = null;

    private void Start()
    {
        plugin = SpeechRecognizerPlugin.GetPlatformPluginVersion(this.gameObject.name);

        startListeningBtn.onClick.AddListener(StartListening);
        stopListeningBtn.onClick.AddListener(StopListening);
        continuousListeningTgle.onValueChanged.AddListener(SetContinuousListening);

        maxResultsInputField.onEndEdit.AddListener(SetMaxResults);
    }

    private void StartListening()
    {
        plugin.StartListening();
    }

    private void StopListening()
    {
        plugin.StopListening();
    }

    private void SetContinuousListening(bool isContinuous)
    {
        plugin.SetContinuousListening(isContinuous);
    }



    private void SetMaxResults(string inputValue)
    {
        if (string.IsNullOrEmpty(inputValue))
            return;

        int maxResults = 2;
        plugin.SetMaxResultsForNextRecognition(maxResults);
    }



    public void OnResult(string recognizedResult)
    {
        char[] delimiterChars = { '~' };
        string[] result = recognizedResult.Split(delimiterChars);

        resultsTxt.text = "";
        for (int i = 0; i < result.Length; i++)
        {
            resultsTxt.text += result[i] + '\n';

            if (resultsTxt.text.Contains("switch"))
            {
                weaponsTxt.text = "Weapons Switched!";
                resultsTxt.text = null;
            }
        }
        
    }


    /* If results.txt == "Switch" or "Switch weapons"{
    Switch weapons
    }
    */

    public void OnError(string recognizedError)
    {
        ERROR error = (ERROR)int.Parse(recognizedError);
        switch (error)
        {
            case ERROR.UNKNOWN:
                Debug.Log("<b>ERROR: </b> Unknown");
                errorsTxt.text += "Unknown";
                break;
            case ERROR.INVALID_LANGUAGE_FORMAT:
                Debug.Log("<b>ERROR: </b> Language format is not valid");
                errorsTxt.text += "Language format is not valid";
                break;
            default:
                break;
        }
    }
}
