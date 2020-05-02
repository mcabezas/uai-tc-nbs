using System;
using System.Net;
using Http.Router;

namespace Http.Server
{
    public class HttpServer
    {
        public static void Start(string prefix, IRouter router)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
            
            var listener = new HttpListener();
            listener.Prefixes.Add(prefix);
            listener.Start();
            Console.WriteLine("Listening... at: " + prefix);
            while (true)
            {
                var context = listener.GetContext();
                var request = context.Request;
                var response = context.Response;

                Console.WriteLine($"requestId {request.RequestTraceIdentifier} Method {request.HttpMethod} resource {request.Url} ");
                if (response.StatusCode != (int)HttpStatusCode.LengthRequired)
                {
                    router.HandleRequest(request, response);
                }
            }
            listener.Stop();
        }
    }
}