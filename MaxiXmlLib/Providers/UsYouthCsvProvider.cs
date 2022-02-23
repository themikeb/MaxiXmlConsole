using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace MaxXmlLib.Models
{
  public class UsYouthCsvProvider
  {
    public static readonly string BaseFileName = "USYouthPerformance_{0}.csv";

    public static List<USYouthReportAthlete> GetSeasonRecords(int season)
    {
      List<USYouthReportAthlete> athletes = new List<USYouthReportAthlete>();

      try
      {
        var filename = String.Format(BaseFileName, season);
        using (var reader = new StreamReader(filename))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
          athletes = csv.GetRecords<USYouthReportAthlete>().ToList();
        }
      }
      catch(Exception ex)
      {
        
      }

      return athletes;
    }

    public static bool UpdateSeasonRecords(int season, List<USYouthReportAthlete> athletes)
    {
      bool rc = false;

      try
      {
        var fileName = String.Format(BaseFileName, season);

        using (var writer = new StreamWriter(fileName))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
          csv.WriteRecords(athletes);
        }
      }
      catch(Exception ex)
      {
        rc = false;
      }
      return rc;
    }
  }
}
