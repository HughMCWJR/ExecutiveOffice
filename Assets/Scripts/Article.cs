using System.Xml;
using System.Xml.Serialization;

public class Article
{
    [XmlAttribute("title")]
    public string title;

    public string text;
}

