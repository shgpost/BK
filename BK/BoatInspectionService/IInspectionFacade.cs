using Repository;
using System.Threading.Tasks;

namespace BoatInspectionService
{
  public interface IInspectionFacade
  {
    Task<ImageMetaData> ExtractMetadataFromImage(byte[] imgBytes);
    Task<string> ExtractTextFromImage(byte[] imgBytes);
    Task SaveInspectionResult(string boatId, ImageMetaData metadata);
  }
}