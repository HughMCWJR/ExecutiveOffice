using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suitcase : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    PolygonCollider2D polyCollider;

    public Sprite unhighlighted;
    public Sprite highlighted;

    public GameObject mainCamera;

    void Start()
    {

        spriteRenderer = this.GetComponent<SpriteRenderer>();
        polyCollider = this.GetComponent<PolygonCollider2D>();

        spriteRenderer.sprite = unhighlighted;

    }

    void Update()
    {

        if (Clock.time < Main.workDayEnd || Clock.am == true)
        {

            spriteRenderer.sprite = unhighlighted;

        }

        if (Main.reading == null)
        {

            polyCollider.enabled = true;

        } else
        {

            polyCollider.enabled = false;

            spriteRenderer.sprite = unhighlighted;

        }

    }

    void OnMouseOver()
    {

        if (Clock.time >= Main.workDayEnd && Clock.am == false)
        {

            spriteRenderer.sprite = highlighted;

        }

    }

    void OnMouseDown()
    {

        if (spriteRenderer.sprite == highlighted)
        {

            mainCamera.GetComponent<Main>().EndDay();

        }

    }

    void OnMouseExit()
    {

        spriteRenderer.sprite = unhighlighted;

    }
}
