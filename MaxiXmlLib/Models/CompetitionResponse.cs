using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace MaxXmlLib.Models
{

  [XmlRoot(ElementName = "event")]
  public class CompetitionEvent
  {
    [XmlAttribute(AttributeName = "id")]
    public string Id { get; set; }
  }

  [XmlRoot(ElementName = "CompetitionResponse")]
  public class CompetitionResponse : MaxiXmlBaseResponse
  {
    [XmlElement(ElementName = "name")]
    public string Name { get; set; }
    [XmlElement(ElementName = "type")]
    public string Type { get; set; }
    [XmlElement(ElementName = "season")]
    public string Season { get; set; }
    [XmlElement(ElementName = "date")]
    public string Date { get; set; }
    [XmlElement(ElementName = "arenaId")]
    public string ArenaId { get; set; }
    [XmlElement(ElementName = "nationId")]
    public string NationId { get; set; }
    [XmlElement(ElementName = "regionId")]
    public string RegionId { get; set; }
    [XmlElement(ElementName = "standings")]
    public string Standings { get; set; }
    [XmlElement(ElementName = "attendance")]
    public string Attendance { get; set; }
    [XmlElement(ElementName = "meteo")]
    public string Meteo { get; set; }
    [XmlElement(ElementName = "status")]
    public string Status { get; set; }
    [XmlElement(ElementName = "event")]
    public List<CompetitionEvent> Events { get; set; }
  }



}
