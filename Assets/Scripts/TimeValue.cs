using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeValue {

    private int minute;
    private int hour;
    private int day;
    private int month;
    private int year;

    public TimeValue(int min, int h, int d, int m, int y)
    {
        Minute = min;
        Hour = h;
        Day = d;
        Month = m;
        Year = y;

    }

    public int Minute
    {
        get
        {
            return minute;
        }

        set
        {
            minute = value;
        }
    }

    public int Hour
    {
        get
        {
            return hour;
        }

        set
        {
            hour = value;
        }
    }

    public int Day
    {
        get
        {
            return day;
        }

        set
        {
            day = value;
        }
    }

    public int Month
    {
        get
        {
            return month;
        }

        set
        {
            month = value;
        }
    }

    public int Year
    {
        get
        {
            return year;
        }

        set
        {
            year = value;
        }
    }
}
