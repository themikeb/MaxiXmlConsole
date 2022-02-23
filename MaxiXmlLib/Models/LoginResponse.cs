using System;


namespace MaxXmlLib.Models
{
  [Serializable]
  public class LoginResponse : MaxiXmlBaseResponse
  {
    [System.Xml.Serialization.XmlElementAttribute("login")]
    public string login { get; set; }
  }
}
