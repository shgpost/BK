using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
  public interface IInspectionRepository
  {
    Task AddOrUpdateInspection(string boatId, ImageMetaData medatada);
    Inspection GetInspectionsByBoatId(string boatId);
  }

  //todo shg would fit in domain layer
  public class ImageMetaData
  {
    public float Length { get; set; }
    public byte[] ImageStream { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
   // public bool InspectionResult { get; set; }
    public uint InspectorId { get; set; }
  }

  public class Inspection
  {
    //public string Id { get; set; } // boatnumber+date
    public string BoatId { get; set; }
    
    
    public ImageMetaData[] Documentation { get; set; }
  }
}
