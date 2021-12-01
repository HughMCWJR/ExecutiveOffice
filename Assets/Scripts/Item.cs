using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    //If this item can be sent out
    public bool goOut;

    public List<Sprite> itemSprites = new List<Sprite>();
    public List<Sprite> openSprites = new List<Sprite>();
    public List<Sprite> deskSprites = new List<Sprite>();
    public List<Sprite> highlightSprites = new List<Sprite>();
    public List<Sprite> crumpleSprites = new List<Sprite>();

    //Used to stop sprite changing when highlighting and unhighlighting
    int deskSpriteNum;

    GameObject inbox;
    GameObject outbox;

    SpriteRenderer spriteRenderer;
    PolygonCollider2D polyCollider;

    public int page;

    string location;

    void Start()
    {

        inbox = GameObject.Find("LetterBoxIn");
        outbox = GameObject.Find("LetterBoxOut");

        spriteRenderer = this.GetComponent<SpriteRenderer>();
        polyCollider = this.GetComponent<PolygonCollider2D>();

        page = 0;

    }

    void Update()
    {

        if (Input.GetMouseButtonDown(1) && location == "open")
        {

            GoToDesk(false);

            Main.reading = null;

        }

    }

    public void GoToInbox()
    {

        //Added due to glitch with event system
        if (spriteRenderer == null)
        {

            inbox = GameObject.Find("LetterBoxIn");
            spriteRenderer = this.GetComponent<SpriteRenderer>();
            polyCollider = this.GetComponent<PolygonCollider2D>();

        }

        location = "inbox";

        spriteRenderer.sprite = itemSprites[Random.Range(0, itemSprites.Count)];
        spriteRenderer.sortingLayerName = "1";

        inbox.GetComponent<LetterBox>().boxItems.Add(this.gameObject);

        this.gameObject.transform.position = new Vector2(-40, 48);

        polyCollider.enabled = false;

    }

    public void GoToOutbox()
    {

        location = "outbox";

        spriteRenderer.sprite = itemSprites[Random.Range(0, itemSprites.Count)];
        spriteRenderer.sortingLayerName = "1";

        outbox.GetComponent<LetterBox>().boxItems.Add(this.gameObject);

        Main.desk = null;

        this.gameObject.transform.position = new Vector2(40, 48);

        polyCollider.enabled = false;

    }

    public void GoToDesk(bool fromInbox)
    {

        location = "desk";

        deskSpriteNum = Random.Range(0, deskSprites.Count);

        spriteRenderer.sprite = deskSprites[deskSpriteNum];
        spriteRenderer.sortingLayerName = "1";

        if (fromInbox)
        {

            inbox.GetComponent<LetterBox>().boxItems.Remove(this.gameObject);

        }

        Main.reading = null;
        Main.desk = this.gameObject;

        this.gameObject.transform.position = new Vector2(-5, -50);

        polyCollider.enabled = true;

    }

    public void GoToCrumple()
    {

        location = "crumple";

        spriteRenderer.sprite = crumpleSprites[Random.Range(0, crumpleSprites.Count)];

    }

    public void Open()
    {

        page = 0;

        location = "open";

        spriteRenderer.sprite = openSprites[Random.Range(0, openSprites.Count)];
        spriteRenderer.sortingLayerName = "2";

        this.gameObject.transform.position = new Vector2(0, -90);

        Main.reading = this.gameObject;
        Main.desk = null;

        polyCollider.enabled = false;

    }

    void OnMouseEnter()
    {

        if (location != "crumple")
        {

            spriteRenderer.sprite = highlightSprites[deskSpriteNum];

        }

    }

    void OnMouseExit()
    {

        if (location == "desk")
        {

            spriteRenderer.sprite = deskSprites[deskSpriteNum];

        }

    }

    void OnMouseOver()
    {

        if (Input.GetMouseButtonDown(1))
        {

            if (location == "desk")
            {

                GoToCrumple();

            }
            else if (location == "crumple")
            {

                Main.desk = null;

                Destroy(this.gameObject);

            }

        }

    }

    void OnMouseDown()
    {

        if (location == "crumple")
        {

            GoToDesk(false);
            spriteRenderer.sprite = highlightSprites[deskSpriteNum];

        }
        else
        {

            Open();

        }

    }

}
