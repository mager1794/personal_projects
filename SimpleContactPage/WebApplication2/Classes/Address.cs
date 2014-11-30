using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication2.Classes
{
    public class Address
    {
        public bool CompareAddress(Address address2)
        {
            if (this.Street != address2.Street)
                return false;
            if (this.Suite != address2.Suite)
                return false;
            if (this.State != address2.State)
                return false;
            if (this.Zip != address2.Zip)
                return false;

            if (this.City != address2.City)
                return false;

            return true;
        }
        public int Id
        {
            get;
            set;
        }

        public string Street
        {
            get;
            set;
        }

        public string Suite
        {
            get;
            set;
        }

        public string City
        {
            get;
            set;
        }

        public string State
        {
            get;
            set;
        }

        public int Zip
        {
            get;
            set;
        }

        public string GetAddress()
        {
            string address = Street + "|";

            if (Suite.Length > 0)
                address += Suite;
            

            address += "|" + City;
            address += "|" + State;
            address += "|" + Zip;
            return address;
        }

        public static Address GetAddressFromString(string address)
        {
            string[] info = address.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            Address add = new Address();
            add.Street = info[0];
            add.Suite = info[1];
            add.City = info[2];
            add.State = info[3];
            add.Zip = int.Parse(info[4]);

            return add;
        }

        public override string ToString()
        {

            return Street + " " + City + ", " + State;
        }
    }
}