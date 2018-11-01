using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Repository;

namespace Test
{
  [TestFixture, Ignore("Does not work")]
  public class SqlDatabaseTest
  {
    [Test]
    public async Task ShouldSave()
    {
      var data = new ImageMetaData()
      {
        Date = DateTime.Now,
        Description = "SomeData",
        InspectorId = 123,
        Length = 111,
        ImageStream = new byte[] {1, 2, 3}
      };

      var target = new InspectionRepository();
      await target.AddOrUpdateInspection("123", data);
    }
  }
}
