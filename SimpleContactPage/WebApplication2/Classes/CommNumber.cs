using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2.Classes
{
    public class CommNumber 
    {
        public bool Compare(CommNumber comm)
        {
            if (comm.AreaCode != this.AreaCode)
                return false;
            if (comm.Number != this.Number)
                return false;

            return true;
        }
        public int Id
        {
            get;
            set;
        }

        public string Number
        {
            get;
            set;
        }

        public string AreaCode
        {
            get;
            set;
        }

        public override string ToString()
        {
            return Number;
        }

        public enum NumberType
        {
            Phone,
            Fax
        }
    }
}