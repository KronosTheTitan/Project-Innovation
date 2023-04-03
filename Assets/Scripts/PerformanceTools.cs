using System;
using UnityEngine;
using UnityEngine.UI;

public class PerformanceTools : MonoBehaviour
{
    [SerializeField] private Text text;
    void Update()
    {
        text.text = "FPS = " + (1.0f / Time.deltaTime) + Environment.NewLine + "Memory usage = " + MemoryUsage();
    }

    long MemoryUsage()
    {
        return (System.GC.GetTotalMemory(false)/1048576);
    }
}