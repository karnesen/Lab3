//Kirsi And Josh Coleman 2/15/21
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab3
{
    public partial class NewUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillStates();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // COMMIT VALUES
            if (Page.IsValid)
            {
                try
                {
                    System.Data.SqlClient.SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["AUTH"].ConnectionString.ToString());
                    lblStatus.Text = "Database Connection Successful";

                    sc.Open();

                    System.Data.SqlClient.SqlCommand createUser = new System.Data.SqlClient.SqlCommand();
                    createUser.Connection = sc;
                    // INSERT USER RECORD
                    createUser.CommandText = "INSERT INTO Person (FirstName, LastName, Username, phoneNumber, phoneType, streetAddress, City, State, zipcode, new) " +
                    "VALUES (@FName, @LName, @Username, @phone, @phonetype, @address, @city, @state, @zip, 1)";
                    createUser.Parameters.Add(new SqlParameter("@FName", txtFirstName.Text));
                    createUser.Parameters.Add(new SqlParameter("@LName", txtLastName.Text));
                    createUser.Parameters.Add(new SqlParameter("@Username", txtUsername.Text));
                    createUser.Parameters.Add(new SqlParameter("@phone", txtPhoneNumber.Text));
                    createUser.Parameters.Add(new SqlParameter("@phonetype", ddlPhoneNumberType.SelectedValue));
                    createUser.Parameters.Add(new SqlParameter("@address", txtAddress.Text));
                    createUser.Parameters.Add(new SqlParameter("@city", txtCity.Text));
                    createUser.Parameters.Add(new SqlParameter("@state", ddlState.SelectedValue));
                    createUser.Parameters.Add(new SqlParameter("@zip", txtZipCode.Text));
                    createUser.ExecuteNonQuery();

                    System.Data.SqlClient.SqlCommand setPass = new System.Data.SqlClient.SqlCommand();
                    setPass.Connection = sc;
                    // INSERT PASSWORD RECORD AND CONNECT TO USER
                    setPass.CommandText = "INSERT INTO Pass (UserID, Username, PasswordHash) VALUES ((select max(userid) from person), @Username, @Password)";
                    setPass.Parameters.Add(new SqlParameter("@Username", txtUsername.Text));
                    setPass.Parameters.Add(new SqlParameter("@Password", PasswordHash.HashPassword(txtPassword.Text))); // hash entered password
                    setPass.ExecuteNonQuery();

                    sc.Close();

                    lblStatus.Text = "User committed!";
                    txtFirstName.Text = "";
                    txtLastName.Text = "";
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    txtAddress.Text = "";
                    txtCity.Text = "";
                    txtZipCode.Text = "";
                    ddlState.SelectedIndex = -1;
                    ddlPhoneNumberType.SelectedIndex = -1;
                    txtPhoneNumber.Text = "";
                }
                catch
                {
                    lblStatus.Text = "Database Error - User not committed.";
                }
            }

        }

        protected void lnkLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("userLogin.aspx", false);
        }

        protected void fillStates()
        {
            List<String> states = new List<String> { "AL", "AK", "AS", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FL", "GA", "GU", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "MP", "OH", "OK", "OR", "PA", "PR", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "VI", "WA", "WV", "WI", "WY" };
            foreach (String i in states)
            {
                ddlState.Items.Add(new ListItem(i));
            }

        }

        protected void cvCheckUniqueCustomer_ServerValidate(object source, ServerValidateEventArgs args)
        {
            String email = txtUsername.Text;
            String sqlQuery = "Select * from Person where Username = @email";
            // Define the connection to the Database:
            SqlConnection sqlConnect = new SqlConnection(WebConfigurationManager.ConnectionStrings["AUTH"].ConnectionString);
            // Create the SQL Command object which will send the query:
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Parameters.Add(new SqlParameter("@email", email));
            sqlCommand.Connection = sqlConnect;
            sqlCommand.CommandType = System.Data.CommandType.Text;
            sqlCommand.CommandText = sqlQuery;
            // Open your connection, send the query, retrieve the results:
            sqlConnect.Open();
            SqlDataReader queryResults = sqlCommand.ExecuteReader();
            if (queryResults.HasRows)
            {
                args.IsValid = false;
            }

            else
            {
                args.IsValid = true;
            }
            sqlConnect.Close();
        }
    }
}