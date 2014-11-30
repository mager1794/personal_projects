using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace WebApplication2.Classes
{
    public class PhoneNumber : CommNumber
    {
        public NumberType NumberType
        {
            get { return NumberType.Phone; }
        }

        public void FillWithData(IDataRecord record)
        {
            this.Id = (int)record["Id"];
            this.AreaCode = record["Area Code"].ToString();
            this.Number = record["Number"].ToString();

        }
    }
}