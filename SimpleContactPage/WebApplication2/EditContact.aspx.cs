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
    public partial class AddNewContact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            if (id != null && indexvalue.Value == "")
            {
                //Initiate A Switch Function For Multiple Search Methods.
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='|DataDirectory|\Database.mdf';Integrated Security=True";
                conn.Open();

                indexvalue.Value = id;


                //Populate Person Information

                SqlCommand cmd = new SqlCommand("GetPersonInfo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Index", id);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    firstname.Text = reader["First Name"].ToString();
                    lastname.Text = reader["Last Name"].ToString();
                    title.Text = reader["Title"].ToString();
                    company.Text = reader["Company"].ToString();
                }

                reader.Close();

                ///Populate Emails

                cmd = new SqlCommand("GetEmails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Index", id);

                reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    emails.Items.Add(new ListItem(reader["Email"].ToString(), reader["Email"].ToString()));
                }
                reader.Close();

                ///Populate Phone Numbers

                cmd = new SqlCommand("GetPhoneNumbers", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Index", id);

                reader = cmd.ExecuteReader();
               
                while (reader.Read())
                {
                    string itemvalue = "Phone | " + reader["number"];
                    numbers.Items.Add(new ListItem(itemvalue, itemvalue));
                }
                reader.Close();

                ///Populate Fax Numbers

                cmd = new SqlCommand("GetFaxNumbers", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Index", id);

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string itemvalue = "Fax | " + reader["number"];
                    numbers.Items.Add(new ListItem(itemvalue, itemvalue));
                }
                reader.Close();

                ///Populate Addresses

                cmd = new SqlCommand("GetAddresses", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Index", id);

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Classes.Address usr_address = new Classes.Address();
                    usr_address.Street = reader["Street"].ToString();
                    usr_address.Suite = reader["Suite"].ToString();
                    usr_address.City = reader["City"].ToString();
                    usr_address.State = reader["State"].ToString();
                    if (zip.Text.Length > 0)
                        usr_address.Zip = int.Parse(reader["Zip"].ToString());

                    addresses.Items.Add(new ListItem(usr_address.ToString(), usr_address.GetAddress()));
                }
                reader.Close();


                conn.Close();

            }
        }

        protected void button1_Click(object sender, EventArgs e)
        {
            emails.Items.Add(new ListItem(email.Text));
        }

        protected void button3_Click(object sender, EventArgs e)
        {
            if (emails.SelectedIndex != -1)
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
            if (zip.Text.Length > 0)
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
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='|DataDirectory|\Database.mdf';Integrated Security=True";

            // 1.  create a command object identifying the stored procedure
            conn.Open();


            SqlCommand cmd = new SqlCommand("UpdatePerson", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Index", "");
            cmd.Parameters.AddWithValue("@FirstName", firstname.Text);
            cmd.Parameters.AddWithValue("@LastName", lastname.Text);
            cmd.Parameters.AddWithValue("@Company", company.Text);
            cmd.Parameters.AddWithValue("@Title", title.Text);

            cmd.ExecuteNonQuery();

            ///Generate Emails From Database for comparison algorithm
            cmd = new SqlCommand("GetEmails", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Index", indexvalue.Value);

            SqlDataReader reader = cmd.ExecuteReader();

            List<Classes.Email> liEmails = new List<Classes.Email>();
            while (reader.Read())
            {
                Classes.Email em = new Classes.Email(reader["Email"].ToString());
                em.Id = int.Parse(reader["Id"].ToString());

                liEmails.Add(em);
            }
            reader.Close();

            //Create new emails
            foreach (ListItem li in emails.Items)
            {
                bool createNew = true;
               foreach(Classes.Email em in liEmails)
               {
                   if(li.Value == em.EmailAddress )
                       createNew = false;
               }

              if(createNew)
              {
                  cmd = new SqlCommand("CreateEmail", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PersonId", indexvalue.Value);
                cmd.Parameters.AddWithValue("@Email", li.Value);

                cmd.ExecuteNonQuery();
              }
            }
            //Delete missing Emails
            foreach (Classes.Email em in liEmails)
            {
                bool deleteEmail = true;
                foreach (ListItem li in emails.Items)
                {
                    if (li.Value == em.EmailAddress)
                        deleteEmail = false;
                }

                if (deleteEmail)
                {
                    cmd = new SqlCommand("DeleteEmail", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", em.Id);

                    cmd.ExecuteNonQuery();
                }
            }

            ///Generate Emails From Database for comparison algorithm
            cmd = new SqlCommand("GetAddresses", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Index", indexvalue.Value);

            reader = cmd.ExecuteReader();

            List<Classes.Address> liAddress = new List<Classes.Address>();
            while (reader.Read())
            {
                Classes.Address em = new Classes.Address();
                em.Id = int.Parse(reader["Id"].ToString());
                em.Street = reader["Street"].ToString();
                em.Suite = reader["Suite"].ToString();
                em.City = reader["City"].ToString();
                em.State = reader["State"].ToString();
                em.Zip = int.Parse(reader["Zip"].ToString());

                liAddress.Add(em);
            }
            reader.Close();

            //Create New Addresses
            foreach (ListItem li in addresses.Items)
            {
                bool createNew = true;
                Classes.Address add1 = Classes.Address.GetAddressFromString(li.Value);
                foreach (Classes.Address em in liAddress)
                {
                    if (add1.CompareAddress(em))
                        createNew = false;
                }

                if (createNew)
                {
                    cmd = new SqlCommand("CreateAddress", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PersonId", indexvalue.Value);
                    cmd.Parameters.AddWithValue("@Street", add1.Street);
                    cmd.Parameters.AddWithValue("@Suite", add1.Suite);
                    cmd.Parameters.AddWithValue("@City", add1.City);
                    cmd.Parameters.AddWithValue("@State", add1.State);
                    cmd.Parameters.AddWithValue("@Zip", add1.Zip.ToString());

                    cmd.ExecuteNonQuery();
                }
            }
            //Delete missing Emails
            foreach (Classes.Address em in liAddress)
            {
                bool deleteaddress = true;
                foreach (ListItem li in addresses.Items)
                {
                    Classes.Address add1 = Classes.Address.GetAddressFromString(li.Value);
                    if (add1.CompareAddress(em))
                        deleteaddress = false;
                }

                if (deleteaddress)
                {
                    cmd = new SqlCommand("DeleteAddress", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", em.Id);

                    cmd.ExecuteNonQuery();
                }
            }

            //Phone And Fax Numbers
            List<Classes.FaxNumber> liFax= new List<Classes.FaxNumber>();
            List<Classes.PhoneNumber> liPhone = new List<Classes.PhoneNumber>();

            //Populate Fax Numbers
            cmd = new SqlCommand("GetFaxNumbers", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Index", indexvalue.Value);

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Classes.FaxNumber em = new Classes.FaxNumber();
                em.Id = int.Parse(reader["Id"].ToString());
                em.AreaCode = reader["AreaCode"].ToString();
                em.Number = reader["Number"].ToString();

                liFax.Add(em);
            }
            reader.Close();


            cmd = new SqlCommand("GetPhoneNumbers", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Index", indexvalue.Value);

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Classes.PhoneNumber em = new Classes.PhoneNumber();
                em.Id = int.Parse(reader["Id"].ToString());
                em.AreaCode = reader["AreaCode"].ToString();
                em.Number = reader["Number"].ToString();

                liPhone.Add(em);
            }

            reader.Close();

            foreach (ListItem li in numbers.Items)
            {
                bool createNew = true;
                string[] phoneData = li.Value.Split(new string[] { " | " }, StringSplitOptions.RemoveEmptyEntries);
                if (phoneData[0] == "Fax")
                {
                    foreach(Classes.FaxNumber fax in liFax)
                    {
                        if (fax.Number == phoneData[1])
                            createNew = false;
                    }
                }
                else
                {
                    foreach (Classes.PhoneNumber phoneNum in liPhone)
                    {

                        if (phoneNum.Number == phoneData[1])
                            createNew = false;
                    }
                }


                if(createNew)
                {
                   
                    if (phoneData[0] == "Fax")
                    {
                        cmd = new SqlCommand("CreateFax", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PersonId", indexvalue.Value);
                        cmd.Parameters.AddWithValue("@AreaCode", phoneData[1].Substring(0, 3));
                        cmd.Parameters.AddWithValue("@Number", phoneData[1]);

                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd = new SqlCommand("CreatePhone", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PersonId", indexvalue.Value);
                        cmd.Parameters.AddWithValue("@AreaCode", phoneData[1].Substring(0, 3));
                        cmd.Parameters.AddWithValue("@Number", phoneData[1]);

                        cmd.ExecuteNonQuery();
                    }
                }
            }

            foreach (Classes.PhoneNumber phoneNum in liPhone)
            {
  
                bool deleteNum = true;
                foreach (ListItem li in numbers.Items)
                {
                    
                    string[] phoneData = li.Value.Split(new string[] { " | " }, StringSplitOptions.RemoveEmptyEntries);

                    if (phoneData[1] == phoneNum.Number && phoneData[0] != "Fax")
                        deleteNum = false;
                }

                if(deleteNum)
                {
                    cmd = new SqlCommand("DeletePhoneNumbers", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", phoneNum.Id);

                    cmd.ExecuteNonQuery();
                }
                
            }

            foreach (Classes.FaxNumber faxNum in liFax)
            {
                bool deleteNum = true;
                foreach (ListItem li in numbers.Items)
                {
                    string[] phoneData = li.Value.Split(new string[] { " | " }, StringSplitOptions.RemoveEmptyEntries);

                    if (phoneData[1] == faxNum.Number && phoneData[0] != "Phone")
                        deleteNum = false;
                }

                if (deleteNum)
                {
                    cmd = new SqlCommand("DeleteFaxNumbers", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", faxNum.Id);

                    cmd.ExecuteNonQuery();
                }

            }

            conn.Close();
            Response.Redirect("Contact.aspx?id=" + indexvalue.Value);
             
        }
    }
}