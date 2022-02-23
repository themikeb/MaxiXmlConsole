using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using MaxXmlLib.Models;
using MaxXmlLib.Providers;
using NUnit.Framework;

namespace Tests
{
  [TestFixture]
  public class Tests
  {
    private static readonly log4net.ILog logger = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private string username;
    private int scode;

    [SetUp]
    public void Init()
    {
      username = ConfigurationManager.AppSettings["maxi.username"];
      scode = Int32.Parse(ConfigurationManager.AppSettings["maxi.password"]);
    }

    [Test]
    public void GetTeamAthletesTest()
    {
      CookieContainer requestCookie = new CookieContainer();

      // Login
      bool loginStatus = MaxiXmlProvider.Login(requestCookie, username, scode);
      Assert.IsTrue(loginStatus);

      // Get Athletes
      var athletes = MaxiXmlProvider.GetAthletes(requestCookie);
      Assert.IsTrue(athletes.Count > 0);

    }

    [TestCase(91624)]
    public void GetTeamTest(int teamId)
    {
      CookieContainer requestCookie = new CookieContainer();

      // Login
      bool loginStatus = MaxiXmlProvider.Login(requestCookie, username, scode);
      Assert.IsTrue(loginStatus);

      // Get Athletes
      var teamInfo = MaxiXmlProvider.GetTeam(requestCookie, teamId);
      Assert.IsNotNull(teamInfo);
      Assert.IsTrue(Int32.Parse(teamInfo.TeamId) == teamId);
    }

    [TestCase(4285976)] // full athlete details
    [TestCase(91624)] // partial athlete details
    public void GetAthleteTest(int athleteId)
    {
      CookieContainer requestCookie = new CookieContainer();

      // Login
      bool loginStatus = MaxiXmlProvider.Login(requestCookie, username, scode);
      Assert.IsTrue(loginStatus);

      // Get Athletes
      var athleteInfo = MaxiXmlProvider.GetAthlete(requestCookie, athleteId);
      Assert.IsNotNull(athleteInfo);
      //Assert.IsTrue(Int32.Parse(athleteInfo.Id) == athleteId);
    }

    [TestCase(176, 84)] // partial athlete details
    public void GetLeagueTest(int leagueId, int season)
    {
      CookieContainer requestCookie = new CookieContainer();

      // Login
      bool loginStatus = MaxiXmlProvider.Login(requestCookie, username, scode);
      Assert.IsTrue(loginStatus);

      // Get Athletes
      MaxiXmlProvider.GetLeague(requestCookie, leagueId, season);

    }

    [TestCase(84)] // partial athlete details
    public void TestYouthCompetitionReport(int season)
    {
      CookieContainer requestCookie = new CookieContainer();

      // Login
      bool loginStatus = MaxiXmlProvider.Login(requestCookie, username, scode);
      Assert.IsTrue(loginStatus);

      var leagueIds = new List<int> { 169, 170, 171, 172, 173, 174, 175, 211, 216, 219, 224};

      var isGood = USYouthReportProvider.USYouthPerformanceListReport(null, requestCookie, leagueIds, season, false);
      Assert.IsTrue(isGood);

    }

    [TestCase(84, 877071,877072)] // partial athlete details
    public void TestYouthCompetition(int season, int competitionId1, int competitionId2)
    {
      CookieContainer requestCookie = new CookieContainer();

      // Login
      bool loginStatus = MaxiXmlProvider.Login(requestCookie, username, scode);
      Assert.IsTrue(loginStatus);

      var ids = new List<int> { 877071, 877072, 876991, 876992, 877011, 877012,
        877031, 877032, 877051, 877052, 877091, 877092, 877111, 877112, 877131, 877132,
      877151, 877152, 877171, 877172};

      var isGood = USYouthReportProvider.USYouthPerformanceForCompetitions(null, requestCookie, ids, season );
      Assert.IsTrue(isGood);

    }

    [TestCase(EventTypes.EventType_200M, 21.08, "21.08")]
    [TestCase(EventTypes.EventType_10RW, 3653.25, "1h:00:53.25")]
    [TestCase(EventTypes.EventType_10RW, 3677.25, "1h:01:17.25")]
    [TestCase(EventTypes.EventType_1500M, 303.71, "5:03.71")]
    [TestCase(EventTypes.EventType_1500M, 303.01, "5:03.01")]
    [TestCase(EventTypes.EventType_1500M, 303.10, "5:03.10")]
    [TestCase(EventTypes.EventType_HJ, 0.88, "0.88m")]
    [TestCase(EventTypes.EventType_Female_PEN, 1788.00, "1788 pts")]
    public void TestFixeDataResultFormatting(EventTypes eventType, double performance, string expectedResult)
    {
      var formattedResult = FixedData.FormatEventResult(eventType, performance);
      Assert.IsTrue(formattedResult == expectedResult);
    }

    [TestCase(84)]
    public void TestCsvProviderRead(int season)
    {
      var records = UsYouthCsvProvider.GetSeasonRecords(season);
      Assert.IsTrue(records.Count > 0);
    }
  }
}