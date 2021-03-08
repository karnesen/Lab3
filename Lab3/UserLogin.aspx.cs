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
    public partial class UserLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Get("loggedout") == "true")
            {
                lblStatus.Text = "User has logged out successfully";
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // connect to database to retrieve stored password string
            try
            {
                System.Data.SqlClient.SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["AUTH"].ConnectionString.ToString());
                lblStatus.Text = "Database Connection Successful";

                
                System.Data.SqlClient.SqlCommand findPass = new System.Data.SqlClient.SqlCommand();
                findPass.Connection = sc;
                findPass.CommandType = System.Data.CommandType.StoredProcedure;
                // SELECT PASSWORD STRING WHERE THE ENTERED USERNAME MATCHES
                findPass.CommandText = "JeremyEzellLab3";
                findPass.Parameters.Add(new SqlParameter("@Username", txtUsername.Text.ToString()));
                sc.Open();
                SqlDataReader reader = findPass.ExecuteReader(); // create a reader

                if (reader.HasRows) // if the username exists, it will continue
                {
                    while (reader.Read()) // this will read the single record that matches the entered username
                    {
                        string storedHash = reader["PasswordHash"].ToString(); // store the database password into this variable

                        if (PasswordHash.ValidatePassword(txtPassword.Text, storedHash)) // if the entered password matches what is stored, it will show success
                        {
                            lblStatus.Text = "Success!";
                            btnLogin.Enabled = false;
                            txtUsername.Enabled = false;
                            txtPassword.Enabled = false;
                            Session["CustomerUsername"] = txtUsername.Text;
                            Session["CustomerID"] = reader.GetInt32(1);
                            Response.Redirect("UserRequestService.aspx", false);
                        }
                        else
                            lblStatus.Text = "Password is wrong.";
                    }
                }
                else // if the username doesn't exist, it will show failure
                    lblStatus.Text = "Login failed.";

                sc.Close();
            }
            catch
            {
                lblStatus.Text = "Database Error.";
            }
        }

        protected void lnkCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewUser.aspx", false);
        }
    }
}