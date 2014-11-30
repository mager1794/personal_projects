using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Classes
{
    public class Person
    {
        public bool CompareAddress(Person ppl2)
        {
            return this.Id == ppl2.Id;
        }

        public int Id
        {
            get;
            set;
        }

        public string Company
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }


        public override string ToString()
        {

            return "";
        }
    }
}