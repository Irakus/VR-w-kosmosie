using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


static class TimeConverter
{
    public static String ConvertTimeToString(float time)
    {
        float minutes = Mathf.Floor(time / 60.0f);
        float seconds = Mathf.Floor(time % 60.0f);
        float milis = Mathf.Floor((time - Mathf.Floor(time)) * 1000.0f);

        return minutes.ToString().PadLeft(2, '0') + ":"
                                                  + seconds.ToString().PadLeft(2, '0') + "."
                                                  + milis.ToString().PadLeft(3, '0');
    }
}

