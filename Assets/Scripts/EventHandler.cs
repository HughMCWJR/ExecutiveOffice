using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    
    public Interaction secAgri, secTech;

    public GameObject newspaperOpen;

    public List<GameObject> itemPrefabs = new List<GameObject>();

    // Start is called before the first frame update
    public void Start()
    {

        //Declare what interactions do and the message paired with them
        secAgri = new Interaction("AGRI", "LETTER");
        secTech = new Interaction("TECH", "LETTER");

    }

    // Update is called once per frame
    void Update()
    {
        


    }
    
    //Orchestrate events for a given day/chapter
    public void EventsToday(int day, string chapter, out Dictionary<GameObject, List<string>> itemBlockSpriteNames, out Dictionary<GameObject, List<Interaction>> itemBlockInteractions, out List<GameObject> items, out Dictionary<GameObject, List<string>> articleTitlesByReading)
    {

        items = new List<GameObject>();

        itemBlockSpriteNames = new Dictionary<GameObject, List<string>>();
        itemBlockInteractions = new Dictionary<GameObject, List<Interaction>>();
        articleTitlesByReading = new Dictionary<GameObject, List<string>>();

        if (chapter == "main")
        {

            //Initialize newspaper
            itemBlockSpriteNames[newspaperOpen] = new List<string>();
            itemBlockInteractions[newspaperOpen] = new List<Interaction>();

            if (day == 1)
            {

                //First day

                //Newspaper
                AddItemBlock(newspaperOpen, "NewspaperTitle 0", null, ref itemBlockSpriteNames, ref itemBlockInteractions);
                AddItemBlock(newspaperOpen, "NewspaperFrontpage 0", null, "A NEW WORLD!", ref itemBlockSpriteNames, ref itemBlockInteractions, ref articleTitlesByReading);
                AddItemBlock(newspaperOpen, "NewspaperPhoto 0", null, ref itemBlockSpriteNames, ref itemBlockInteractions);

                //Introductory letter
                items.Add(Instantiate(itemPrefabs[1], new Vector2(0, 0), Quaternion.identity));

                itemBlockSpriteNames[items[0]] = new List<string>();
                itemBlockInteractions[items[0]] = new List<Interaction>();

                AddItemBlock(items[0], "Letter 0", null, "FIRST SECRETARY OF USR: First Day", ref itemBlockSpriteNames, ref itemBlockInteractions, ref articleTitlesByReading);

                //Example item
                items.Add(Instantiate(itemPrefabs[0], new Vector2(0, 0), Quaternion.identity));

                itemBlockSpriteNames[items[1]] = new List<string>();
                itemBlockInteractions[items[1]] = new List<Interaction>();
                
                AddItemBlock(items[1], "StatsTitle 0", null, ref itemBlockSpriteNames, ref itemBlockInteractions);
                AddItemBlock(items[1], "PieGraph " + 1.ToString(), null, ref itemBlockSpriteNames, ref itemBlockInteractions);
                AddItemBlock(items[1], "StatsGDP 0", secTech, ref itemBlockSpriteNames, ref itemBlockInteractions);
                AddItemBlock(items[1], "StatsVoters 0", secAgri, ref itemBlockSpriteNames, ref itemBlockInteractions);

            }

        }

    }

    //Add ItemBlock
    void AddItemBlock (GameObject gameObject, string spriteName, Interaction interaction, ref Dictionary<GameObject, List<string>> itemBlockSpriteNames, ref Dictionary<GameObject, List<Interaction>> itemBlockInteractions)
    {

        itemBlockSpriteNames[gameObject].Add(spriteName);
        itemBlockInteractions[gameObject].Add(interaction);

    }

    //Add ItemBlock with article
    void AddItemBlock(GameObject gameObject, string spriteName, Interaction interaction, string title, ref Dictionary<GameObject, List<string>> itemBlockSpriteNames, ref Dictionary<GameObject, List<Interaction>> itemBlockInteractions, ref Dictionary<GameObject, List<string>> articleTitleByReading)
    {

        itemBlockSpriteNames[gameObject].Add(spriteName);
        itemBlockInteractions[gameObject].Add(interaction);

        if (!articleTitleByReading.ContainsKey(gameObject))
        {

            articleTitleByReading.Add(gameObject, new List<string>());

        }

        while (articleTitleByReading[gameObject].Count != itemBlockSpriteNames[gameObject].Count - 1)
        {

            articleTitleByReading[gameObject].Add(null);

        }

        articleTitleByReading[gameObject].Add(title);

    }

}
