using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitsPlace : MonoBehaviour
{

    public int place;
    public string color;

    public float input;
    public float placeValue;


    Sprite[] numbers;

    SpriteRenderer spriteRenderer;

    void Start()
    {

        if (color == "black")
        {

            numbers = Resources.LoadAll<Sprite>("Numbers");

        } else
        {

            numbers = Resources.LoadAll<Sprite>("NumbersGray");

        }

        spriteRenderer = this.GetComponent<SpriteRenderer>();

    }

    void Update()
    {

        //1-4 is time, 5-12 is date
        switch (place)
        {

            case 0:

                spriteRenderer.sprite = numbers[Mathf.FloorToInt((input / placeValue) % 10)];

                break;
            case 1:

                if (Mathf.FloorToInt(Clock.time / 60) >= 10 || Mathf.FloorToInt(Clock.time / 60) == 0)
                {

                    spriteRenderer.sprite = numbers[1];

                } else
                {

                    spriteRenderer.sprite = numbers[0];

                }
                break;
            case 2:

                if (Mathf.FloorToInt(Clock.time / 60) == 0)
                {

                    spriteRenderer.sprite = numbers[2];

                } else
                {

                    spriteRenderer.sprite = numbers[Mathf.FloorToInt(Clock.time / 60) % 10];

                }

                break;
            case 3:

                spriteRenderer.sprite = numbers[Mathf.FloorToInt((Clock.time % 60) / 10)];

                break;
            case 4:

                spriteRenderer.sprite = numbers[(Clock.time % 60) % 10];

                break;

        }

    }
}
