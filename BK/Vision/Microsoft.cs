using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Vision
{
  public interface IVision
  {
    Task<string> TextFromImageAsync(byte[] imageStream);
    Task<string> MetaDataFromImageAsync(byte[] imageStream);
  }
  //todo shg DRY
  public class MsVision : IVision
  {
    private const string subscriptionKey = "8715a698ede2411b8909589611ad3f6d";
    public async Task<string> TextFromImageAsync(byte[] imageStream)
    {
      const string uriBase = "https://westcentralus.api.cognitive.microsoft.com/vision/v2.0/ocr";
      const string requestParameters = "language=en&detectOrientation=true";
      string uri = string.Format("{0}?{1}", uriBase, requestParameters);

      HttpResponseMessage response;

      HttpClient client = new HttpClient();
      client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

      using (ByteArrayContent content = new ByteArrayContent(imageStream))
      {
        content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        response = await client.PostAsync(uri, content);
      }

      string contentString = await response.Content.ReadAsStringAsync();

      var textNodes = JToken.Parse(contentString).SelectToken("..text", false);
      if (textNodes == null)
      {
        return "";
      }
      return textNodes.ToString();
    }

    //https://cloud.google.com/vision/
    //https://azure.microsoft.com/da-dk/services/cognitive-services/computer-vision/
    public async Task<string> MetaDataFromImageAsync(byte[] imageStream)
    {
      const string uriBase = "https://westcentralus.api.cognitive.microsoft.com/vision/v2.0/analyze";
      const string requestParameters = "visualFeatures=Categories,Description,Color";
      string uri = string.Format("{0}?{1}", uriBase, requestParameters);

      HttpResponseMessage response;
      HttpClient client = new HttpClient();
      client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);


      using (ByteArrayContent content = new ByteArrayContent(imageStream))
      {
        content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        response = await client.PostAsync(uri, content);
      }

      string contentString = await response.Content.ReadAsStringAsync();
      var jTags = JToken.Parse(contentString).SelectToken("..tags", false);
      if (jTags != null && jTags.HasValues)
      {
        var tags = string.Join(",", jTags.Values());
        return tags;
      }

      return "";

    }
  }
}
