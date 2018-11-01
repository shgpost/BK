using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Test
{
  [TestFixture]
  public class VisionTest
  {
    [Test]
    public async Task ShouldExtractTextFromImageByUsingMsApi()
    {
      var path = GetPath("123.png");
      var bytes = GetImageAsByteArray(path);
      string result = await new Vision.MsVision().TextFromImageAsync(bytes);
      Assert.That(result, Is.EqualTo("12354"));
    }

    [Test]
    public async Task ShouldDetectObjectsInImageMsApi()
    {
      var path = GetPath("snor.png");
      var bytes = GetImageAsByteArray(path);
      string result = await new Vision.MsVision().MetaDataFromImageAsync(bytes);
      result = result.ToLower();
      Assert.That(result.Contains("knot") || result.Contains("rope"));
    }


    static string GetPath(string fileName)
    {
      return FileHelper.GetPath(fileName);
    }
    static byte[] GetImageAsByteArray(string imageFilePath)
    {
      return File.ReadAllBytes(imageFilePath);
    }
  }
}
