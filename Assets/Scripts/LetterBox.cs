using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBox : MonoBehaviour
{

    //Items in this box
    public List<GameObject> boxItems = new List<GameObject>();

    public bool receiving;

    public GameObject highlight;

    BoxCollider2D boxCollider;

    //When increses tells box to update rendering order
    int lastBoxItemCount;

    void Start()
    {

        boxCollider = this.gameObject.GetComponent<BoxCollider2D>();

        lastBoxItemCount = 0;

    }

    void Update()
    {

        if (Main.reading == null)
        {

            boxCollider.enabled = true;

        }
        else
        {

            boxCollider.enabled = false;

            highlight.SetActive(false);

        }

        if (boxItems.Count > lastBoxItemCount)
        {

            for (int i = 0; i < boxItems.Count; i++)
            {

                if (receiving)
                {

                    boxItems[i].GetComponent<SpriteRenderer>().sortingOrder = boxItems.Count - 1 - i;

                } else
                {

                    boxItems[i].GetComponent<SpriteRenderer>().sortingOrder = i;
                    
                }

            }

        }

        lastBoxItemCount = boxItems.Count;

    }

    void OnMouseOver()
    {

        if (((boxItems.Count > 0 && receiving && Main.desk == null) || (receiving != true && Main.desk != null)) && Main.reading == null)
        {

            if (receiving)
            {

                highlight.SetActive(true);

            } else if (Main.desk.GetComponent<Item>().goOut) {

                highlight.SetActive(true);

            }

        }


    }

    void OnMouseDown()
    {

        if (highlight.activeSelf && Main.reading == null)
        {

            if (receiving)
            {

                boxItems[0].GetComponent<Item>().GoToDesk(true);

            } else
            {

                Main.desk.GetComponent<Item>().GoToOutbox();

            }

            highlight.SetActive(false);

        }

    }

    void OnMouseExit()
    {

        highlight.SetActive(false);

    }

}
