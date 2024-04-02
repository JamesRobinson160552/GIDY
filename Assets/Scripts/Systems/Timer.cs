using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    public static Timer i { get; private set; }
    public long newDayThreshold;
    public long currentTicks;
    public long ticksToNextDay;
    public TimeSpan timeToNextDay;
    public string timeToNextDayString;
    public bool newDay;

    void Awake()
    {
        if (i == null) i = this;
        newDay = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        newDayThreshold = long.Parse(PlayerPrefs.GetString("newDay", "0"));
        DateTime now = DateTime.Now;
        currentTicks = now.Ticks;

        if (newDayThreshold == 0 || newDayThreshold < now.Ticks)
        {
            newDay = true;
            newDayThreshold = now.AddDays(1).Ticks;
            PlayerPrefs.SetString("newDay", newDayThreshold.ToString());
        }
    }

    public void FixedUpdate()
    {
        ticksToNextDay = newDayThreshold - DateTime.Now.Ticks;
        timeToNextDayString = TimeSpan.FromTicks(ticksToNextDay).ToString("hh':'mm':'ss");
    }
}
