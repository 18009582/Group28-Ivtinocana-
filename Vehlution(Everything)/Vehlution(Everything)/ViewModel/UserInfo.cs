using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vehlution_Everything_.ViewModel
{
    public class UserInfo
    {
        public int UserID { get; set; }
        public int UserRoleID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Blocked { get; set; }
    }
}