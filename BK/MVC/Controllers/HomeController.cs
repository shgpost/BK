using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BoatInspectionService;

namespace MVC.Controllers
{
  public class HomeController : Controller
  {
    private readonly InspectionFacade inspectionInspectionFacade;

    public HomeController(InspectionFacade inspectionInspectionFacade)
    {
      this.inspectionInspectionFacade = inspectionInspectionFacade;
    }
    public ActionResult Index()
    {
      return View();
    }


    [HttpPost]
    public async Task<ActionResult> CaptureText(string data)
    {
      byte[] imgBytes = ExtractBytes(data);
      var result = await inspectionInspectionFacade.ExtractTextFromImage(imgBytes);
      return Json(new { Text = result });
    }

    [HttpPost] //todo shg DRY
    public Task<EmptyResult> SaveData(string boatId, string meta)
    {
      return Task.FromResult(new EmptyResult()); //todo shg redirect to index and remove boiler from js?
    }

    [HttpPost]//todo shg DRY
    public async Task<ActionResult> CaptureMeta(string data)
    {
      byte[] imgBytes = ExtractBytes(data);
      var result = await inspectionInspectionFacade.ExtractMetadataFromImage(imgBytes);
      return Json(new
      {
        Length = result.Length,
        ImageStream = Convert.ToBase64String(result.ImageStream),
        Description = result.Description
      });
    }

    private static byte[] ExtractBytes(string base64)
    {
      string rawData = base64.Trim().Split(',').Last();
      return Convert.FromBase64String(rawData);
    }
   
  }
}