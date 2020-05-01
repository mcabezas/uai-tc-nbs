using System.Net;

namespace Common.Http
{
    public interface IHttpHandler
    {
        public void Handle(HttpListenerRequest request, HttpListenerResponse response);
    }
}