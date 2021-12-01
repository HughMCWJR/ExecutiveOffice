using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class ItemBlock : MonoBehaviour
{

    public GameObject thisReading;
    public string thisReadingTag;

    public GameObject digit;
    List<GameObject> numbers = new List<GameObject>();

    public GameObject boldText;
    public GameObject smallText;
    List<GameObject> texts = new List<GameObject>();

    //public GameObject interactionButton;
    //List<GameObject> interactionButtons = new List<GameObject>();

    public bool left;
    public GameObject stickyNote;
    public GameObject stickyNoteText;
    GameObject thisStickyNote;
    GameObject thisStickyNoteText;
    int stickyNoteColor;

    //Page, Location on page starting at 0
    public int page;
    public int location;

    //Legacy \/
    /*
    //Signature
    public GameObject signaturePrefab;
    GameObject signature;

    //Check that shows if the ItemBlock has been interacted with
    public GameObject checkPrefab;
    GameObject check;
    int checkSpriteInt;
    */

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    GameObject mainCamera;
    GameObject canvas;

    Sprite[] interactions;
    Sprite[] interactionsHighlight;

    bool alreadyOpen;
    public bool mouseOver;
    bool interactionCheck;

    public Main mainScript;

    void Start()
    {

        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        boxCollider = this.gameObject.GetComponent<BoxCollider2D>();

        spriteRenderer.sprite = null;

        mainCamera = GameObject.Find("Main Camera");
        canvas = GameObject.Find("Canvas");

        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);

        interactions = Resources.LoadAll<Sprite>("Interactions");
        interactionsHighlight = Resources.LoadAll<Sprite>("InteractionsHighlight");

        alreadyOpen = false;

        mainScript = mainCamera.GetComponent<Main>();
        
        stickyNoteColor = Random.Range(0, 10);

    }

    void Update()
    {

        if (Main.reading != null)
        {

            if (Main.reading.tag == thisReadingTag)
            {

                thisReading = Main.reading;

                if (alreadyOpen == false)
                {

                    if (thisReading.tag == "NewspaperOpen")
                    {

                        if (thisReading.GetComponent<NewspaperOpen>().page == page)
                        {
                            Open();

                        }

                    }
                    else
                    {

                        if (thisReading.GetComponent<Item>().page == page)
                        {

                            Open();

                        }

                    }

                } else if (thisReading.tag == "NewspaperOpen")
                {

                    if (thisReading.GetComponent<NewspaperOpen>().page != page)
                    {

                        Close();

                    }

                }
                else if (thisReading.GetComponent<Item>().page != page)
                {
                    Close();

                }

            }

        } else
        {

            Close();

        }

        //Legacy \/
        /*
        interactionCheck = false;

        if (check == null)
        {

            if (mouseOver == false)
            {

                foreach (GameObject interaction in interactionButtons)
                {

                    if (interaction.GetComponent<InteractionButton>().mouseOver == true)
                    {

                        interactionCheck = true;

                    }

                }

            }
            else
            {

                interactionCheck = true;

            }

        }

        if (interactionCheck == false)
        {

            foreach (GameObject interactionButton in interactionButtons)
            {

                Destroy(interactionButton.GetComponent<InteractionButton>().thisText);

                Destroy(interactionButton);

            }

            interactionButtons = new List<GameObject>(); 

        }
        */

    }

    public void Open()
    {

        alreadyOpen = true;

        spriteRenderer.sprite = mainScript.itemBlockSpritesByString[mainScript.itemBlockSpriteNames[thisReading][location]];

        boxCollider.enabled = true;

        AddText();
        
        //Instantiate and set settings for Sticky Note if needed
        try
        {

            if (mainCamera.GetComponent<Main>().itemBlockInteractions[thisReading][location].buttonText != null)
            {

                if (left)
                {

                    thisStickyNote = Instantiate(stickyNote, new Vector2(this.gameObject.transform.position.x - 42, this.gameObject.transform.position.y + 46), Quaternion.identity, this.gameObject.transform);

                    thisStickyNoteText = Instantiate(stickyNoteText, new Vector2(this.gameObject.transform.position.x + 43, this.gameObject.transform.position.y + 51.7f), Quaternion.identity, canvas.transform);

                } else
                {

                    thisStickyNote = Instantiate(stickyNote, new Vector2(this.gameObject.transform.position.x + 42, this.gameObject.transform.position.y + 46), Quaternion.identity, this.gameObject.transform);

                    thisStickyNoteText = Instantiate(stickyNoteText, new Vector2(this.gameObject.transform.position.x - 42, this.gameObject.transform.position.y + 51.7f), Quaternion.identity, canvas.transform);
                    thisStickyNoteText.GetComponent<TMPro.TextMeshProUGUI>().alignment = TMPro.TextAlignmentOptions.Right;

                }

                thisStickyNote.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("StickyNote")[stickyNoteColor];

                thisStickyNoteText.GetComponent<TMPro.TextMeshProUGUI>().text = mainCamera.GetComponent<Main>().itemBlockInteractions[thisReading][location].buttonText;

            }

        }
        catch
        {
            


        }

        //Legacy \/ Replaced by StickyNote
        /*
        if (check != null)
        {

            check.SetActive(true);
            check.GetComponent<Animator>().Play("Check" + checkSpriteInt.ToString(), 0, 1);

        }
        */

    }

    void AddText()
    {

        List<GameObject> tempList = new List<GameObject>();

        //Do sprite sheets in if/elses and individual sprites in switch
        if (spriteRenderer.sprite.name.Contains("StatsPieGraph"))
        {



        }
        else if (spriteRenderer.sprite.name.Contains("Letter"))
        {

            //Every letter heading will be in the same aseprite, letter will have one itemblock, letter sender will be in title, letter recipient and text will be in text
            string title = mainCamera.GetComponent<Main>().articleTitlesByReading[thisReading][location];

            //Letter Text
            texts.Add(Instantiate(smallText, new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 95.5f), Quaternion.identity, canvas.transform));
            texts[texts.Count - 1].GetComponent<TMPro.TextMeshProUGUI>().text = mainCamera.GetComponent<Main>().articles[title].text;
            texts[texts.Count - 1].GetComponent<RectTransform>().sizeDelta = new Vector2(128, 120);

            //Letter Sender
            texts.Add(Instantiate(smallText, new Vector2(this.gameObject.transform.position.x - 35, this.gameObject.transform.position.y + 40.5f), Quaternion.identity, canvas.transform));
            Regex rx = new Regex("[A-Z ,]+:");
            string sender = rx.Match(title).Groups[0].Value;
            texts[texts.Count - 1].GetComponent<TMPro.TextMeshProUGUI>().text = sender.Substring(0, sender.Length - 1);
            texts[texts.Count - 1].GetComponent<TMPro.TextMeshProUGUI>().alignment = TMPro.TextAlignmentOptions.TopRight;

        }
        else
        {

            switch (spriteRenderer.sprite.name)
            {

                case "StatsVoters 0":

                    tempList = Main.CreateDigitPair(new Vector2(this.gameObject.transform.position.x - 4, this.gameObject.transform.position.y + 28), 0.1f, digit, this.gameObject.transform, StatContainer.stats["liberal"].value);
                    numbers.Add(tempList[0]);
                    numbers.Add(tempList[1]);

                    tempList = Main.CreateDigitPair(new Vector2(this.gameObject.transform.position.x - 4, this.gameObject.transform.position.y + 13), 0.1f, digit, this.gameObject.transform, StatContainer.stats["conservative"].value);
                    numbers.Add(tempList[0]);
                    numbers.Add(tempList[1]);

                    break;
                case "StatsGDP 0":

                    tempList = Main.CreateDigitPair(new Vector2(this.gameObject.transform.position.x - 16, this.gameObject.transform.position.y + 25), 10000, digit, this.gameObject.transform, StatContainer.stats["GDPmil"].value);
                    numbers.Add(tempList[0]);
                    numbers.Add(tempList[1]);

                    numbers.Add(Instantiate(digit, new Vector2(this.gameObject.transform.position.x - 6, this.gameObject.transform.position.y + 25), Quaternion.identity, this.gameObject.transform));
                    numbers[2].GetComponent<DigitsPlace>().placeValue = 100;
                    numbers[2].GetComponent<DigitsPlace>().input = StatContainer.stats["GDPmil"].value;
                    numbers[2].GetComponent<DigitsPlace>().place = 0;

                    break;
                case "NewspaperFrontpage 0":
                    
                    string title = mainCamera.GetComponent<Main>().articleTitlesByReading[thisReading][location];

                    //Article Title
                    texts.Add(Instantiate(boldText, new Vector2(this.gameObject.transform.position.x + 3, this.gameObject.transform.position.y + 80.5f), Quaternion.identity, canvas.transform));
                    texts[texts.Count - 1].GetComponent<TMPro.TextMeshProUGUI>().text = title;

                    //Article Text
                    texts.Add(Instantiate(smallText, new Vector2(this.gameObject.transform.position.x - 37.5f, this.gameObject.transform.position.y + 47.5f), Quaternion.identity, canvas.transform));
                    texts[texts.Count - 1].GetComponent<TMPro.TextMeshProUGUI>().text = mainCamera.GetComponent<Main>().articles[title].text;
                    texts[texts.Count - 1].GetComponent<RectTransform>().sizeDelta = new Vector2(59, 68);

                    break;

            }

        }

    }

    public void Close()
    {

        alreadyOpen = false;

        spriteRenderer.sprite = null;

        boxCollider.enabled = false;

        foreach(GameObject number in numbers)
        {

            Destroy(number);

        }

        numbers = new List<GameObject>();

        foreach (GameObject text in texts)
        {

            Destroy(text);

        }

        texts = new List<GameObject>();

        /*
        foreach (GameObject interactionButton in interactionButtons)
        {

            interactionButton.GetComponent<InteractionButton>().Close();

        }

        interactionButtons = new List<GameObject>();

        if (check != null)
        {

            check.SetActive(false);

        }
        */

    }

    //Legacy \/ Replaced with StickyNote system
    /*
    void OnMouseOver()
    {


        if (mainCamera.GetComponent<Main>().itemBlockInteractions[thisReading][location].Count > 0 && interactionButtons.Count == 0 && check == null)
        {

        //Added highlight, removed because it looked ugly and wasn't needed
        //this.gameObject.transform.GetChild(0).gameObject.SetActive(true);

            foreach (Interaction interactionIndex in mainCamera.GetComponent<Main>().itemBlockInteractions[thisReading][location])
            {

                GameObject temp = this.gameObject;

                temp = Instantiate(interactionButton, new Vector2(this.gameObject.transform.position.x + 67, (this.gameObject.transform.position.y + 42) - (8 * interactionButtons.Count)), Quaternion.identity, this.gameObject.transform);

                //temp.GetComponent<SpriteRenderer>().sprite = interactions[interactionIndex];
                temp.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Interaction");
                //temp.GetComponent<Interaction>().highlighted = interactionsHighlight[interactionIndex];
                temp.GetComponent<InteractionButton>().highlighted = Resources.Load<Sprite>("InteractionHighlight");
                temp.GetComponent<InteractionButton>().thisInteraction = interactionIndex;
                temp.GetComponent<InteractionButton>().thisItemBlock = this.gameObject;
                interactionButtons.Add(temp);

            }

        }

        mouseOver = true;

    }

    void OnMouseExit()
    {

        //this.gameObject.transform.GetChild(0).gameObject.SetActive(false);

        mouseOver = false;

    }
    */

    //Legacy \/
    //Add check GameObject to denote that this itemblock is interacted with, Also enact chosen interaction
    /*
    public void Check(Interaction interaction)
    {

        mainScript.clock.GetComponent<Clock>().AddTime(60);

        check = Instantiate(checkPrefab, new Vector2(0, 0), Quaternion.identity, this.gameObject.transform);
        check.transform.localPosition = new Vector2(0, 0);
        checkSpriteInt = Random.Range(1, 4);
        check.GetComponent<Animator>().Play("Check" + checkSpriteInt.ToString());

    }
    */

}
