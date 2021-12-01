using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

public class Main : MonoBehaviour
{

    //Measurements for where in the game the player is
    public int day;
    public string chapter;
    EventHandler eventHandler;
    public Dictionary<string, Article> articles = new Dictionary<string, Article>();
    public Dictionary<GameObject, List<string>> articleTitlesByReading = new Dictionary<GameObject, List<string>>();

    //Items (objects that go in the letterbox)
    public List<GameObject> items = new List<GameObject>();
    public List<GameObject> itemPrefabs = new List<GameObject>();
    public List<Sprite> allItemBlockSprites = new List<Sprite>();
    public Dictionary<string, Sprite> itemBlockSpritesByString = new Dictionary<string, Sprite>();
    public Dictionary<GameObject, List<string>> itemBlockSpriteNames = new Dictionary<GameObject, List<string>>();
    public Dictionary<GameObject, List<Interaction>> itemBlockInteractions = new Dictionary<GameObject, List<Interaction>>();

    public static GameObject reading;
    public static GameObject desk;

    //Legacy \/ replaced by Stat class
    //List format: (value, daily percent change, daily arithmetic change in percent change)
    //public static int area;
    //public static Dictionary<string, List<float>> information = new Dictionary<string, List<float>>();
    //public static float GDPperCapita;
    //public static float budgetPercentGDP;
    //public static float budget;

    //public static Dictionary<string, List<float>> pastStats = new Dictionary<string, List<float>>();

    //Workday in terms of minutes
    public static int workDayStart;
    public static int workDayEnd;

    public static float sleepMod;

    public GameObject clock;
    public GameObject newspaperOpen;

    void Start()
    {

        //Initialize event system
        day = 0;
        chapter = "main";
        eventHandler = new EventHandler();
        eventHandler.Start();
        eventHandler.newspaperOpen = newspaperOpen;
        eventHandler.itemPrefabs = itemPrefabs;

        //Load articles
        var textAsset = Resources.Load<TextAsset>("DataFiles/articles");
        var serializer = new XmlSerializer(typeof(ArticleContainer));
        //var stream = new FileStream(Path.Combine(Application.dataPath, "Resources/DataFiles/articles.xml"), FileMode.Open);
        var stream = new System.IO.StringReader(textAsset.text);
        var tempArticleList = serializer.Deserialize(stream) as ArticleContainer;
        stream.Close();

        foreach (Article article in tempArticleList.Articles)
        {

            articles.Add(article.title, article);

        }

        foreach (Sprite sprite in allItemBlockSprites)
        {

            itemBlockSpritesByString[sprite.name] = sprite;

        }

        reading = null;
        desk = null;

        //Instantiate Stat objects
        StatContainer.CreateStat("area", 30000);

        StatContainer.CreateStat("pop", 4000000);

        StatContainer.CreateStat("GDPmil", 9000);
        StatContainer.CreateStat("budgetPercentGDP", 0.03f);

        StatContainer.CreateStat("liberal", 0.45f);
        StatContainer.CreateStat("conservative", 0.55f);

        //Legacy \/ replaced by Stat class
        //area = 30000;

        //AddInformation("pop", 4000000);

        //AddInformation("GDPmil", 9000);
        //GDPperCapita = information["GDPmil"][0] / information["pop"][0];

        // Temp \/
        //budgetPercentGDP = 0.03f;
        //budget = budgetPercentGDP * information["GDPmil"][0];

        //AddInformation("liberal", 0.45f);
        //AddInformation("conservative", 0.55f);

        //pastStats["GDPs"] = new List<float>();
        //pastStats["pops"] = new List<float>();

        workDayStart = 540;
        workDayEnd = 300;

        sleepMod = 1;

    }

    void Update()
    {

        if (Input.GetKey("escape"))
        {

            Application.Quit();

        }

    }

    public void EndDay()
    {

        //itemBlockSprites.Clear();

        //pastStats["GDPs"].Add(information["GDPmil"][0]);
        //pastStats["pops"].Add(information["pop"][0]);

        //foreach (List<float> pastStat in pastStats.Values)
        //{

        //    if (pastStat.Count > 10)
        //    {

        //        pastStat.Remove(pastStat[10]);

        //    }

        //}

        StartDay();

    }

    public void StartDay()
    {

        day += 1;

        //if (Clock.time >= 480 && Clock.am == false && reading == null)
        {

            clock.GetComponent<Clock>().ChangeTime(workDayStart, true, 1);

        }
        
        //Initialize itemBlocks for that day for every item
        eventHandler.EventsToday(day, chapter, out itemBlockSpriteNames, out itemBlockInteractions, out items, out articleTitlesByReading);
        
        //Legacy \/ replaced by event class
        /*Newspaper
        itemBlockSpriteNames[newspaperOpen] = new List<string>();
        itemBlockInteractions[newspaperOpen] = new List<List<Interaction>>();

        Other items
        itemBlockSpriteNames[items[0]] = new List<string>();
        itemBlockInteractions[items[0]] = new List<List<Interaction>>();

        itemBlockSpriteNames[items[0]].Add("StatsTitle 0");
        itemBlockInteractions[items[0]].Add(new List<Interaction>());
        itemBlockSpriteNames[items[0]].Add("PieGraph " + 1.ToString());
        itemBlockInteractions[items[0]].Add(new List<Interaction>());
        itemBlockSpriteNames[items[0]].Add("StatsGDP 0");
        itemBlockInteractions[items[0]].Add(new List<Interaction>(){raiseTaxes, lowerTaxes});*/
        
        foreach (GameObject item in items)
        {
            
            item.GetComponent<Item>().GoToInbox();

        }

    }

    public static List<GameObject> CreateDigitPair(Vector2 firstPos, float firstPlaceValue, GameObject digit, Transform parent = null, float input = 0)
    {

        List<GameObject> tempList = new List<GameObject>();

        tempList.Add(Instantiate(digit, firstPos, Quaternion.identity, parent));
        tempList.Add(Instantiate(digit, new Vector2 (firstPos.x + 4, firstPos.y), Quaternion.identity, parent));

        tempList[0].GetComponent<DigitsPlace>().placeValue = firstPlaceValue;
        tempList[1].GetComponent<DigitsPlace>().placeValue = firstPlaceValue / 10;

        tempList[0].GetComponent<DigitsPlace>().input = input;
        tempList[1].GetComponent<DigitsPlace>().input = input;

        tempList[0].GetComponent<DigitsPlace>().place = 0;
        tempList[1].GetComponent<DigitsPlace>().place = 0;

        return tempList;

    }

    //Legacy \/ replaced by Stat class
    //void AddInformation(string key, float value)
    //{

    //    information[key] = new List<float>();
    //    information[key].Add(value);
    //    information[key].Add(value);

    //}

}
