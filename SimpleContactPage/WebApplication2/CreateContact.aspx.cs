using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//using System.Data.SqlServerCe;
using System.Data.SqlClient;

namespace WebApplication2
{
    public partial class EditContact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }


        protected void button1_Click(object sender, EventArgs e)
        {
            emails.Items.Add(new ListItem(email.Text));
        }

        protected void button3_Click(object sender, EventArgs e)
        {
            if(emails.SelectedIndex != -1)
                emails.Items.Remove(emails.SelectedItem);
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        protected void email_add(object sender, EventArgs e)
        {
            emails.Items.Add(new ListItem(email.Text, email.Text));
        }

        protected void address_add(object sender, EventArgs e)
        {
            //Insert Validation


            //Create Address Class
            Classes.Address usr_address = new Classes.Address();
            usr_address.Street = street.Text;
            if (suite.Text.Length > 0)
                usr_address.Suite = suite.Text;
            else
                usr_address.Suite = "N/A";
            usr_address.City = city.Text;
            usr_address.State = state.Text;
            if(zip.Text.Length > 0)
                usr_address.Zip = int.Parse(zip.Text);

            addresses.Items.Add(new ListItem(usr_address.ToString(), usr_address.GetAddress()));
        }

        protected void numbers_add(object sender, EventArgs e)
        {

            numbers.Items.Add(new ListItem(numberType.Text + " | " + areacode.Text + phonenumber.Text));
        }

        protected void number_remove(object sender, EventArgs e)
        {
            if (numbers.SelectedIndex != -1)
                numbers.Items.Remove(numbers.SelectedItem);
        }

        protected void address_remove(object sender, EventArgs e)
        {
            if (addresses.SelectedIndex != -1)
                addresses.Items.Remove(addresses.SelectedItem);
        }

        protected void submitbutton_Click(object sender, EventArgs e)
        {
            int product_Index = 0;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='|DataDirectory|\Database.mdf';Integrated Security=True";

            conn.Open();


            SqlCommand cmd = new SqlCommand("CreatePerson", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Index", "");
            cmd.Parameters.AddWithValue("@FirstName", firstname.Text);
            cmd.Parameters.AddWithValue("@LastName", lastname.Text);
            cmd.Parameters.AddWithValue("@Company", company.Text);
            cmd.Parameters.AddWithValue("@Title", title.Text);

            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("GetNextIndex", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            product_Index = ((int)cmd.ExecuteScalar());

            foreach(ListItem li in emails.Items)
            {
                cmd = new SqlCommand("CreateEmail", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PersonId", product_Index);
                cmd.Parameters.AddWithValue("@Email", li.Value);

                cmd.ExecuteNonQuery();
            }

            foreach (ListItem li in numbers.Items)
            {
                string[] phoneData = li.Value.Split(new string[] { " | " }, StringSplitOptions.RemoveEmptyEntries);
                if (phoneData[0] == "Fax")
                {
                    cmd = new SqlCommand("CreateFax", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PersonId", product_Index);
                    cmd.Parameters.AddWithValue("@AreaCode", phoneData[1].Substring(0,3));
                    cmd.Parameters.AddWithValue("@Number", phoneData[1]);

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd = new SqlCommand("CreatePhone", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PersonId", product_Index);
                    cmd.Parameters.AddWithValue("@AreaCode", phoneData[1].Substring(0, 3));
                    cmd.Parameters.AddWithValue("@Number", phoneData[1]);

                    cmd.ExecuteNonQuery();
                }
            }

            foreach (ListItem li in addresses.Items)
            {
                Classes.Address li_add = Classes.Address.GetAddressFromString(li.Value);
                cmd = new SqlCommand("CreateAddress", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PersonId", product_Index);
                cmd.Parameters.AddWithValue("@Street", li_add.Street);
                cmd.Parameters.AddWithValue("@Suite", li_add.Suite.ToString());
                cmd.Parameters.AddWithValue("@City", li_add.City);
                cmd.Parameters.AddWithValue("@State", li_add.State);
                cmd.Parameters.AddWithValue("@Zip", li_add.Zip.ToString());

                cmd.ExecuteNonQuery();
            }

            conn.Close();
            Response.Redirect("default.aspx");
        }


    }
}