using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{

    //Value of stat
    public float value;

    //Daily percent increase of stat
    public float dailyPercentIncrease;

    //Past 10 days of this stat
    public float[] pastValues;

    public Stat (float v)
    {

        value = v;
        dailyPercentIncrease = 1;
        pastValues = new float[10];

    }

    public Stat(float v, float d)
    {

        value = v;
        dailyPercentIncrease = d;
        pastValues = new float[10];

    }

    public void NewDay()
    {

        //Update past values
        float[] tempPastValues = new float[10];

        tempPastValues[0] = value;

        for (int i = 1; i < tempPastValues.Length; i++)
        {

            tempPastValues[i] = pastValues[i - 1];

        }

        pastValues = tempPastValues;

        //Change value by daily change
        value = value * dailyPercentIncrease;

    }

}
