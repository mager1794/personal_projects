using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication2.Classes
{

    public class Email 
    {
        public int Id
        {
            get;
            set;
        }

        public Email GetFromString(string text)
        {
            return new Email(text);
        }

        public Email(string email)
        {
            EmailAddress = email;
        }
        public string EmailAddress
        {
            get;
            set;
        }

        public override string ToString()
        {
            return EmailAddress;
        }
    }
}