using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxXmlLib
{
  public class Constants
  {
    // Endpoints
    public static string MaxiXmlUri = "https://www.maxithlon.com/maxi-xml/";
    public static string LoginEndpoint = "login.php?user={0}&scode={1}";
    public static string LogoutEndpoint = "logout.php";
    public static string AthletesEndpoint = "athletes.php";
    public static string CompetitionEndpoint = "competition.php?competitionid={0}";
    public static string EventEndpoint = "event.php?eventid={0}";
    public static string TeamEndpoint = "team.php?teamid={0}";
    public static string AthleteEndpoint = "athlete.php?athleteid={0}";
    public static string LeagueEndpoint = "league.php?leagueid={0}&season={1}";
    public static string YouthLeagueEndpoint = "youthleague.php?leagueid={0}&season={1}";

  }
}
