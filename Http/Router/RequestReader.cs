using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using Common;

namespace Http.Router
{
    public static class RequestReader
    {
        public static MaybeEmpty<T> ReadBody<T>(HttpListenerRequest req)
        {
            using var receiveStream = req.InputStream;
            using var readStream = new StreamReader(receiveStream, Encoding.UTF8);
            var rawBody = readStream.ReadToEnd();
            try
            {
                var request = JsonSerializer.Deserialize<T>(rawBody);
                return MaybeEmpty<T>.Of(request);
            }
            catch
            {
                return MaybeEmpty<T>.Empty();
            }
        }
    }
}