using System;

namespace MaxXmlLib.Models
{
  public class USYouthReportAthlete
  {
    public DateTime EventDate { get; set; }
    public int Index { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public GenderTypes Gender { get; set; }
    public int NationId { get; set; }
    public string Country { get; set; }
    public int TeamId { get; set; }
    public string TeamName { get; set; }
    public EventTypes EventType { get; set; }
    public string EventName { get; set; }
    public string Performance { get; set; }
    public float SortPerformance { get; set; }
    public int Points { get; set; }
  }
}
