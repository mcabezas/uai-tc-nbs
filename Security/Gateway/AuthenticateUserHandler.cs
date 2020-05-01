using System.Net;
using System.Text.Json;
using Common.Http;
using Security.BLL;
using SessionToken = Security.BE.SessionToken;


namespace Security.Gateway
{
    public class AuthenticateUserHandler : IHttpHandler
    {
        private readonly User _bll;

        internal AuthenticateUserHandler(User bll)
        {
            _bll = bll;
        }

        public void Handle(HttpListenerRequest req, HttpListenerResponse res)
        {
            var credentialsFound = RequestReader.ReadBody<AuthenticateUserCommand>(req);
            if (!credentialsFound.IsPresent)
            {
                ResponseWriter.Write(res, HttpStatusCode.Unauthorized, "");
                return;
            }

            var credentials = credentialsFound.Get();
            if (credentials.IsValid())
            {
                ResponseWriter.Write(res, HttpStatusCode.Unauthorized, "");
            }

            var found = _bll.Authenticate(credentials);

            if (!found.IsPresent)
            {
                ResponseWriter.Write(res, HttpStatusCode.Unauthorized, "");
                return;
            }

            var response = new AuthenticateUserResponse(found.Get());
            var body = JsonSerializer.Serialize(response);
            ResponseWriter.Write(res, body);
        }
    }

    internal class AuthenticateUserCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public bool IsValid()
        {
            return Email != "" && Password != "";
        }
    }

    internal class AuthenticateUserResponse
    {
        public string Token { get; }

        public AuthenticateUserResponse(SessionToken sessionToken)
        {
            Token = sessionToken.Token;
        }
    }
}