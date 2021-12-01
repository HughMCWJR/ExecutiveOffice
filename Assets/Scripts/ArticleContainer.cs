using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Xml;

[XmlRoot("ArticleCollection")]
public class ArticleContainer
{
    [XmlArray("Articles"), XmlArrayItem("Article")]
    public List<Article> Articles = new List<Article>();
}