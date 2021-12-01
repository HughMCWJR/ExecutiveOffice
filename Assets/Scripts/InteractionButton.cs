using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionButton : MonoBehaviour
{

    //Legacy \/
    /*
    public bool mouseOver;

    public Sprite unhighlighted;
    public Sprite highlighted;

    SpriteRenderer spriteRenderer;

    public Interaction thisInteraction;

    public GameObject text;
    public GameObject canvas;
    public GameObject thisText;
    public GameObject thisItemBlock;
    public GameObject blankPaper;

    void Start()
    {

        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

        unhighlighted = spriteRenderer.sprite;

        canvas = GameObject.Find("Canvas");

        thisText = Instantiate(text, new Vector2(this.gameObject.transform.position.x + 62, this.gameObject.transform.position.y + 9.5f), Quaternion.identity, canvas.transform);

        //Debug.Log(thisInteraction.buttonText);

        thisText.GetComponent<TMPro.TextMeshProUGUI>().text = thisInteraction.buttonText;

    }

    void Update()
    {

        spriteRenderer.sortingOrder = -Mathf.RoundToInt(this.gameObject.transform.position.y);

    }

    void OnMouseEnter()
    {

        mouseOver = true;

    }

    void OnMouseOver()
    {

        mouseOver = true;

        spriteRenderer.sprite = highlighted;

    }

    void OnMouseExit()
    {

        mouseOver = false;

        spriteRenderer.sprite = unhighlighted;

    }

    void OnMouseDown()
    {
        
        thisItemBlock.GetComponent<ItemBlock>().Check(thisInteraction);

        //switch (thisInteraction.type)
        //{

        //    case "BILL":

        //        break;

        //}
        
        //thisItemBlock.GetComponent<ItemBlock>().thisReading.GetComponent<Item>().GoToDesk(false);

        //GameObject temp = this.gameObject;

        //temp = Instantiate(blankPaper, new Vector2(0, -90), Quaternion.identity);

        //temp.GetComponent<BlankPaper>().type = thisInteraction.type;

    }

    public void Close()
    {

        Destroy(thisText);

        Destroy(this.gameObject);

    }

    */

}
