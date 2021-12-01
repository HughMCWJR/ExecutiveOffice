using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newspaper : MonoBehaviour
{

    public Sprite unhighlighted;
    public Sprite highlighted;

    public GameObject newspaperOpen;

    SpriteRenderer spriteRenderer;
    PolygonCollider2D polyCollider;

    void Start()
    {

        spriteRenderer = this.GetComponent<SpriteRenderer>();
        polyCollider = this.GetComponent<PolygonCollider2D>();

    }

    void OnEnable()
    {

        newspaperOpen.SetActive(false);

        spriteRenderer.sprite = unhighlighted;

    }

    void Update()
    {

        if (Main.reading == null)
        {

            polyCollider.enabled = true;

        }
        else
        {

            polyCollider.enabled = false;

            spriteRenderer.sprite = unhighlighted;

        }

    }

    void OnMouseEnter()
    {

        spriteRenderer.sprite = highlighted;

    }

    void OnMouseDown()
    {

        Main.reading = newspaperOpen;

        newspaperOpen.SetActive(true);

    }

    void OnMouseExit()
    {

        spriteRenderer.sprite = unhighlighted;

    }
}
