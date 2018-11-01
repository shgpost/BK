using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
  class Program
  {
    static void Main(string[] args)
    {
      var v = new Vision.OpenCvMeasurement();
      byte[] img = System.IO.File.ReadAllBytes(@"C:\ROOT\KA\img\snor.png");
      var r = v.Measure(img);
      System.IO.File.WriteAllBytes(@"C:\ROOT\KA\img\snormodify.png", r.Result.ImageStream);
    }
  }
}
