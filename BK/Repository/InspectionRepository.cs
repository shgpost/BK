using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Repository
{
  public class InspectionRepository:IInspectionRepository
  {
    private const string TableMetadata = "Meta";
    private const string TableInspection = "Inspection";
    public Task AddOrUpdateInspection(string boatId, ImageMetaData medatada)
    {
      //https://www.codeproject.com/Articles/823854/How-to-connect-SQL-Database-to-your-Csharp-program
      using (SqlConnection conn = new SqlConnection())
      {
        conn.ConnectionString = "Data Source=DK-SHG-HP850G4;Initial Catalog=BK;Integrated Security=True";
        conn.Open();
        var cmd = string.Format("INSERT INTO {0} (BoatId, Length, Description, Date, ImageStream) VALUES (@boatId, @length, @description, @date,imageStream)",TableMetadata);
        SqlCommand insertCommand = new SqlCommand(cmd, conn);

        insertCommand.Parameters.Add(new SqlParameter("boatId", boatId));
        insertCommand.Parameters.Add(new SqlParameter("length", medatada.Length));
        insertCommand.Parameters.Add(new SqlParameter("description", medatada.Description));
        insertCommand.Parameters.Add(new SqlParameter("date", System.DateTime.Now));
        insertCommand.Parameters.Add(new SqlParameter("imageStream", medatada.ImageStream));

        insertCommand.ExecuteNonQuery();
      }
      return Task.FromResult(0);
    }

    public Inspection GetInspectionsByBoatId(string boatId)
    {
      throw new NotImplementedException();
    }
  }
}