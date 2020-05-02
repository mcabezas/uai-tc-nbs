using System;
using System.Dynamic;

namespace Security.BE
{
    internal class SessionToken
    {
        public User User { get; set; }
        public string Token { get; set; }
        
        public DateTime ExpireAt { get; set; }
    }
}