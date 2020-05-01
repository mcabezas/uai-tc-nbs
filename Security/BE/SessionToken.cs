using System;

namespace Security.BE
{
    internal class SessionToken
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
}