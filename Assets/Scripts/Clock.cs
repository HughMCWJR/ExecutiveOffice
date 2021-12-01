using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{

    public static int time;
    public int timeTestingDisplay;

    public static int month;
    public static int day;
    public static int year;

    public GameObject minuteHand;
    public GameObject hourHand;

    public GameObject tooltip;
    public GameObject digit;

    List<GameObject> digits = new List<GameObject>();

    public Sprite unhighlighted;
    public Sprite highlighted;

    public Sprite clockTooltipAM;
    public Sprite clockTooltipPM;

    SpriteRenderer spriteRenderer;
    PolygonCollider2D polyCollider;

    public int daysPerMonth;
    public int monthsPerYear;

    public static bool am;

    bool changedAM;

    void Start()
    {

        spriteRenderer = this.GetComponent<SpriteRenderer>();
        polyCollider = this.GetComponent<PolygonCollider2D>();

        //Get and assign children, remember to change the 3 if adding children
        for (int i = 0; i < 3; i ++)
        {

            switch (this.transform.GetChild(i).gameObject.name)
            {

                case "MinuteHand 0":
                    minuteHand = this.transform.GetChild(i).gameObject;
                    break;
                case "HourHand 0":
                    hourHand = this.transform.GetChild(i).gameObject;
                    break;
                case "ClockTooltip 0":
                    tooltip = this.transform.GetChild(i).gameObject;
                    break;

            }

        }

        hourHand.GetComponent<Animator>().Play("Hours", 0,  2f / 3f);
        minuteHand.GetComponent<Animator>().Play("Minutes", 0, 0);

        am = false;

        changedAM = true;

        month = 1;
        day = 0;
        year = 1;

        daysPerMonth = 30;
        monthsPerYear = 12;

        for (int i = 1; i < 5; i++)
        {

            GameObject temp = this.gameObject;

            switch (i)
            {

                case 1:
                    temp = Instantiate(digit, new Vector2(tooltip.transform.position.x - 10, tooltip.transform.position.y + 16), Quaternion.identity, tooltip.transform);
                    break;
                case 2:
                    temp = Instantiate(digit, new Vector2(tooltip.transform.position.x - 6, tooltip.transform.position.y + 16), Quaternion.identity, tooltip.transform);
                    break;
                case 3:
                    temp = Instantiate(digit, new Vector2(tooltip.transform.position.x + 0, tooltip.transform.position.y + 16), Quaternion.identity, tooltip.transform);
                    break;
                case 4:
                    temp = Instantiate(digit, new Vector2(tooltip.transform.position.x + 4, tooltip.transform.position.y + 16), Quaternion.identity, tooltip.transform);
                    break;

            }

            temp.GetComponent<DigitsPlace>().place = i;

        }

        List<GameObject> tempList = new List<GameObject>();

        tempList = Main.CreateDigitPair(new Vector2(tooltip.transform.position.x - 17, tooltip.transform.position.y + 10), 10, digit, tooltip.transform);
        digits.Add(tempList[0]);
        digits.Add(tempList[1]);

        tempList = Main.CreateDigitPair(new Vector2(tooltip.transform.position.x - 5, tooltip.transform.position.y + 10), 10, digit, tooltip.transform);
        digits.Add(tempList[0]);
        digits.Add(tempList[1]);

        tempList = Main.CreateDigitPair(new Vector2(tooltip.transform.position.x + 7, tooltip.transform.position.y + 10), 1000, digit, tooltip.transform);
        digits.Add(tempList[0]);
        digits.Add(tempList[1]);

        tempList = Main.CreateDigitPair(new Vector2(tooltip.transform.position.x + 15, tooltip.transform.position.y + 10), 10, digit, tooltip.transform);
        digits.Add(tempList[0]);
        digits.Add(tempList[1]);

    }

    void Update()
    {

        time = Mathf.FloorToInt((hourHand.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime % 1) * 720f);
        // * 60 + Mathf.RoundToInt((minuteHand.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime % 1) * 60f)

        timeTestingDisplay = time;

        if (changedAM == false && time < 60)
        {

            if (am == true)
            {

                am = false;

            } else
            {

                am = true;

                day += 1;

                if (day > daysPerMonth)
                {

                    day = 1;
                    month += 1;

                }

                if (month > monthsPerYear)
                {

                    month = 1;
                    year += 1;

                }

            }

            changedAM = true;

        } else if (changedAM == true && time > 60)
        {

            changedAM = false;

        }

        if (Main.reading == null)
        {

            polyCollider.enabled = true;

        }
        else
        {

            polyCollider.enabled = false;

            spriteRenderer.sprite = unhighlighted;

            tooltip.SetActive(false);

        }

        digits[0].GetComponent<DigitsPlace>().input = month;
        digits[1].GetComponent<DigitsPlace>().input = month;
        digits[2].GetComponent<DigitsPlace>().input = day;
        digits[3].GetComponent<DigitsPlace>().input = day;
        digits[4].GetComponent<DigitsPlace>().input = year;
        digits[5].GetComponent<DigitsPlace>().input = year;
        digits[6].GetComponent<DigitsPlace>().input = year;
        digits[7].GetComponent<DigitsPlace>().input = year;

    }

    void OnMouseOver()
    {

        spriteRenderer.sprite = highlighted;

        tooltip.SetActive(true);

        if (am == true)
        {

            tooltip.GetComponent<SpriteRenderer>().sprite = clockTooltipAM;

        }
        else
        {

            tooltip.GetComponent<SpriteRenderer>().sprite = clockTooltipPM;

        }

    }

    void OnMouseExit()
    {

        spriteRenderer.sprite = unhighlighted;

        tooltip.SetActive(false);

    }

    //TEMP?
    void OnMouseDown()
    {

        AddTime(60);

    }

    //Sets time and am, increases day
    public void ChangeTime(int timeChange, bool amChange, int dayChange = 0)
    {

        time = timeChange;
        am = amChange;
        day += dayChange;

        if (day > daysPerMonth)
        {

            day = 1;
            month += 1;

        }

        if (month > monthsPerYear)
        {

            month = 1;
            year += 1;

        }

        hourHand.GetComponent<Animator>().Play("Hours", 0, (float)time / 720f);
        minuteHand.GetComponent<Animator>().Play("Minutes", 0, time % 60);

    }

    //Increase time by given number of minutes, then uses ChangeTime() to update clock
    public void AddTime(int minutesAdded)
    {

        time += minutesAdded;

        if (time > 720)
        {

            time -= 720;

            if (am)
            {

                am = false;

                ChangeTime(time, am, 0);

            } else
            {

                am = true;

                ChangeTime(time, am, 1);

            }

        } else
        {

            ChangeTime(time, am, 0);

        }

    }

}
