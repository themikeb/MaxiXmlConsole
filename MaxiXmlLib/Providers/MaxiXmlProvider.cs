using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using MaxiXmlLib.Models;
using MaxXmlLib.Models;


namespace MaxXmlLib.Providers
{
  public class MaxiXmlProvider
  {
    /// <summary>
    /// Makes the web request to Login
    /// </summary>
    /// <param name="requestCookie">Contains a cookie container with validated login tokens</param>
    /// <param name="userName">User name for logging into Maxi</param>
    /// <param name="scode">Security Code - this is NOT your Maxi Password</param>
    /// <returns>true if logged in</returns>
    public static bool Login(CookieContainer requestCookie, string userName, int scode)
    {
      bool loggedIn = false;
      try
      {
        var loginUri = Constants.MaxiXmlUri + String.Format(Constants.LoginEndpoint, userName, scode);
        HttpWebRequest loginRequest = WebRequest.Create(loginUri) as HttpWebRequest;
        loginRequest.CookieContainer = requestCookie;

        using (var response = loginRequest.GetResponse() as HttpWebResponse)
        {
          string responseString;
          using (var reader = new StreamReader(response.GetResponseStream()))
          {
            responseString = reader.ReadToEnd();
            // Responses from the api have a top-level element of "maxi-xml".  
            // This makes deserializing to specialized objects difficult 
            // Replace maxi-xml with an element name that matches the object we are 
            //  going to deserialize to
            responseString = responseString.Replace("maxi-xml", "LoginResponse");

            XmlSerializer serializer = new XmlSerializer(typeof(LoginResponse));
            TextReader textReader = new StringReader(responseString);

            var loginResponse = (LoginResponse)serializer.Deserialize(textReader);
            if (!String.IsNullOrEmpty(loginResponse.error))
            {
              throw new Exception($"Error logging in. {loginResponse.error}");
            }
            loggedIn = true;
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }
      return loggedIn;
    }

    /// <summary>
    /// Make web request to Logout
    /// </summary>
    /// <param name="requestCookie">Contains a cookie container with validated login tokens</param>
    public static void Logout(CookieContainer requestCookie)
    {
      try
      {
        var logoutUri = Constants.MaxiXmlUri + Constants.LogoutEndpoint;
        HttpWebRequest logoutRequest = WebRequest.Create(logoutUri) as HttpWebRequest;
        logoutRequest.CookieContainer = requestCookie;

        using (var response = logoutRequest.GetResponse() as HttpWebResponse)
        {
          string responseString;
          using (var reader = new StreamReader(response.GetResponseStream()))
          {
            responseString = reader.ReadToEnd();

          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }

    /// <summary>
    /// Makes the request to get the logged in user's list of athletes
    /// </summary>
    /// <param name="requestCookie">Contains a cookie container with validated login tokens</param>
    /// <returns>List of athletes</returns>
    public static List<Athlete> GetAthletes(CookieContainer requestCookie)
    {
      List<Athlete> athletes = new List<Athlete>();

      try
      {
        var athletesUri = Constants.MaxiXmlUri + Constants.AthletesEndpoint;
        HttpWebRequest athletesRequest = WebRequest.Create(athletesUri) as HttpWebRequest;
        athletesRequest.CookieContainer = requestCookie;

        using (var response = athletesRequest.GetResponse() as HttpWebResponse)
        {
          string responseString;
          using (var reader = new StreamReader(response.GetResponseStream()))
          {
            responseString = reader.ReadToEnd();
            // Responses from the api have a top-level element of "maxi-xml".  
            // This makes deserializing to specialized objects difficult 
            // Replace maxi-xml with an element name that matches the object we are 
            //  going to deserialize to
            responseString = responseString.Replace("<maxi-xml>", "<AthletesResponse>");
            responseString = responseString.Replace("</maxi-xml>", "</AthletesResponse>");

            XmlSerializer serializer = new XmlSerializer(typeof(AthletesResponse));
            TextReader textReader = new StringReader(responseString);

            var athletesResponse = (AthletesResponse)serializer.Deserialize(textReader);
            if (!String.IsNullOrEmpty(athletesResponse.error))
            {
              throw new Exception($"Error logging in. {athletesResponse.error}");
            }
            athletes = athletesResponse.Athletes;
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }

      return athletes;
    }

    /// <summary>
    /// Get a competition response
    /// </summary>
    /// <param name="requestCookie"></param>
    /// <param name="compId">id of the competition - find in maxi</param>
    /// <returns>CompetitionResponse</returns>
    public static CompetitionResponse GetCompetitionResults(CookieContainer requestCookie, int compId)
    {
      bool rc = false;
      CompetitionResponse competitionResponse = null;

      try
      {
        var competitionUri = Constants.MaxiXmlUri + String.Format(Constants.CompetitionEndpoint, compId);
        HttpWebRequest competitionRequest = WebRequest.Create(competitionUri) as HttpWebRequest;
        competitionRequest.CookieContainer = requestCookie;

        using (var response = competitionRequest.GetResponse() as HttpWebResponse)
        {
          string responseString;
          using (var reader = new StreamReader(response.GetResponseStream()))
          {
            responseString = reader.ReadToEnd();
            // Responses from the api have a top-level element of "maxi-xml".  
            // This makes deserializing to specialized objects difficult 
            // Replace maxi-xml with an element name that matches the object we are 
            //  going to deserialize to
            responseString = responseString.Replace("<maxi-xml>", "<CompetitionResponse>");
            responseString = responseString.Replace("</maxi-xml>", "</CompetitionResponse>");

            XmlSerializer serializer = new XmlSerializer(typeof(CompetitionResponse));
            TextReader textReader = new StringReader(responseString);

            competitionResponse = (CompetitionResponse)serializer.Deserialize(textReader);
            if (!String.IsNullOrEmpty(competitionResponse.error))
            {
              throw new Exception($"Error logging in. {competitionResponse.error}");
            }
          }
        }

      }
      catch (Exception ex)
      {
        competitionResponse = null;
        Console.WriteLine($"Error: {ex.Message}");
      }
      return competitionResponse;
    }

    /// <summary>
    /// Returns event results for a given event
    /// </summary>
    /// <param name="requestCookie"></param>
    /// <param name="eventId"></param>
    /// <returns></returns>
    public static List<EventResponse> GetEventResults(CookieContainer requestCookie, int eventId)
    {
      bool rc = false;
      List<EventResponse> eventResults = new List<EventResponse>();

      try
      {
        var eventUri = Constants.MaxiXmlUri + String.Format(Constants.EventEndpoint, eventId);
        HttpWebRequest competitionRequest = WebRequest.Create(eventUri) as HttpWebRequest;
        competitionRequest.CookieContainer = requestCookie;

        using (var response = competitionRequest.GetResponse() as HttpWebResponse)
        {
          string responseString;
          using (var reader = new StreamReader(response.GetResponseStream()))
          {
            responseString = reader.ReadToEnd();
            // Responses from the api have a top-level element of "maxi-xml".  
            // This makes deserializing to specialized objects difficult 
            // Replace maxi-xml with an element name that matches the object we are 
            //  going to deserialize to
            responseString = responseString.Replace("<maxi-xml>", "<EventResponse>");
            responseString = responseString.Replace("</maxi-xml>", "</EventResponse>");

            XmlSerializer serializer = new XmlSerializer(typeof(EventResponse));
            TextReader textReader = new StringReader(responseString);

            var eventResponse = (EventResponse)serializer.Deserialize(textReader);
            if (!String.IsNullOrEmpty(eventResponse.error))
            {
              eventResults.Clear();

              throw new Exception($"Error logging in. {eventResponse.error}");
            }
            eventResults.Add(eventResponse);
          }
        }

      }
      catch (Exception ex)
      {
        eventResults.Clear();

        Console.WriteLine($"Error: {ex.Message}");
      }
      return eventResults;
    }

    /// <summary>
    /// Retrieve a specific team by team id
    /// </summary>
    /// <param name="requestCookie"></param>
    /// <param name="teamId"></param>
    /// <returns></returns>
    public static TeamResponse GetTeam(CookieContainer requestCookie, int teamId)
    {
      TeamResponse teamResponse = null;

      try
      {
        var teamUri = Constants.MaxiXmlUri + String.Format(Constants.TeamEndpoint, teamId);
        HttpWebRequest teamRequest = WebRequest.Create(teamUri) as HttpWebRequest;
        teamRequest.CookieContainer = requestCookie;

        using (var response = teamRequest.GetResponse() as HttpWebResponse)
        {
          string responseString;
          using (var reader = new StreamReader(response.GetResponseStream()))
          {
            responseString = reader.ReadToEnd();
            // Responses from the api have a top-level element of "maxi-xml".  
            // This makes deserializing to specialized objects difficult 
            // Replace maxi-xml with an element name that matches the object we are 
            //  going to deserialize to
            responseString = responseString.Replace("<maxi-xml>", "<TeamResponse>");
            responseString = responseString.Replace("</maxi-xml>", "</TeamResponse>");

            XmlSerializer serializer = new XmlSerializer(typeof(TeamResponse));
            TextReader textReader = new StringReader(responseString);

           teamResponse = (TeamResponse)serializer.Deserialize(textReader);
          }
        }

      }
      catch (Exception ex)
      {
        teamResponse = null;

        Console.WriteLine($"Error: {ex.Message}");
      }
      return teamResponse;
    }

    public static AthleteResponse GetAthlete(CookieContainer requestCookie, int athleteId)
    {
      AthleteResponse athleteResponse = null;

      try
      {
        var athleteUri = Constants.MaxiXmlUri + String.Format(Constants.AthleteEndpoint, athleteId);
        HttpWebRequest athleteRequest = WebRequest.Create(athleteUri) as HttpWebRequest;
        athleteRequest.CookieContainer = requestCookie;

        using (var response = athleteRequest.GetResponse() as HttpWebResponse)
        {
          string responseString;
          using (var reader = new StreamReader(response.GetResponseStream()))
          {
            responseString = reader.ReadToEnd();
            // Responses from the api have a top-level element of "maxi-xml".  
            // This makes deserializing to specialized objects difficult 
            // Replace maxi-xml with an element name that matches the object we are 
            //  going to deserialize to
            responseString = responseString.Replace("<maxi-xml>", "<AthleteResponse>");
            responseString = responseString.Replace("</maxi-xml>", "</AthleteResponse>");

            XmlSerializer serializer = new XmlSerializer(typeof(AthleteResponse));
            TextReader textReader = new StringReader(responseString);

            athleteResponse = (AthleteResponse)serializer.Deserialize(textReader);
          }
        }

      }
      catch (Exception ex)
      {
        athleteResponse = null;

        Console.WriteLine($"Error: {ex.Message}");
      }
      return athleteResponse;
    }

    /// <summary>
    /// Retrieves League information (note: there is a separate API for youth league)
    /// </summary>
    /// <param name="requestCookie"></param>
    /// <param name="leagueId"></param>
    /// <param name="season"></param>
    /// <returns></returns>
    public static LeagueResponse GetLeague(CookieContainer requestCookie, int leagueId, int season)
    {
      LeagueResponse leagueResponse = null;

      try
      {
        var leagueUri = Constants.MaxiXmlUri + String.Format(Constants.LeagueEndpoint, leagueId, season);
        HttpWebRequest leagueRequest = WebRequest.Create(leagueUri) as HttpWebRequest;
        leagueRequest.CookieContainer = requestCookie;

        using (var response = leagueRequest.GetResponse() as HttpWebResponse)
        {
          string responseString;
          using (var reader = new StreamReader(response.GetResponseStream()))
          {
            responseString = reader.ReadToEnd();
            // Responses from the api have a top-level element of "maxi-xml".  
            // This makes deserializing to specialized objects difficult 
            // Replace maxi-xml with an element name that matches the object we are 
            //  going to deserialize to
            responseString = responseString.Replace("<maxi-xml>", "<LeagueResponse>");
            responseString = responseString.Replace("</maxi-xml>", "</LeagueResponse>");
            
            XmlSerializer serializer = new XmlSerializer(typeof(LeagueResponse));
            TextReader textReader = new StringReader(responseString);

            leagueResponse = (LeagueResponse)serializer.Deserialize(textReader);
            
          }
        }

      }
      catch (Exception ex)
      {
        leagueResponse = null;

        Console.WriteLine($"Error: {ex.Message}");
      }
      return leagueResponse;
    }
    /// <summary>
    /// Retrieves YOUTH League information (note: there is a separate API for regular league)
    /// </summary>
    /// <param name="requestCookie"></param>
    /// <param name="leagueId"></param>
    /// <param name="season"></param>
    /// <returns></returns>
    public static LeagueResponse GetYouthLeague(CookieContainer requestCookie, int leagueId, int season)
    {
      LeagueResponse leagueResponse = null;

      try
      {
        var leagueUri = Constants.MaxiXmlUri + String.Format(Constants.YouthLeagueEndpoint, leagueId, season);
        HttpWebRequest leagueRequest = WebRequest.Create(leagueUri) as HttpWebRequest;
        leagueRequest.CookieContainer = requestCookie;

        using (var response = leagueRequest.GetResponse() as HttpWebResponse)
        {
          string responseString;
          using (var reader = new StreamReader(response.GetResponseStream()))
          {
            responseString = reader.ReadToEnd();
            // Responses from the api have a top-level element of "maxi-xml".  
            // This makes deserializing to specialized objects difficult 
            // Replace maxi-xml with an element name that matches the object we are 
            //  going to deserialize to
            responseString = responseString.Replace("<maxi-xml>", "<LeagueResponse>");
            responseString = responseString.Replace("</maxi-xml>", "</LeagueResponse>");

            XmlSerializer serializer = new XmlSerializer(typeof(LeagueResponse));
            TextReader textReader = new StringReader(responseString);

            leagueResponse = (LeagueResponse)serializer.Deserialize(textReader);

          }
        }

      }
      catch (Exception ex)
      {
        leagueResponse = null;

        Console.WriteLine($"Error: {ex.Message}");
      }
      return leagueResponse;
    }
  }
}
