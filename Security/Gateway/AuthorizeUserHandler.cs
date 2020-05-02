using System.Net;
using System.Text.Json;
using Http.Router;
using Security.BLL;


namespace Security.Gateway
{
    public class AuthorizeUserHandler : IHttpHandler
    {
        private readonly User _bll;

        internal AuthorizeUserHandler(User bll)
        {
            _bll = bll;
        }

        public void Handle(HttpListenerRequest req, HttpListenerResponse res)
        {
            var authorizeRequestIntent = RequestReader.ReadBody<AuthorizeUserCommand>(req);
            if (!authorizeRequestIntent.Found)
            {
                ResponseWriter.Write(res, HttpStatusCode.Unauthorized, "");
                return;
            }

            if (!authorizeRequestIntent.Found)
            {
                ResponseWriter.Write(res, HttpStatusCode.Unauthorized, "");
                return;
            }

            var authorizeRequest = authorizeRequestIntent.Get();
            var authorized = _bll.Authorize(authorizeRequest);
            if (!authorized)
            {
                ResponseWriter.Write(res, HttpStatusCode.Unauthorized, "");
                return;
            }

            var response = new AuthorizeUserResponse(authorized);
            var body = JsonSerializer.Serialize(response);
            ResponseWriter.Write(res, body);
        }
    }

    internal class AuthorizeUserCommand : ICommand
    {
        public string Token { get; set; }
        public string Action { get; set; }

        public bool IsValid()
        {
            return Token != "" && Action != "";
        }
    }

    internal class AuthorizeUserResponse
    {
        public bool Authorized { get; }

        public AuthorizeUserResponse(bool authorized)
        {
            Authorized = authorized;
        }
    }
}