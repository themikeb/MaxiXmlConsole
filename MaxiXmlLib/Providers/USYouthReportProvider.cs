using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MaxXmlLib.Models;

namespace MaxXmlLib.Providers
{
  public class USYouthReportProvider
  {
    public static bool USYouthPerformanceListReport(log4net.ILog logger, CookieContainer requestCookie, List<int> leagueIds, int season, bool onlyLast)
    {
      bool rc = false;

      try
      {
        List<int> competitionIds = new List<int>();
        if (logger != null) if (logger != null) logger.Info($"Retrieving competitions from {leagueIds.Count} Youth Leagues");

        foreach (var leagueId in leagueIds)
        {
          var leagueResponse = MaxiXmlProvider.GetYouthLeague(requestCookie, leagueId, season);
          var endedCompetitionIds = leagueResponse.Calendar.Competitions.Where(x => Int32.Parse(x.Status) == (int)CompetitionStatus.Ended).Select(x => Int32.Parse(x.Id)).ToList();

          if (onlyLast)
          {
            competitionIds.Add(endedCompetitionIds[endedCompetitionIds.Count() - 1]);
            competitionIds.Add(endedCompetitionIds[endedCompetitionIds.Count() - 2]);
          }
          else
          {
            competitionIds.AddRange(endedCompetitionIds);
          }
        }
        rc = USYouthPerformanceForCompetitions(logger, requestCookie, competitionIds, season);
      }
      catch(Exception ex)
      {
        if (logger != null) logger.Error($"Error processing performance list. {ex.Message}");

        rc = false;
      }
      return rc;
    }
    /// <summary>
    /// Performance List for all US Youth Leagues
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="requestCookie"></param>
    /// <param name="competitionIds"></param>
    /// <param name="season"></param>
    /// <returns></returns>
    public static bool USYouthPerformanceForCompetitions(log4net.ILog logger, CookieContainer requestCookie, List<int> competitionIds, int season)
    {
      bool rc = false;

      try
      {
        List<EventResponse> eventResponses = new List<EventResponse>();

        // Get competitions
        if (logger != null) logger.Info($"Retrieving results from {competitionIds.Count} competitions");

        foreach (var competitionId in competitionIds)
        {
          var competitionResults = MaxiXmlProvider.GetCompetitionResults(requestCookie, competitionId);
          if (competitionResults.Events != null)
          {
            foreach (var item in competitionResults.Events)
            {
              var eventResults = MaxiXmlProvider.GetEventResults(requestCookie, Int32.Parse(item.Id));

              eventResponses.AddRange(eventResults);
            }
          }
        }

        // Gather data for each event result
        //
        //  public int Index { get; set; }
        //public string Name { get; set; }
        //public int Age { get; set; }
        //public string Country { get; set; }
        //public string TeamName { get; set; }
        //public EventTypes EventType { get; set; }
        // public string Performance { get; set; }
        //public float SortPerformance { get; set; }
        var reportAthletes = new List<USYouthReportAthlete>();
        Dictionary<int, string> TeamNames = new Dictionary<int, string>();

        if (logger != null) logger.Info($"Gathering data for  {eventResponses.Count} event responses");

        foreach (var eventResult in eventResponses)
        {
          foreach (var heat in eventResult.Heat)
          {
            foreach(var heatAthlete in heat.EventAthlete)
            {
              Console.WriteLine($"Event:{eventResult.Id}  Athlete:{heatAthlete.Id}");

              var athleteInfo = MaxiXmlProvider.GetAthlete(requestCookie, Int32.Parse(heatAthlete.Id));
              var eventTypeId = (EventTypes)Int32.Parse(eventResult.TypeId);
              var nationId = (CountryTypes)Int32.Parse(athleteInfo.NationId);
              var teamId = Int32.Parse(heatAthlete.TeamId);

              if (!TeamNames.ContainsKey(teamId))
              {
                var teamResponse = MaxiXmlProvider.GetTeam(requestCookie, teamId);

                TeamNames.Add(teamId, teamResponse.TeamName);
              }


              // Only US Youth
              if ( (nationId == CountryTypes.UnitedStates) &&
                   (athleteInfo.Youth == "1"))
              {
                try
                {
                  var reportItem = new USYouthReportAthlete
                  {
                    EventDate = DateTime.Parse(eventResult.Date),
                    Index = Int32.Parse(heatAthlete.Id),
                    Name = athleteInfo.Surname + ", " + athleteInfo.Name,
                    Age = Int32.Parse(athleteInfo.Age),
                    Gender = (GenderTypes)Int32.Parse(eventResult.Sex),
                    NationId = (int)nationId,
                    Country = FixedData.CountryNames[nationId],
                    TeamId = teamId,
                    TeamName = TeamNames[teamId],
                    EventType = eventTypeId,
                    EventName = FixedData.EventNames[eventTypeId],
                    SortPerformance = (float)Double.Parse(heatAthlete.Performance),
                    Performance = FixedData.FormatEventResult(eventTypeId, Double.Parse(heatAthlete.Performance)),
                    Points = Int32.Parse(heatAthlete.Score)
                  };

                  reportAthletes.Add(reportItem);
                }
                catch(Exception ex)
                {
                  throw;
                }
              }
            }
          }
        } // end of eventResults list
        if (logger != null) logger.Info($"Found data for {reportAthletes.Count} athletes.");


        // Get existing CSV records
        if (logger != null) logger.Info($"Reading existing season records.");

        var existingRecords = UsYouthCsvProvider.GetSeasonRecords(season);

        existingRecords.AddRange(reportAthletes);

        // Dump to CSV
        if (logger != null) logger.Info($"Updating season records.  Total={existingRecords.Count}");

        UsYouthCsvProvider.UpdateSeasonRecords(season, existingRecords);

        // Reduce to best result for each athlete for an event
        if (logger != null) logger.Info($"Reducing results to best performance.");

        reportAthletes = ReduceToBestPerformance(existingRecords);

        // Split into a Dictionary of Lists by event type
        if (logger != null) logger.Info($"Spliting and sorting results by event.");

        Dictionary<EventTypes, List<USYouthReportAthlete>> resultsByEvent = new Dictionary<EventTypes, List<USYouthReportAthlete>>();

        foreach (var item in reportAthletes)
        {
          if (!resultsByEvent.ContainsKey(item.EventType))
          {
            resultsByEvent[item.EventType] = new List<USYouthReportAthlete>();
          }

          resultsByEvent[item.EventType].Add(item);
        }

        // Sort each list by SortPerformance
        Dictionary<EventTypes, List<USYouthReportAthlete>> sortedResultsByEvent = new Dictionary<EventTypes, List<USYouthReportAthlete>>();

        foreach (var key in resultsByEvent.Keys)
        {
        
          if (FixedData.EventSortDirection[key] == EventPerformanceSortType.Ascending)
          {
            sortedResultsByEvent[key] = resultsByEvent[key].OrderBy(x => x.SortPerformance).ToList();
          }
          else
          {
            sortedResultsByEvent[key] = resultsByEvent[key].OrderByDescending(x => x.SortPerformance).ToList();
          }
        }

        // Write the report file
        if (logger != null) logger.Info($"Writing report file.");

        WriteReportFile(sortedResultsByEvent, season);

        if (logger != null) logger.Info($"Report was successfully written.");

        rc = true;
      }
      catch(Exception ex)
      {
        if (logger != null) logger.Error($"Error processing performance list. {ex.Message}");

        rc = false;
      }

      return rc;
    }

    private static List<USYouthReportAthlete> ReduceToBestPerformance(List<USYouthReportAthlete> allRecords)
    {
      List<USYouthReportAthlete> reportAthletes = new List<USYouthReportAthlete>();
      Dictionary<string, USYouthReportAthlete> bestPerformances = new Dictionary<string, USYouthReportAthlete>();

      try
      {
        // Add the best performance for each athlete
        // Use a hash in the dictionary of the athleteId and EventType
        foreach (var record in allRecords)
        {
          var hash = $"{record.Index}_{record.EventType}";

          if (!bestPerformances.ContainsKey(hash))
          {
            bestPerformances[hash] = record;
          }
          else
          {
            if (FixedData.EventSortDirection[record.EventType] == EventPerformanceSortType.Ascending)
            {
              if (record.SortPerformance < bestPerformances[hash].SortPerformance) bestPerformances[hash] = record;
            }
            else
            {
              if (record.SortPerformance > bestPerformances[hash].SortPerformance) bestPerformances[hash] = record;
            }
          }
        }

        // Create a new list of report athletes from the best performances
        foreach (var key in bestPerformances.Keys)
        {
          var parts = key.Split('_');
          var id = Int32.Parse(parts[0]);
          
          reportAthletes.Add(bestPerformances[key]);
        }
      }
      catch(Exception ex)
      {

      }
      return reportAthletes;
    }

    private static void WriteReportFile(Dictionary<EventTypes, List<USYouthReportAthlete>> eventResults, int season)
    {
      var outputFilename = $"USYouthPerformanceList_Season_{season}.txt";

      var sprintEvents =   new List<EventTypes> { EventTypes.EventType_100M, EventTypes.EventType_200M, EventTypes.EventType_400M, EventTypes.EventType_110H, EventTypes.EventType_400H };
      var distanceEvents = new List<EventTypes> { EventTypes.EventType_800M, EventTypes.EventType_1500M, EventTypes.EventType_3000, EventTypes.EventType_5000, EventTypes.EventType_10000, EventTypes.EventType_MTH };
      var walkEvents = new List<EventTypes> { EventTypes.EventType_10RW, EventTypes.EventType_20RW, EventTypes.EventType_50RW };
      var fieldEvents = new List<EventTypes> { EventTypes.EventType_HJ, EventTypes.EventType_PV, EventTypes.EventType_LJ, EventTypes.EventType_TJ, EventTypes.EventType_SP, EventTypes.EventType_DSC, EventTypes.EventType_HMR, EventTypes.EventType_JV };
      var malePentathlon = new List<EventTypes> { EventTypes.EventType_Male_PEN, EventTypes.EventType_MPEN_LJ, EventTypes.EventType_MPEN_JV, EventTypes.EventType_MPEN_200M, EventTypes.EventType_MPEN_DSC, EventTypes.EventType_MPEN_1500 };
      var femalePentathlon = new List<EventTypes> { EventTypes.EventType_Female_PEN, EventTypes.EventType_FPEN_110H, EventTypes.EventType_FPEN_HJ, EventTypes.EventType_FPEN_SP, EventTypes.EventType_FPEN_LJ, EventTypes.EventType_FPEN_800 };
      var maleDecathlon = new List<EventTypes> { EventTypes.EventType_Male_DEC, EventTypes.EventType_MDEC_100M, EventTypes.EventType_MDEC_LJ, EventTypes.EventType_MDEC_SP, EventTypes.EventType_MDEC_HJ, EventTypes.EventType_MDEC_400M, EventTypes.EventType_MDEC_110H, EventTypes.EventType_MDEC_DSC, EventTypes.EventType_MDEC_PV, EventTypes.EventType_MDEC_JV, EventTypes.EventType_MDEC_1500};
      var femaleHeptathlon = new List<EventTypes> { EventTypes.EventType_Female_HEP, EventTypes.EventType_FHEP_100H, EventTypes.EventType_FHEP_HJ, EventTypes.EventType_FHEP_SP, EventTypes.EventType_FHEP_200M, EventTypes.EventType_FHEP_LJ, EventTypes.EventType_FHEP_JV, EventTypes.EventType_FHEP_800 };


      using (var writer = new StreamWriter(outputFilename))
      {
        // Males
        var gender = GenderTypes.Male;
        writer.WriteLine($"{FixedData.GenderNames[gender]} US Youth Performance List - Season {season}");
        writer.WriteLine("===================================");

        // Sprints
        foreach(var type in sprintEvents)
        {
          if (eventResults.ContainsKey(type))
          {
            WriteEventResults(type, gender, eventResults[type], writer);
          }
        }
        // Distance
        foreach (var type in distanceEvents)
        {
          if (eventResults.ContainsKey(type))
          {
            WriteEventResults(type, gender, eventResults[type], writer);
          }
        }
        // Walks
        foreach (var type in walkEvents)
        {
          if (eventResults.ContainsKey(type))
          {
            WriteEventResults(type, gender, eventResults[type], writer);
          }
        }
        // Fields
        foreach (var type in fieldEvents)
        {
          if (eventResults.ContainsKey(type))
          {
            WriteEventResults(type, gender, eventResults[type], writer);
          }
        }
        // Pentathlon
        foreach (var type in malePentathlon)
        {
          if (eventResults.ContainsKey(type))
          {
            WriteEventResults(type, gender, eventResults[type], writer);
          }
        }
        // Decathlon
        foreach (var type in maleDecathlon)
        {
          if (eventResults.ContainsKey(type))
          {
            WriteEventResults(type, gender, eventResults[type], writer);
          }
        }


        // Females
        gender = GenderTypes.Female;
        writer.WriteLine($"{FixedData.GenderNames[gender]} US Youth Performance List - Season {season}");
        writer.WriteLine("===================================");

        // Sprints
        foreach (var type in sprintEvents)
        {
          if (eventResults.ContainsKey(type))
          {
            WriteEventResults(type, gender, eventResults[type], writer);
          }
        }
        // Distance
        foreach (var type in distanceEvents)
        {
          if (eventResults.ContainsKey(type))
          {
            WriteEventResults(type, gender, eventResults[type], writer);
          }
        }
        // Walks
        foreach (var type in walkEvents)
        {
          if (eventResults.ContainsKey(type))
          {
            WriteEventResults(type, gender, eventResults[type], writer);
          }
        }
        // Fields
        foreach (var type in fieldEvents)
        {
          if (eventResults.ContainsKey(type))
          {
            WriteEventResults(type, gender, eventResults[type], writer);
          }
        }
        // Pentathlon
        foreach (var type in femalePentathlon)
        {
          if (eventResults.ContainsKey(type))
          {
            WriteEventResults(type, gender, eventResults[type], writer);
          }
        }
        // Heptathlon
        foreach (var type in femaleHeptathlon)
        {
          if (eventResults.ContainsKey(type))
          {
            WriteEventResults(type, gender, eventResults[type], writer);
          }
        }


      }
    }

    private static void WriteEventResults(EventTypes eventType, GenderTypes gender, List<USYouthReportAthlete> athletes, StreamWriter writer)
    {
      var eventName = FixedData.EventNames[eventType];

      writer.WriteLine($"{eventName}  {FixedData.GenderNames[gender]}");
      int place = 1;
      foreach (var athlete in athletes)
      {
        if (athlete.Gender == gender)
        {
          // Place, Name, Age, Performance, Club, Date
          var dataLine = String.Format("{0,4}{1,25}{2,4}{3,12}{4,5}{5,30},{6,12}",
            place, athlete.Name, athlete.Age, athlete.Performance, athlete.Points, athlete.TeamName, athlete.EventDate.ToString("yyyy-MM-dd"));

          writer.WriteLine(dataLine);

          place++;
        }
      }
      writer.WriteLine("");
    }
  }
}
