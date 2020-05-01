using Common.Http;
using Http.Router;

namespace CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            var securityGateway = new Security.Gateway.Gateway();
            IRouter router = new Router();
            router = securityGateway.RoutesUp(router);

            Http.Server.HttpServer.Start("http://*:8888/", router);
        }
    }
}