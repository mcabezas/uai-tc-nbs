using System.Net;
using System.Text.Json;
using Http.Router;
using Security.BLL;


namespace Security.Gateway
{
    public class LogoutHandler : IHttpHandler
    {
        private readonly SessionToken _bll;

        internal LogoutHandler(SessionToken bll)
        {
            _bll = bll;
        }

        public void Handle(HttpListenerRequest req, HttpListenerResponse res)
        {
            var revokeSessionIntent = RequestReader.ReadBody<RevokeSessionTokenCommand>(req);
            if (!revokeSessionIntent.Found)
            {
                ResponseWriter.Write(res, HttpStatusCode.Unauthorized, "");
                return;
            }

            var revokeSessionRequest = revokeSessionIntent.Get();
            if (!revokeSessionRequest.IsValid())
            {
                ResponseWriter.Write(res, HttpStatusCode.Unauthorized, "");
                return;
            }

            var authorized = _bll.RevokeToken(revokeSessionRequest);
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

    class RevokeSessionTokenCommand : ICommand
    {
        public string Token { get; set; }

        public bool IsValid()
        {
            return Token != "";
        }
    }

    class RevokeSessionTokenResponse
    {
        public bool Revoked { get; }

        public RevokeSessionTokenResponse(bool revoked)
        {
            Revoked = revoked;
        }
    }
}