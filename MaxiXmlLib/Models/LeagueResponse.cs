using System.Collections.Generic;
using System;
using System.Xml.Serialization;

namespace MaxiXmlLib.Models
{
  [XmlRoot(ElementName = "team")]
  public class LeagueTeam
  {
    [XmlAttribute(AttributeName = "id")]
    public string Id { get; set; }
    [XmlAttribute(AttributeName = "position")]
    public string Position { get; set; }
    [XmlAttribute(AttributeName = "points")]
    public string Points { get; set; }
    [XmlAttribute(AttributeName = "eventScore")]
    public string EventScore { get; set; }
  }

  [XmlRoot(ElementName = "standing")]
  public class Standing
  {
    [XmlElement(ElementName = "team")]
    public List<LeagueTeam> Teams { get; set; }
  }

  [XmlRoot(ElementName = "competition")]
  public class LeagueCompetition
  {
    [XmlAttribute(AttributeName = "id")]
    public string Id { get; set; }
    [XmlAttribute(AttributeName = "status")]
    public string Status { get; set; }
  }

  [XmlRoot(ElementName = "calendar")]
  public class Calendar
  {
    [XmlElement(ElementName = "competition")]
    public List<LeagueCompetition> Competitions { get; set; }
  }

  [XmlRoot(ElementName = "LeagueResponse")]
  public class LeagueResponse
  {
    [XmlElement(ElementName = "leagueId")]
    public string LeagueId { get; set; }
    [XmlElement(ElementName = "season")]
    public string Season { get; set; }
    [XmlElement(ElementName = "nationId")]
    public string NationId { get; set; }
    [XmlElement(ElementName = "level")]
    public string Level { get; set; }
    [XmlElement(ElementName = "number")]
    public string Number { get; set; }
    [XmlElement(ElementName = "standing")]
    public Standing Standing { get; set; }
    [XmlElement(ElementName = "calendar")]
    public Calendar Calendar { get; set; }
  }
}
