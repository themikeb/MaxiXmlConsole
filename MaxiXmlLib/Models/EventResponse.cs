using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace MaxXmlLib.Models
{

  [XmlRoot(ElementName = "athlete")]
  public class EventAthlete
  {
    [XmlElement(ElementName = "id")]
    public string Id { get; set; }
    [XmlElement(ElementName = "teamId")]
    public string TeamId { get; set; }
    [XmlElement(ElementName = "performance")]
    public string Performance { get; set; }
    [XmlElement(ElementName = "placing")]
    public string Placing { get; set; }
    [XmlElement(ElementName = "score")]
    public string Score { get; set; }
    [XmlAttribute(AttributeName = "index")]
    public string Index { get; set; }
  }

  [XmlRoot(ElementName = "heat")]
  public class Heat
  {
    [XmlElement(ElementName = "athlete")]
    public List<EventAthlete> EventAthlete { get; set; }
    [XmlAttribute(AttributeName = "index")]
    public string Index { get; set; }
  }

  [XmlRoot(ElementName = "EventResponse")]
  public class EventResponse : MaxiXmlBaseResponse
  {
    [XmlElement(ElementName = "id")]
    public string Id { get; set; }
    [XmlElement(ElementName = "typeId")]
    public string TypeId { get; set; }
    [XmlElement(ElementName = "sex")]
    public string Sex { get; set; }
    [XmlElement(ElementName = "date")]
    public string Date { get; set; }
    [XmlElement(ElementName = "status")]
    public string Status { get; set; }
    [XmlElement(ElementName = "heat")]
    public List<Heat> Heat { get; set; }
  }


}
