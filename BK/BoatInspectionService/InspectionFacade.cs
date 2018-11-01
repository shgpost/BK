using System.Threading.Tasks;
using Repository;
using Vision;

namespace BoatInspectionService
{
  public class InspectionFacade : IInspectionFacade
  {
    private readonly IMeasurement measurementService;
    private readonly IInspectionRepository inspectionRepository;
    private readonly IVision visionService;

    public InspectionFacade(IVision visionService, IMeasurement measurementService, IInspectionRepository inspectionRepository)
    {
      this.measurementService = measurementService;
      this.inspectionRepository = inspectionRepository;
      this.visionService = visionService;
    }
    public Task<string> ExtractTextFromImage(byte[] imgBytes)
    {
      return visionService.TextFromImageAsync(imgBytes);
    }
    public async Task<ImageMetaData> ExtractMetadataFromImage(byte[] imgBytes)
    {
      var taskDescription = visionService.MetaDataFromImageAsync(imgBytes);
      var taskLength = measurementService.Measure(imgBytes);
      await Task.WhenAll(taskDescription, taskLength);
      return new ImageMetaData()
      {
        Description = taskDescription.Result,
        Length = taskLength.Result.Length,
        ImageStream = taskLength.Result.ImageStream,
      };
    }

    public Task SaveInspectionResult(string boatId, ImageMetaData metadata)
    {
      return inspectionRepository.AddOrUpdateInspection(boatId, metadata);
    }
  }

  
}
