using Common.Http;
using Security.BLL;

namespace Security.Gateway
{
    public class Gateway
    {
        private readonly BLL.User _user;

        public Gateway()
        {
            var sessionToken = new SessionToken();
            _user = new User(sessionToken);
        }

        public IRouter RoutesUp(IRouter router)
        {
            router
                .AddRequestHandler("/users/authenticate", "PUT", new AuthenticateUserHandler(_user));
            return router;
        }
    }
}