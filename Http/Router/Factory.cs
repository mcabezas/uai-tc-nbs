using System.Net.NetworkInformation;

namespace Http.Router
{
    public class Factory
    {
        public static IRouter CreateRouter()
        {
            return new Router();
        }
    }
}