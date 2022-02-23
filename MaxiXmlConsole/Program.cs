using System;
using System.Net;
using System.Collections.Generic;
using MaxXmlLib.Models;
using MaxXmlLib.Providers;
using System.Linq;
using System.Configuration;

namespace MaxiXmlConsole
{
  class Program
  {
    private static readonly log4net.ILog logger = log4net.LogManager.GetLogger
                (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    /// <summary>
    /// Command line:  [-athletes | -youthperformance] <season> <week> <comma-sep list of competition ids> 
    /// -athletes 84 2
    /// -youthperformance 84 2 "877073,877074,876993,876994,877013,877014,877033,877034,877053,877054,877093,877094,877113,877114,877133,877134,877153,877154,877173,877174,877193"
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
      try
      {
        logger.Info("MaxiXmlConsole is starting...");

        if (args.Length < 3)
        {
          throw new Exception($"Error: invalid command line arguments");
        }

        if ( (args[0] == "-youthperformance") &&
             (args.Length != 4) )
        {
          throw new Exception($"Error: invalid command line arguments");
        }

        var userName = ConfigurationManager.AppSettings["maxi.username"];
        var password = ConfigurationManager.AppSettings["maxi.password"];

        CookieContainer requestCookie = new CookieContainer();
        // Login
        if (!MaxiXmlProvider.Login(requestCookie, userName, Int32.Parse(password)))
        {
          throw new Exception("Login failed.");
        }

        // Get Athletes
        if (args[0] == "-athletes")
        {
          logger.Info("Processing athlete report");

          var athletes = MaxiXmlProvider.GetAthletes(requestCookie);
        }
        else if (args[0] == "-youthperformance")
        {
          int season = Int32.Parse(args[1]);
          var idStrs = args[3].Split(',');
          List<int> competitionIds = idStrs.Select(x => Int32.Parse(x)).ToList();

          logger.Info("Processing US Youth Performance List");

         // TODO: var success = USYouthReportProvider.USYouthPerformanceListReport(logger, requestCookie, competitionIds, season);
          //if (!success)
          {
            throw new Exception("Failed to complete US Youth Performance List.");
          }
        }   
        
        
        // Logout - returns a 404, not working
        //Logout(requestCookie);

      }
      catch(Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
        logger.Info($"Error: {ex.Message}");
      }

      return;
    }



  }
}
