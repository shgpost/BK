using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using Vision;

namespace Test
{
  [TestFixture]
  public class MeasurementTest
  {
    [Test]
    public async Task ShouldMeasureLengthOfRope()
    {
      var ropeLongPath = FileHelper.GetPath("snor.png");
      var ropeShortPath = FileHelper.GetPath("snorkort.png"); 

      var ropeShort= File.ReadAllBytes(ropeShortPath);
      var ropeLong = File.ReadAllBytes(ropeLongPath);
      var rShort = await new OpenCvMeasurement().Measure(ropeShort);
      var rLong = await new OpenCvMeasurement().Measure(ropeLong);
      Assert.That(rLong.Length, Is.GreaterThan(rShort.Length));
    }


   
  }
}
