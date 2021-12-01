using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewspaperOpen : MonoBehaviour
{

    public GameObject newspaper;

    public int page;
    public int numPages;

    SpriteRenderer spriteRenderer;
    PolygonCollider2D polyCollider;

    //0 & 1 is first page (default, interacted), 2 & 3 & 4 is middle pages (default, interacted back, interacted forward), 5 & 6 is last page (default, interacted)
    public List<Sprite> sprites = new List<Sprite>();

    void Start()
    {

        page = 0;

        spriteRenderer = this.GetComponent<SpriteRenderer>();
        polyCollider = this.GetComponent<PolygonCollider2D>();

    }

    void OnEnable()
    {

        page = 0;

        newspaper.SetActive(false);

    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(1)) {

            Main.reading = null;

            newspaper.SetActive(true);

        }

    }

    void OnMouseOver()
    {

        if (page == 0)
        {

            if (Input.mousePosition.x > Screen.width / 2)
            {

                spriteRenderer.sprite = sprites[1];

            }

        } else if (page == numPages - 1)
        {

            if (Input.mousePosition.x < Screen.width / 2)
            {

                spriteRenderer.sprite = sprites[6];

            }

        } else
        {

            if (Input.mousePosition.x < Screen.width / 2)
            {

                spriteRenderer.sprite = sprites[3];

            }
            else
            {

                spriteRenderer.sprite = sprites[4];

            }

        }

    }

    void OnMouseExit()
    {

        if (page == 0)
        {

            if (Input.mousePosition.x > Screen.width / 2)
            {

                spriteRenderer.sprite = sprites[0];

            }

        }
        else if (page == numPages - 1)
        {

            if (Input.mousePosition.x < Screen.width / 2)
            {

                spriteRenderer.sprite = sprites[5];

            }

        }
        else
        {

            spriteRenderer.sprite = sprites[2];

        }

    }

    void OnMouseDown()
    {

        if (page == 0)
        {

            if (Input.mousePosition.x > Screen.width / 2)
            {

                page += 1;

            }

        }
        else if (page == numPages - 1)
        {

            if (Input.mousePosition.x < Screen.width / 2)
            {

                page -= 1;

            }

        }
        else
        {

            if (Input.mousePosition.x < Screen.width / 2)
            {

                page -= 1;

                if (page == 0)
                {

                    spriteRenderer.sprite = sprites[0];

                }

            }
            else
            {

                page += 1;

                if (page == numPages - 1)
                {

                    spriteRenderer.sprite = sprites[5];

                }

            }

        }

    }

}
