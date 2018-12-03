using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour {

    public Text timeText;
    public GameObject taskBroker;
    public Button[] speedButton;

    public float basetimespeed;
    

    private int[] timeSpeeds = { 1, 2, 4 };
    private float lastChange = 0.0f;
    private int minutes = 0;
    private int hours = 6;
    private int days = 1;
    private int months = 1;
    private int years = 2050;
    private int timespeed = 0;

    private TaskBroker taskBrokerScript;

	// Use this for initialization
	void Start () {
        UpdateClockDisplay();
        SetSpeed(0);
        taskBrokerScript = taskBroker.GetComponent<TaskBroker>();
     }
	
	// Update is called once per frame
	void Update () {
		if(Time.time - lastChange > basetimespeed/timeSpeeds[timespeed])
        {
            minutes++;
            if(minutes >= 60) {
                minutes = 0;
                hours++;
                if (hours == 24)
                {
                    hours = 0;
                }
            }
            UpdateClockDisplay();
            taskBrokerScript.Update_TaskTimes();
            lastChange = Time.time;

        }
	}

    void UpdateClockDisplay ()
    {
        timeText.text = hours.ToString().PadLeft(2, '0') + ":" + minutes.ToString().PadLeft(2, '0') + "    " + days.ToString().PadLeft(2, '0') + "." + months.ToString().PadLeft(2, '0') + "." + years.ToString().PadLeft(2, '0');
    }

    public int GetMinutes()
    {
        return minutes;
    }
    public int GetHours() { return hours; }
    public int GetDays() { return days; }
    public int GetMonths() { return months; }
    public int GetYears() { return years; }

    public TimeValue GetCurrentTime()
    {
        return new TimeValue(minutes, hours, days, months, years);
    }

    public void SetSpeed(int speed)
    {
        speedButton[timespeed + 1].GetComponent<Image>().color = Color.white;
        timespeed = speed;
        speedButton[timespeed + 1].GetComponent<Image>().color = Color.green;
    }
}
