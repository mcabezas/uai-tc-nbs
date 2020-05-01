using Common.Http;

namespace Http.Router
{
    internal interface INode
    {
        public void AddHandler(string[] paths, string method, IHttpHandler handler);
        public IHttpHandler GetHandler(string[] paths, string method);
    }
}