using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private int maxFramesForAverage = 60;  //maximum frames to average over

    private static float lastFpsCalculated = 0f;
    private readonly List<float> frameTimes = new();
    private bool isVisible;
    private TextMeshProUGUI textMeshProUGUI;

    // Use this for initialization
    void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        lastFpsCalculated = 0f;
        frameTimes.Clear();
        isVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            isVisible = !isVisible;
            textMeshProUGUI.enabled = isVisible;
        }

        if (isVisible)
        {
            AddFrame();
            lastFpsCalculated = CalculateFps();
            textMeshProUGUI.text = $"{lastFpsCalculated:n0} FPS\n{Screen.width}x{Screen.height}";            
        }
    }

    private void AddFrame()
    {
        frameTimes.Add(Time.unscaledDeltaTime);
        if (frameTimes.Count > maxFramesForAverage)
        {
            frameTimes.RemoveAt(0);
        }
    }

    private float CalculateFps()
    {
        float newFPS;

        float totalTimeOfAllFrames = 0f;
        foreach (float frame in frameTimes)
        {
            totalTimeOfAllFrames += frame;
        }
        newFPS = ((float)(frameTimes.Count)) / totalTimeOfAllFrames;

        return newFPS;
    }

    public static float GetCurrentFps()
    {
        return lastFpsCalculated;
    }
}
