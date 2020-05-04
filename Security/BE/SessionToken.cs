using System;

namespace Security.BE
{
    internal class SessionToken
    {
        public User User { get; set; }
        public string Token { get; set; }

        public bool Active { get; set; }

        public DateTime ExpireAt { get; set; }
    }
}