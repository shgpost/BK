using System;
using System.Linq;
using System.Threading.Tasks;
using OpenCvSharp;

namespace Vision
{
  public interface IMeasurement
  {
    Task<MeasurementResult> Measure(byte[] imageStream);
  }
  public class OpenCvMeasurement :IMeasurement
  {
    //https://books.google.dk/books?id=u3FGDwAAQBAJ&pg=PA278&lpg=PA278&dq=AGAST+open+cv&source=bl&ots=z7EK_vlvo_&sig=xuhCORmG0bFp7s2G-8V_QQzQxKY&hl=da&sa=X&ved=2ahUKEwj_iJCnv5DeAhWKZ1AKHSQRDMYQ6AEwCXoECAIQAQ#v=onepage&q=AGAST%20open%20cv&f=false

    public Task<MeasurementResult> Measure(byte[] imageStream)
    {
     
      try
      {
        Mat img = Cv2.ImDecode(imageStream, ImreadModes.GrayScale);
        Mat imgOrg = Cv2.ImDecode(imageStream, ImreadModes.Color);
        //detect edges 
        KeyPoint[] kp = Cv2.FAST(img, 8, true, FASTType.TYPE_5_8);

        //draw the keypoints
        Cv2.DrawKeypoints(img, kp, imgOrg);

        Func<float[], int[]> minMax = f =>
        {
          return new[] { (int)f.Min(a => a), (int)f.Max(b => b) };
        };

        //draw rect
        int[] xMinMax = minMax(kp.Select(a => a.Pt.X).ToArray());
        int[] yMinMax = minMax(kp.Select(b => b.Pt.Y).ToArray());

        Point p1 = new Point(xMinMax[0], yMinMax[0]);
        Point p2 = new Point(xMinMax[1], yMinMax[1]);
        Cv2.Rectangle(imgOrg, p1, p2, Scalar.Cyan, 1, 0);

        var length = xMinMax[1] - xMinMax[0];
        return Task.FromResult(new MeasurementResult
        {
          ImageStream = imgOrg.ToBytes(),
          Length = length
        });
      }
      catch (Exception)
      {
        return Task.FromResult(MeasurementResult.Empty());
      }
    }
  }

  public class MeasurementResult
  {
    public static MeasurementResult Empty()
    {
      return new MeasurementResult(){ImageStream = new byte[]{}, Length = 0};
    }
    public byte[] ImageStream { get; set; }
    public float Length { get; set; }
  }
}
