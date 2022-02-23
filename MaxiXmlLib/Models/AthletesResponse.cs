using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MaxXmlLib.Models
{
  [XmlRoot(ElementName = "sponsor")]
  public class Sponsor
  {
    [XmlAttribute(AttributeName = "id")]
    public string Id { get; set; }
    [XmlAttribute(AttributeName = "money")]
    public string Money { get; set; }
    [XmlAttribute(AttributeName = "tvMoney")]
    public string TvMoney { get; set; }
    [XmlAttribute(AttributeName = "weeks")]
    public string Weeks { get; set; }
  }

  [XmlRoot(ElementName = "sell")]
  public class Sell
  {
    [XmlAttribute(AttributeName = "deadline")]
    public string Deadline { get; set; }
    [XmlAttribute(AttributeName = "bid")]
    public string Bid { get; set; }
    [XmlAttribute(AttributeName = "bidder")]
    public string Bidder { get; set; }
  }

  [XmlRoot(ElementName = "trainingProgram")]
  public class TrainingProgram
  {
    [XmlAttribute(AttributeName = "coachId")]
    public string CoachId { get; set; }
    [XmlAttribute(AttributeName = "form")]
    public string Form { get; set; }
    [XmlAttribute(AttributeName = "strenght")]
    public string Strenght { get; set; }
    [XmlAttribute(AttributeName = "stamina")]
    public string Stamina { get; set; }
    [XmlAttribute(AttributeName = "speed")]
    public string Speed { get; set; }
    [XmlAttribute(AttributeName = "agility")]
    public string Agility { get; set; }
    [XmlAttribute(AttributeName = "jump")]
    public string Jump { get; set; }
    [XmlAttribute(AttributeName = "throw")]
    public string Throw { get; set; }
    [XmlAttribute(AttributeName = "specialty1")]
    public string Specialty1 { get; set; }
    [XmlAttribute(AttributeName = "specialty2")]
    public string Specialty2 { get; set; }
    [XmlAttribute(AttributeName = "total")]
    public string Total { get; set; }
  }
  [XmlRoot(ElementName = "specialtyId")]
  public class SpecialtyId
  {
    [XmlAttribute(AttributeName = "change")]
    public string Change { get; set; }
    [XmlText]
    public string Text { get; set; }
  }

  [XmlRoot(ElementName = "athlete")]
  public class Athlete : MaxiXmlBaseResponse
  {
    [XmlElement(ElementName = "athleteId")]
    public string AthleteId { get; set; }
    [XmlElement(ElementName = "name")]
    public string Name { get; set; }
    [XmlElement(ElementName = "surname")]
    public string Surname { get; set; }
    [XmlElement(ElementName = "sex")]
    public string Sex { get; set; }
    [XmlElement(ElementName = "age")]
    public string Age { get; set; }
    [XmlElement(ElementName = "nationId")]
    public string NationId { get; set; }
    [XmlElement(ElementName = "owner")]
    public string Owner { get; set; }
    [XmlElement(ElementName = "wage")]
    public string Wage { get; set; }
    [XmlElement(ElementName = "height")]
    public string Height { get; set; }
    [XmlElement(ElementName = "weight")]
    public string Weight { get; set; }
    [XmlElement(ElementName = "fans")]
    public string Fans { get; set; }
    [XmlElement(ElementName = "sponsor")]
    public Sponsor Sponsor { get; set; }
    [XmlElement(ElementName = "maxid")]
    public string Maxid { get; set; }
    [XmlElement(ElementName = "form")]
    public string Form { get; set; }
    [XmlElement(ElementName = "care")]
    public string Care { get; set; }
    [XmlElement(ElementName = "experience")]
    public string Experience { get; set; }
    [XmlElement(ElementName = "mood")]
    public string Mood { get; set; }
    [XmlElement(ElementName = "favoriteEventId")]
    public string FavoriteEventId { get; set; }
    [XmlElement(ElementName = "strenght")]
    public string Strenght { get; set; }
    [XmlElement(ElementName = "stamina")]
    public string Stamina { get; set; }
    [XmlElement(ElementName = "speed")]
    public string Speed { get; set; }
    [XmlElement(ElementName = "agility")]
    public string Agility { get; set; }
    [XmlElement(ElementName = "jump")]
    public string Jump { get; set; }
    [XmlElement(ElementName = "throw")]
    public string Throw { get; set; }
    [XmlElement(ElementName = "specialty1")]
    public string Specialty1 { get; set; }
    [XmlElement(ElementName = "specialty2")]
    public string Specialty2 { get; set; }
    [XmlElement(ElementName = "coachId")]
    public string CoachId { get; set; }
    [XmlElement(ElementName = "injury")]
    public string Injury { get; set; }
    [XmlElement(ElementName = "sell")]
    public Sell Sell { get; set; }
    [XmlElement(ElementName = "youth")]
    public string Youth { get; set; }
    [XmlElement(ElementName = "specialtyIdTrained")]
    public string SpecialtyIdTrained { get; set; }
    [XmlElement(ElementName = "diet")]
    public string Diet { get; set; }
    [XmlElement(ElementName = "trainingRetreat")]
    public string TrainingRetreat { get; set; }
    [XmlElement(ElementName = "numberEvents")]
    public string NumberEvents { get; set; }
    [XmlElement(ElementName = "lossFans")]
    public string LossFans { get; set; }
    [XmlElement(ElementName = "trainingProgram")]
    public TrainingProgram TrainingProgram { get; set; }
    [XmlAttribute(AttributeName = "index")]
    public string Index { get; set; }
    [XmlElement(ElementName = "specialtyId")]
    public SpecialtyId SpecialtyId { get; set; }
  }
   
  [XmlRoot(ElementName = "AthletesResponse")]
  public class AthletesResponse : MaxiXmlBaseResponse
  {
    [XmlElement(ElementName = "athlete")]
    public List<Athlete> Athletes { get; set; }
  }
}
