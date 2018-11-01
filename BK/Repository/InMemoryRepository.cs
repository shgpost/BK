using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
  public class InMemoryRepository : IInspectionRepository
  {
    private Dictionary<string,ImageMetaData> list = new Dictionary<string, ImageMetaData>();
    public Task AddOrUpdateInspection(string boatId, ImageMetaData medatada)
    {
      list[boatId] = medatada;
      return Task.FromResult(0);
    }

    public Inspection GetInspectionsByBoatId(string boatId)
    {
      var metaData = list[boatId];
      return new Inspection{BoatId = boatId, Documentation = new []{metaData}};
    }
  }
}
