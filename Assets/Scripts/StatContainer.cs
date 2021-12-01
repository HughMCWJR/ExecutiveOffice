using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatContainer
{

    public static Dictionary<string, Stat> stats = new Dictionary<string, Stat>();

    public static void CreateStat(string name, float value)
    {

        stats.Add(name, new Stat(value));

    }

    public static void CreateStat(string name, float value, float dailyPercentIncrease)
    {

        stats.Add(name, new Stat(value, dailyPercentIncrease));

    }

    public static void UpdateStats()
    {

        foreach(KeyValuePair<string, Stat> stat in stats)
        {

            stat.Value.NewDay();

        }

    }

}
