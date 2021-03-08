//Kirsi And Josh Coleman 2/15/21
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Lab2
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Get("loggedout") == "true")
            {
                lblLoginMessage.Text = "User has logged out successfully";
            }

            if (Session["InvalidUse"] != null)
            {
                lblLoginMessage.Text = Session["InvalidUse"].ToString();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                System.Data.SqlClient.SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["AUTH"].ConnectionString.ToString());
                
                System.Data.SqlClient.SqlCommand findPass = new System.Data.SqlClient.SqlCommand();
                findPass.Connection = sc;
                findPass.CommandType = System.Data.CommandType.Text;
                // SELECT PASSWORD STRING WHERE THE ENTERED USERNAME MATCHES
                findPass.CommandText = "SELECT PasswordHash, EmployeeID FROM ePass WHERE Username = @Username";
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
                            Session["UserName"] = HttpUtility.HtmlEncode(txtUsername.Text);
                            Session["EmployeeID"] = reader["EmployeeID"];
                            Response.Redirect("HomePage.aspx");
                        }
                        else
                            lblLoginMessage.Text = "Either username or password is incorrect";
                    }
                }
                else // if the username doesn't exist, it will show failure
                    lblLoginMessage.Text = "Either username or password is incorrect";

                sc.Close();
            }
            catch
            {
                lblLoginMessage.Text = "Database Error.";
            }
        }
    }
}