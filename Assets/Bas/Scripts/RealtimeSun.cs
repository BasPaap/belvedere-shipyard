using System;
using UnityEngine;
using UnityEngine.Serialization;

public class RealtimeSun : MonoBehaviour
{
    [SerializeField, FormerlySerializedAs("noonHeight")] private float apex;
    [SerializeField, FormerlySerializedAs("sunriseXPosition")] private float riseXPosition;
    [SerializeField] private float riseYPosition;
    [SerializeField, FormerlySerializedAs("sunsetXPosition")] private float setXPosition;
    [Space]
    [Header("Rise time")]
    [SerializeField, Range(0, 23)] private int riseHour;
    [SerializeField, Range(0, 59)] private int riseMinute;
    [Header("Set time")]
    [SerializeField, Range(0, 23)] private int setHour;
    [SerializeField, Range(0, 59)] private int setMinute;
    [Space]
    [Header("Time of day")]
    [SerializeField] private bool isRealtime = true;
    [SerializeField, Range(0, 23)] private int currentHour;
    [SerializeField, Range(0, 59)] private int currentMinute;

    [SerializeField] private float tmpNormalizedTime;
    [SerializeField] private float tmpRiseTime;
    [SerializeField] private float tmpSetTime;

    [SerializeField] private float tmpValue;

    private float normalizedRiseTime;
    private float normalizedSetTime;

    private const float secondsPerDay = 60 * 60 * 24;
    

    private void Awake()
    {
        normalizedRiseTime = GetNormalizedTime(new TimeSpan(riseHour, riseMinute, 0));
        normalizedSetTime = GetNormalizedTime(new TimeSpan(setHour, setMinute, 0));
        tmpRiseTime = normalizedRiseTime;
        tmpSetTime = normalizedSetTime;
    }

    // Update is called once per frame
    void Update()
    {
        //var currentTime = new TimeSpan(currentHour, currentMinute, 0);

        if (isRealtime)
        {
            currentHour = DateTime.Now.Hour;
            currentMinute = DateTime.Now.Minute;
        }

        var normalizedTime = GetNormalizedCurrentTime();
        tmpNormalizedTime = normalizedTime;



        //var currentX = 0.5f * MathF.Sin(2f * normalizedTime - 0.5f * Mathf.PI) + 0.5f;
        //var currentX = Mathf.Sin(normalizedTime * Mathf.PI) - 0.5f;
        //currentX = (currentX * apex * 2) - tmpValue;

        // X = Height of the celestial body in the sky
        // Y = horizontal position of the celestial body in the sky

        ////var currentX = (Mathf.Sin(normalizedTime * Mathf.PI) * (apex * 2)) - apex;
        //var currentX = (apex + riseYPosition) * Mathf.Sin(normalizedTime * Mathf.PI) - riseYPosition;
        //var currentY = riseXPosition + (setXPosition - riseXPosition) * normalizedTime * 2;

        var normalizedRiseTime = GetNormalizedTime(new TimeSpan(riseHour, riseMinute, 0));
        var normalizedSetTime = GetNormalizedTime(new TimeSpan(setHour, setMinute, 0));

        var t = Mathf.InverseLerp(normalizedRiseTime, normalizedSetTime, normalizedTime);
        var tY = t > 0.5f ? (1 - t) : t;

        var currentX = Mathf.Lerp(riseYPosition, apex, tY);
        var currentY = Mathf.Lerp(riseXPosition, setXPosition, t);


        transform.rotation = Quaternion.Euler(currentX, currentY, transform.rotation.eulerAngles.z);
    }

    public static float Remap(float value, float from1, float from2, float to1, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    private float GetNormalizedCurrentTime()
    {
        var currentTime = isRealtime ? DateTime.Now.TimeOfDay : new TimeSpan(currentHour, currentMinute, 0);
        return GetNormalizedTime(currentTime);
    }

    private static float GetNormalizedTime(TimeSpan time)
    {
        var secondsPassedToday = (float)time.TotalSeconds;
        return secondsPassedToday / secondsPerDay;
    }
}