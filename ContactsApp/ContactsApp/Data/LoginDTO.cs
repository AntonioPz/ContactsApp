using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Data
{
    public class LoginDTO
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Error { get; set; }
    }
}
