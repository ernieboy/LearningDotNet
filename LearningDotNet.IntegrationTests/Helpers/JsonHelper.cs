using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace LearningDotNet.Api.IntegrationTests.Helpers;
internal static class JsonHelper
{
    public static JsonSerializerOptions GetJsonSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    public static StringContent BuildJsonStringContentFromObject(object toSerialise)
    {
        return new StringContent(JsonSerializer.Serialize(toSerialise, JsonHelper.GetJsonSerializerOptions()), Encoding.UTF8)
        {
            Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
        };
    }
}