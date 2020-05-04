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

            var credentials = credentialsIntent.Get();
            if (!credentials.IsValid())
            {
                ResponseWriter.Write(res, HttpStatusCode.Unauthorized, "");
                return;
            }

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

    class AuthenticateUserCommand : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public bool IsValid()
        {
            return Email != "" && Password != "";
        }
    }

    class AuthenticateUserResponse
    {
        public string Token { get; }

        public AuthenticateUserResponse(SessionToken sessionToken)
        {
            Token = sessionToken.Token;
        }
    }
}