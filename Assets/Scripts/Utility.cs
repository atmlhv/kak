using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Utility
{
    public const int HOUR_SECOND = 3600;
    public const int MINUTE_SECOND= 60;

    public static T[] GetComponentsInChildrenWithoutSelf<T>(this GameObject self) where T : Component
    {
        return self.GetComponentsInChildren<T>().Where(c => self != c.gameObject).ToArray();
    }

    public static string SecondToText(float second,bool IsDecimal)
    {
        int t_hour = (int)(second / HOUR_SECOND);
        int t_minute = (int)((second - t_hour*HOUR_SECOND) /MINUTE_SECOND);
        float t_second = (second % MINUTE_SECOND);

        string t_text = t_hour.ToString("D2") + ':' + t_minute.ToString("D2") + ':';
        if (IsDecimal)
        {
            t_text += ((int)t_second).ToString("D2");
        }
        else
        {
            t_text += ((int)t_second).ToString("D2");

        }

        return t_text;

    }
}
