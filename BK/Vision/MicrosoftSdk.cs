using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Rest;

namespace Vision
{
  public class MicrosoftSdk
  {
    public async Task<string> TextFromImageAsync(byte[] data)
    {
      ComputerVisionClient computerVision = new ComputerVisionClient(new ApiKeyServiceClientCredentials("8715a698ede2411b8909589611ad3f6d"), new System.Net.Http.DelegatingHandler[] { });
      computerVision.Endpoint = "https://westcentralus.api.cognitive.microsoft.com";
      HttpOperationHeaderResponse<RecognizeTextInStreamHeaders> res = await computerVision.RecognizeTextInStreamWithHttpMessagesAsync(new MemoryStream(data), TextRecognitionMode.Printed);
      var str = await res.Response.Content.ReadAsStringAsync();
      var id = res.Headers.OperationLocation;
      const int numberOfCharsInOperationId = 36;
      id = id.Substring(id.Length - numberOfCharsInOperationId);
      TextOperationResult result = await computerVision.GetTextOperationResultAsync(id);
      var r = res.Response.Content.ReadAsStringAsync();
      return r.ToString();
    }
  }
}
