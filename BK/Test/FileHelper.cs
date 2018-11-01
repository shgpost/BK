using System.IO;
using System.Reflection;

namespace Test
{
  public static class FileHelper
  {
    public static string GetPath(string fileName)
    {
      return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Path.Combine("img", fileName));
    }
  }
}
