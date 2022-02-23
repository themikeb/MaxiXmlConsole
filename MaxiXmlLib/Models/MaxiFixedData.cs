using System;
using System.Collections.Generic;


namespace MaxXmlLib.Models
{
  public enum EventTypes
  {
    EventType_100M = 1,
    EventType_200M = 4,
    EventType_400M = 6,
    EventType_800M = 8,
    EventType_1500M = 11,
    EventType_5000 = 14,
    EventType_10000 = 15,
    EventType_3000 = 19,
    EventType_110H = 23,
    EventType_400H = 25,
    EventType_HJ = 26,
    EventType_PV = 27,
    EventType_LJ = 28,
    EventType_TJ = 29,
    EventType_SP = 31,
    EventType_DSC = 32,
    EventType_HMR = 33,
    EventType_JV = 34,
    EventType_10RW = 46,
    EventType_20RW = 47,
    EventType_MTH = 53,
    EventType_50RW = 72,
    EventType_4X100 = 80,
    EventType_4X400 = 90,
    EventType_Male_PEN = 99,
    EventType_Female_PEN = 100,
    EventType_FPEN_110H = 101,
    EventType_FPEN_HJ = 103,
    EventType_FPEN_SP = 105,
    EventType_FPEN_LJ = 107,
    EventType_FPEN_800 = 109,
    EventType_MPEN_LJ = 102,
    EventType_MPEN_200M = 106,
    EventType_MPEN_DSC = 108,
    EventType_MPEN_JV = 104,
    EventType_MPEN_1500 = 110,
    EventType_Male_DEC = 130,
    EventType_Female_HEP = 120,
    EventType_FHEP_100H = 121,
    EventType_FHEP_HJ = 122,
    EventType_FHEP_SP = 123,
    EventType_FHEP_200M = 124,
    EventType_FHEP_LJ = 125,
    EventType_FHEP_JV = 126,
    EventType_FHEP_800 = 127,

    EventType_MDEC_100M = 131,
    EventType_MDEC_LJ = 132,
    EventType_MDEC_SP = 133,
    EventType_MDEC_HJ = 134,
    EventType_MDEC_400M = 135,
    EventType_MDEC_110H = 136,
    EventType_MDEC_DSC = 137,
    EventType_MDEC_PV = 138,
    EventType_MDEC_JV = 139,
    EventType_MDEC_1500 = 140

  }

  public enum GenderTypes
  {
    Male = 0,
    Female = 1
  }

  public enum CountryTypes
  {
    UnitedStates = 18
  }

  public enum EventPerformanceSortType
  {
    Ascending = 0,
    Descending = 1
  }

  public enum CompetitionStatus
  {
    NotStarted = 0,
    InProgress = 2,
    Ended = 3
  }

  public class FixedData
  {
    public static readonly Dictionary<CountryTypes, string> CountryNames = new Dictionary<CountryTypes, string>
    {
      {CountryTypes.UnitedStates, "USA" }
    };
    public static readonly Dictionary<GenderTypes, string> GenderNames = new Dictionary<GenderTypes, string>
    {
      {GenderTypes.Male, "Men" },
      {GenderTypes.Female, "Women" }

    };
    public static readonly Dictionary<EventTypes, string> EventNames = new Dictionary<EventTypes, string>
    {
      {EventTypes.EventType_100M, "100M" },
      {EventTypes.EventType_200M, "200M" },
      {EventTypes.EventType_400M, "400M" },
      {EventTypes.EventType_110H, "110H" },
      {EventTypes.EventType_400H, "400H" },
      {EventTypes.EventType_800M, "800M" },
      {EventTypes.EventType_1500M, "1500M" },
      {EventTypes.EventType_3000, "STPL" },
      {EventTypes.EventType_5000, "5K" },
      {EventTypes.EventType_10000, "10K" },
      {EventTypes.EventType_MTH, "Marathon" },
      {EventTypes.EventType_10RW, "10K Race Walk" },
      {EventTypes.EventType_20RW, "20K Race Walk" },
      {EventTypes.EventType_50RW, "50K Race Walk" },
      {EventTypes.EventType_HJ, "High Jump" },
      {EventTypes.EventType_LJ, "Long Jump" },
      {EventTypes.EventType_TJ, "Triple Jump" },
      {EventTypes.EventType_PV, "Pole Vault" },
      {EventTypes.EventType_SP, "Shot Put" },
      {EventTypes.EventType_DSC, "Discus" },
      {EventTypes.EventType_HMR, "Hammer Throw" },
      {EventTypes.EventType_JV, "Javelin" },
      {EventTypes.EventType_4X100, "4x100 Relay" },
      {EventTypes.EventType_4X400, "4x400 Realy" },

      {EventTypes.EventType_Male_PEN, "Pentahlon Men" },
      {EventTypes.EventType_Female_PEN, "Pentahlon Women" },
      {EventTypes.EventType_FPEN_110H, "Pentahlon Women 110H" },
      {EventTypes.EventType_FPEN_HJ, "Pentahlon Women HJ" },
      {EventTypes.EventType_FPEN_SP, "Pentahlon Women SP" },
      {EventTypes.EventType_FPEN_LJ, "Pentahlon Women LJ" },
      {EventTypes.EventType_FPEN_800, "Pentahlon Women 800M" },
      {EventTypes.EventType_MPEN_LJ, "Pentahlon Men LJ" },
      {EventTypes.EventType_MPEN_200M, "Pentahlon Men 200M" },
      {EventTypes.EventType_MPEN_DSC, "Pentahlon Men DSC" },
      {EventTypes.EventType_MPEN_JV, "Pentahlon Men JV" },
      {EventTypes.EventType_MPEN_1500, "Pentahlon Men 1500M" },
      {EventTypes.EventType_Male_DEC, "Decathlon" },
      {EventTypes.EventType_Female_HEP, "Heptathlon" },
      {EventTypes.EventType_FHEP_100H, "Heptathlon 100H" },
      {EventTypes.EventType_FHEP_HJ, "Heptathlon HJ" },
      {EventTypes.EventType_FHEP_SP, "Heptathlon SP" },
      {EventTypes.EventType_FHEP_200M, "Heptathlon 200M" },
      {EventTypes.EventType_FHEP_LJ, "Heptathlon LJ" },
      {EventTypes.EventType_FHEP_JV, "Heptathlon JV" },
      {EventTypes.EventType_FHEP_800, "Heptathlon 800M" },

      {EventTypes.EventType_MDEC_100M, "Decathlon" },
      {EventTypes.EventType_MDEC_LJ, "Decathlon LJ" },
      {EventTypes.EventType_MDEC_SP, "Decathlon SP" },
      {EventTypes.EventType_MDEC_HJ, "Decathlon HJ" },
      {EventTypes.EventType_MDEC_400M, "Decathlon 400M" },
      {EventTypes.EventType_MDEC_110H, "Decathlon 110H" },
      {EventTypes.EventType_MDEC_DSC, "Decathlon DSC" },
      {EventTypes.EventType_MDEC_PV, "Decathlon PV" },
      {EventTypes.EventType_MDEC_JV, "Decathlon JV" },
      {EventTypes.EventType_MDEC_1500, "Decathlon 1500M" }
    };

    public static readonly Dictionary<EventTypes, EventPerformanceSortType> EventSortDirection = new Dictionary<EventTypes, EventPerformanceSortType>
    {
      {EventTypes.EventType_100M, EventPerformanceSortType.Ascending },
      {EventTypes.EventType_200M, EventPerformanceSortType.Ascending },
      {EventTypes.EventType_400M, EventPerformanceSortType.Ascending },
      {EventTypes.EventType_110H, EventPerformanceSortType.Ascending },
      {EventTypes.EventType_400H, EventPerformanceSortType.Ascending },
      {EventTypes.EventType_800M, EventPerformanceSortType.Ascending },
      {EventTypes.EventType_1500M, EventPerformanceSortType.Ascending },
      {EventTypes.EventType_3000, EventPerformanceSortType.Ascending },
      {EventTypes.EventType_5000, EventPerformanceSortType.Ascending },
      {EventTypes.EventType_10000, EventPerformanceSortType.Ascending },
      {EventTypes.EventType_MTH, EventPerformanceSortType.Ascending },
      {EventTypes.EventType_10RW, EventPerformanceSortType.Ascending },
      {EventTypes.EventType_20RW, EventPerformanceSortType.Ascending },
      {EventTypes.EventType_50RW, EventPerformanceSortType.Ascending },
      {EventTypes.EventType_HJ, EventPerformanceSortType.Descending },
      {EventTypes.EventType_LJ, EventPerformanceSortType.Descending },
      {EventTypes.EventType_TJ, EventPerformanceSortType.Descending },
      {EventTypes.EventType_PV, EventPerformanceSortType.Descending },
      {EventTypes.EventType_SP, EventPerformanceSortType.Descending },
      {EventTypes.EventType_DSC, EventPerformanceSortType.Descending },
      {EventTypes.EventType_HMR, EventPerformanceSortType.Descending },
      {EventTypes.EventType_JV, EventPerformanceSortType.Descending },
      {EventTypes.EventType_4X100, EventPerformanceSortType.Ascending },
      {EventTypes.EventType_4X400, EventPerformanceSortType.Ascending },

      {EventTypes.EventType_Male_PEN, EventPerformanceSortType.Descending },
      {EventTypes.EventType_Female_PEN, EventPerformanceSortType.Descending },
      {EventTypes.EventType_FPEN_110H, EventPerformanceSortType.Ascending },
      {EventTypes.EventType_FPEN_HJ, EventPerformanceSortType.Descending },
      {EventTypes.EventType_FPEN_SP, EventPerformanceSortType.Descending },
      {EventTypes.EventType_FPEN_LJ, EventPerformanceSortType.Descending },
      {EventTypes.EventType_FPEN_800, EventPerformanceSortType.Ascending },
      {EventTypes.EventType_MPEN_LJ, EventPerformanceSortType.Descending },
      {EventTypes.EventType_MPEN_200M, EventPerformanceSortType.Ascending },
      {EventTypes.EventType_MPEN_DSC, EventPerformanceSortType.Descending },
      {EventTypes.EventType_MPEN_JV, EventPerformanceSortType.Descending },
      {EventTypes.EventType_MPEN_1500, EventPerformanceSortType.Ascending },
      {EventTypes.EventType_Male_DEC, EventPerformanceSortType.Descending },
      {EventTypes.EventType_Female_HEP, EventPerformanceSortType.Descending },
      {EventTypes.EventType_FHEP_100H, EventPerformanceSortType.Ascending },
      {EventTypes.EventType_FHEP_HJ, EventPerformanceSortType.Descending },
      {EventTypes.EventType_FHEP_SP, EventPerformanceSortType.Descending },
      {EventTypes.EventType_FHEP_200M, EventPerformanceSortType.Ascending },
      {EventTypes.EventType_FHEP_LJ, EventPerformanceSortType.Descending },
      {EventTypes.EventType_FHEP_JV, EventPerformanceSortType.Descending },
      {EventTypes.EventType_FHEP_800, EventPerformanceSortType.Ascending },

      {EventTypes.EventType_MDEC_100M, EventPerformanceSortType.Descending },
      {EventTypes.EventType_MDEC_LJ, EventPerformanceSortType.Descending },
      {EventTypes.EventType_MDEC_SP, EventPerformanceSortType.Descending },
      {EventTypes.EventType_MDEC_HJ, EventPerformanceSortType.Descending },
      {EventTypes.EventType_MDEC_400M, EventPerformanceSortType.Ascending },
      {EventTypes.EventType_MDEC_110H, EventPerformanceSortType.Ascending },
      {EventTypes.EventType_MDEC_DSC, EventPerformanceSortType.Descending },
      {EventTypes.EventType_MDEC_PV, EventPerformanceSortType.Descending },
      {EventTypes.EventType_MDEC_JV, EventPerformanceSortType.Descending },
      {EventTypes.EventType_MDEC_1500,EventPerformanceSortType.Ascending }
    };

    public static string FormatEventResult(EventTypes eventType, double performance)
    {
      string formattedResult = String.Empty;

      switch(eventType)
      {
        case EventTypes.EventType_100M:
        case EventTypes.EventType_200M:
        case EventTypes.EventType_400M:
        case EventTypes.EventType_800M:
        case EventTypes.EventType_1500M:
        case EventTypes.EventType_5000:
        case EventTypes.EventType_10000:
        case EventTypes.EventType_3000:
        case EventTypes.EventType_110H:
        case EventTypes.EventType_400H:
        case EventTypes.EventType_10RW:
        case EventTypes.EventType_20RW:
        case EventTypes.EventType_MTH:
        case EventTypes.EventType_50RW:
        case EventTypes.EventType_4X100:
        case EventTypes.EventType_4X400:
        case EventTypes.EventType_FPEN_110H:
        case EventTypes.EventType_FPEN_800:
        case EventTypes.EventType_MPEN_200M:
        case EventTypes.EventType_MPEN_1500:
        case EventTypes.EventType_FHEP_100H:
        case EventTypes.EventType_FHEP_200M:
        case EventTypes.EventType_FHEP_800:
        case EventTypes.EventType_MDEC_100M:
        case EventTypes.EventType_MDEC_400M:
        case EventTypes.EventType_MDEC_110H:
        case EventTypes.EventType_MDEC_1500:
          {
            var secondsFmt = "00.00";
            var minutesFmt = "00";
            var remainder = performance;
            var hours = (int)(performance / 3600);
            remainder -= (hours * 3600);
            var minutes = (int)(remainder / 60);
            remainder -= (minutes * 60);
            var seconds = remainder;

            if (hours > 0) formattedResult += $"{hours}h:";
            if (hours > 0 || minutes > 9) formattedResult += minutes.ToString(minutesFmt) + ":";
            if (hours == 0 && minutes < 10 && minutes > 0) formattedResult += minutes.ToString() + ":";
            formattedResult += seconds.ToString(secondsFmt);
          }
          break;

        case EventTypes.EventType_HJ:
        case EventTypes.EventType_PV:
        case EventTypes.EventType_LJ:
        case EventTypes.EventType_TJ:
        case EventTypes.EventType_SP:
        case EventTypes.EventType_DSC:
        case EventTypes.EventType_HMR:
        case EventTypes.EventType_JV:
        case EventTypes.EventType_FPEN_HJ:
        case EventTypes.EventType_FPEN_SP:
        case EventTypes.EventType_FPEN_LJ:
        case EventTypes.EventType_MPEN_LJ:
        case EventTypes.EventType_MPEN_DSC:
        case EventTypes.EventType_MPEN_JV:
        case EventTypes.EventType_FHEP_HJ:
        case EventTypes.EventType_FHEP_SP:
        case EventTypes.EventType_FHEP_LJ:
        case EventTypes.EventType_FHEP_JV:
        case EventTypes.EventType_MDEC_LJ:
        case EventTypes.EventType_MDEC_SP:
        case EventTypes.EventType_MDEC_HJ:
        case EventTypes.EventType_MDEC_DSC:
        case EventTypes.EventType_MDEC_PV:
        case EventTypes.EventType_MDEC_JV:
          formattedResult = $"{performance}m";
          break;

        case EventTypes.EventType_Male_DEC:
        case EventTypes.EventType_Female_HEP:
        case EventTypes.EventType_Male_PEN:
        case EventTypes.EventType_Female_PEN:
          formattedResult = $"{(int)performance} pts";
          break;
      }

      return formattedResult;
    }
  }
}
