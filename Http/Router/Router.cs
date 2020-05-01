using System.Linq;
using System.Net;
using Common.Http;

namespace Http.Router
{
    public class Router : IRouter
    {
        private readonly INode _root;
        private const string PathSeparator = "/";

        public Router()
        {
            _root = new Node();
        }

        public IRouter AddRequestHandler(string route, string method, IHttpHandler handler)
        {
            _root.AddHandler(PreparePaths(route), method, handler);
            return this;
        }

        public void HandleRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            var handler = GetHandlerRequest(request.RawUrl, request.HttpMethod);
            if (handler == null)
            {
                ResponseWriter.Write(response, HttpStatusCode.NotFound, "NOT FOUND");
                return;
            }

            handler.Handle(request, response);
        }

        private IHttpHandler GetHandlerRequest(string route, string method)
        {
            return _root.GetHandler(PreparePaths(route), method);
        }
        
        #region Helpers

        private static string[] PreparePaths(string route)
        {
            var paths = route.Split(PathSeparator);
            if (paths.Length > 0 && paths[0] == "")
            {
                paths = paths.Skip(1).ToArray();
            }

            return paths;
        }

        #endregion
    }
}