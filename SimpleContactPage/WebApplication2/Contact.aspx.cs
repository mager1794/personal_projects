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
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            if (id != null)
            {
                editcontactlink.HRef = "EditContact.aspx?id=" + id;
                //Initiate A Switch Function For Multiple Search Methods.
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='|DataDirectory|\Database.mdf';Integrated Security=True";
                conn.Open();



                //Populate Person Information
                
                SqlCommand cmd = new SqlCommand("GetPersonInfo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Index", id);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    name.InnerText = reader["First Name"].ToString() + " " + reader["Last Name"].ToString();
                    title.InnerText = reader["Title"].ToString();
                    company.InnerText = reader["Company"].ToString();
                }

                reader.Close();

                ///Populate Emails

                cmd = new SqlCommand("GetEmails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Index", id);

                reader = cmd.ExecuteReader();
                string texttoadd = "";
                while (reader.Read())
                {
                    texttoadd += reader["Email"].ToString() + "<br />";
                }
                reader.Close();
                emails.InnerHtml = texttoadd;

                ///Populate Phone Numbers

                cmd = new SqlCommand("GetPhoneNumbers", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Index", id);

                reader = cmd.ExecuteReader();
                texttoadd = "";
                while (reader.Read())
                {
                    texttoadd += reader["Number"].ToString() + "<br />";
                }
                reader.Close();
                phonenumbers.InnerHtml = texttoadd;

                ///Populate Fax Numbers

                cmd = new SqlCommand("GetFaxNumbers", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Index", id);

                reader = cmd.ExecuteReader();
                texttoadd = "";
                while (reader.Read())
                {
                    texttoadd += reader["Number"].ToString() + "<br />";
                }
                reader.Close();
                faxes.InnerHtml = texttoadd;

                ///Populate Addresses

                cmd = new SqlCommand("GetAddresses", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Index", id);

                reader = cmd.ExecuteReader();
                texttoadd = "";
                while (reader.Read())
                {
                    texttoadd += reader["Street"].ToString();
                    if (!reader["Suite"].ToString().Equals("N/A"))
                        texttoadd += " " + reader["Suite"].ToString();
                    texttoadd += ", ";
                    texttoadd += reader["City"].ToString() + "";
                    texttoadd += ", " + reader["State"].ToString();
                    texttoadd += ", " + reader["Zip"].ToString();

                    texttoadd += "<br />";
                       
                }
                reader.Close();
                addresses.InnerHtml = texttoadd;


                conn.Close();


                ///Eliminate the labels if there is no data beneath them
                if (addresses.InnerHtml == "")
                    address_label.InnerHtml = "";

                if (faxes.InnerHtml == "")
                    fax_label.InnerHtml = "";

                if (phonenumbers.InnerHtml == "")
                    phone_label.InnerHtml = "";

                if (emails.InnerHtml == "")
                    email_label.InnerHtml = "";
            }
        }
    }
}