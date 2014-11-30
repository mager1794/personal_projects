using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace WebApplication2.Classes
{
    public class FaxNumber : CommNumber 
    {
        public NumberType NumberType
        {
            get { return NumberType.Fax; }
        }

        public void FillWithData(IDataRecord record)
        {
            this.Id = (int)record["Id"];
            this.AreaCode = record["Area Code"].ToString();
            this.Number = record["Number"].ToString();

        }
    }
}