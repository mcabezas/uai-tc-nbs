using System.Net;
using System.Text.Json;
using Http.Router;
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
            var credentialsIntent = RequestReader.ReadBody<AuthenticateUserCommand>(req);
            if (!credentialsIntent.Found)
            {
                ResponseWriter.Write(res, HttpStatusCode.Unauthorized, "");
                return;
            }

            if (!credentialsIntent.Found)
            {
                ResponseWriter.Write(res, HttpStatusCode.Unauthorized, "");
                return;
            }

            var credentials = credentialsIntent.Get();
            var sessionTokenIntent = _bll.Authenticate(credentials);
            if (!sessionTokenIntent.Found)
            {
                ResponseWriter.Write(res, HttpStatusCode.Unauthorized, "");
                return;
            }

            var response = new AuthenticateUserResponse(sessionTokenIntent.Get());
            var body = JsonSerializer.Serialize(response);
            ResponseWriter.Write(res, body);
        }
    }

    internal class AuthenticateUserCommand : ICommand
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