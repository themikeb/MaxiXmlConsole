using System.Xml.Serialization;

namespace MaxXmlLib.Models
{
  [XmlRoot(ElementName = "TeamResponse")]
  public class TeamResponse : MaxiXmlBaseResponse
  {
    [XmlElement(ElementName = "teamId")]
    public string TeamId { get; set; }
    [XmlElement(ElementName = "teamName")]
    public string TeamName { get; set; }
    [XmlElement(ElementName = "owner")]
    public string Owner { get; set; }
    [XmlElement(ElementName = "founded")]
    public string Founded { get; set; }
    [XmlElement(ElementName = "youthFounded")]
    public string YouthFounded { get; set; }
    [XmlElement(ElementName = "arenaId")]
    public string ArenaId { get; set; }
    [XmlElement(ElementName = "arenaName")]
    public string ArenaName { get; set; }
    [XmlElement(ElementName = "leagueId")]
    public string LeagueId { get; set; }
    [XmlElement(ElementName = "youthLeagueId")]
    public string YouthLeagueId { get; set; }
    [XmlElement(ElementName = "nationId")]
    public string NationId { get; set; }
    [XmlElement(ElementName = "regionId")]
    public string RegionId { get; set; }
    [XmlElement(ElementName = "budget")]
    public string Budget { get; set; }
    [XmlElement(ElementName = "maxitrainer")]
    public string Maxitrainer { get; set; }
  }
}
