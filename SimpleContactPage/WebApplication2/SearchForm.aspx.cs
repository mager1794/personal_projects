using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string searchval = Request.Form["search"];

            if (searchval != null)
            {
                //Initiate A Switch Function For Multiple Search Methods.
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='|DataDirectory|\Database.mdf';Integrated Security=True";
                string SqlCommand = "SearchName";

                switch(int.Parse(Request.Form["SearchChoice"]))
                {
                    case 1:
                        SqlCommand = "SearchName";
                        break;

                    case 2:
                        SqlCommand = "SearchEmail";
                        break;

                    case 3:
                        SqlCommand = "SearchCompany";
                        break;

                    case 4:
                        SqlCommand = "SearchTitle";
                        break;

                    case 5:
                        SqlCommand = "SearchPhone";
                        break;

                    case 6:
                        SqlCommand = "SearchArea";
                        break;
                    
                    case 7:
                        SqlCommand = "SearchFax";
                        break;
                }
                SqlCommand cmd = new SqlCommand(SqlCommand, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", "%" + searchval + "%");

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                List<Classes.Person> searchResults = new List<Classes.Person>();

                searchdetails.Text = "Search results for '" + searchval + "'";

                // Call Read before accessing data. 
                while (reader.Read())
                {
                    Classes.Person p = new Classes.Person();
                    p.Id = (int)reader["Id"];
                    p.FirstName = reader["First Name"].ToString();
                    p.LastName = reader["Last Name"].ToString();
                    p.Company = reader["Company"].ToString();
                    p.Title = reader["Title"].ToString();
                    searchResults.Add(p);
                }

                //Eliminating Duplicates.
                searchResults = searchResults.Distinct().ToList();

                foreach (Classes.Person person in searchResults)
                {
                    TableRow tempRow = new TableRow();
                    TableCell cell = new TableCell();
                    cell.BorderStyle = BorderStyle.None;
                    cell.Text = person.FirstName;
                    tempRow.Cells.Add(cell);
                    cell = new TableCell();
                    cell.BorderStyle = BorderStyle.None;
                    cell.Text = person.LastName;
                    tempRow.Cells.Add(cell);
                    cell = new TableCell();
                    cell.BorderStyle = BorderStyle.None;
                    cell.Text = "<a href='Contact.aspx?id=" + person.Id.ToString() + "'>Visit Contact Page</a>";
                    tempRow.Cells.Add(cell);
                    Table1.Rows.Add(tempRow);
                }
                conn.Close();
            }
            
               
        }
    }
}